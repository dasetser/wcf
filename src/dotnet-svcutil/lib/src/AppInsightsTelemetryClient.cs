// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Tools.ServiceModel.Svcutil
{
    // Provides the TelemetryClient instance for logging telemetry using AppInsights.
    internal class AppInsightsTelemetryClient
    {
        private const string instrumentationKey = "97d0a8a2-1954-4c71-b95d-89df9627dccb";
        internal const string OptOutVariable = "DOTNET_SVCUTIL_TELEMETRY_OPTOUT";
        private const string eventNamePrefix = "VS/dotnetSvcutil/";
        private const string testModeVariable = "DOTNET_SVCUTIL_TEST_MODE";

        private static bool? _isUserOptedIn = null;
        public static bool IsUserOptedIn
        {
            get
            {
                if (!_isUserOptedIn.HasValue)
                {
                    string optOut = Environment.GetEnvironmentVariable(OptOutVariable);
                    if (string.IsNullOrEmpty(optOut))
                    {
                        _isUserOptedIn = true;
                    }
                    else
                    {
                        // We parse the same values here as the dotnet SDK's opt out.
                        switch (optOut.ToLowerInvariant())
                        {
                            case "true":
                            case "1":
                            case "yes":
                                _isUserOptedIn = false;
                                break;
                            case "false":
                            case "0":
                            case "no":
                            default:
                                _isUserOptedIn = true;
                                break;
                        }
                    }
                }

                return _isUserOptedIn.Value;
            }
            set
            {
                _isUserOptedIn = value;
            }
        }

        private static readonly object _lockObj = new object();
        private static AppInsightsTelemetryClient _instance = null;
        private TelemetryClient _telemetryClient = null;

        private AppInsightsTelemetryClient(TelemetryClient telemetryClient)
        {
            this._telemetryClient = telemetryClient;
        }

        public static async Task<AppInsightsTelemetryClient> GetInstanceAsync(CancellationToken cancellationToken)
        {
            if (_instance == null)
            {
                try
                {
                    if (!bool.TryParse(Environment.GetEnvironmentVariable(testModeVariable), out bool testMode))
                    {
                        testMode = false;
                    }

                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            if (!IsUserOptedIn)
                            {
                                // If the user hasn't opted in return now with a null telemetry client to ensure we don't create any telemetry context.
                                return new AppInsightsTelemetryClient(null);
                            }

                            TelemetryConfiguration config;
                            try
                            {
                                config = TelemetryConfiguration.Active;
                            }
                            catch (InvalidOperationException)
                            {
                                config = new TelemetryConfiguration();
                            }

                            config.TelemetryChannel.DeveloperMode = testMode;

                            _instance = new AppInsightsTelemetryClient(new TelemetryClient(config));
                        }
                    }

                    var telemetryClient = _instance._telemetryClient;
                    telemetryClient.InstrumentationKey = instrumentationKey;

                    // Populate context with properties that are common and should be logged for all events.
                    var context = telemetryClient.Context;
                    context.Device.OperatingSystem = GetOperatingSystemString();

#if !NETCORE10
                    // Set the user id to a stable hash of the user's current username. Users with the same username 
                    // or those with hash collisions will show up as the same id. So the user id won't be perfectly unique.
                    // However, it will give us some idea of how many different users are using the tool.
                    context.User.Id = GetStableHashCode(Environment.UserName).ToString();
#endif

                    // DebugLogger tracks telemetry when adding exceptions. We pass null for the logger to avoid the possibility of an endless cyclic call if something goes wrong in GetSdkVersionAsync.
                    var sdkVersion = await ProjectPropertyResolver.GetSdkVersionAsync(System.IO.Directory.GetCurrentDirectory(), null /* logger */, cancellationToken).ConfigureAwait(false);
                    context.Properties["SvcUtil.Version"] = Tool.PackageVersion;
                    context.Properties["Dotnet.Version"] = string.IsNullOrEmpty(sdkVersion) ? "unknown" : sdkVersion;
                    context.Properties["TestMode"] = testMode.ToString();
                }
                catch (Exception ex)
                {
#if DEBUG
                    ToolConsole.WriteWarning(ex.Message);
#endif
                    _isUserOptedIn = false;
                }
            }

            return _instance;
        }

        // This is copied from the 32 bit implementation from String.GetHashCode.
        // It's a stable string hashing algorithm so it won't change with each run of the tool.
        private static int GetStableHashCode(string str)
        {
            unsafe
            {
                fixed (char* src = str)
                {
                    int hash1 = (5381 << 16) + 5381;
                    int hash2 = hash1;
                    
                    int* pint = (int*)src;
                    int len = str.Length;
                    while (len > 2)
                    {
                        hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ pint[0];
                        hash2 = ((hash2 << 5) + hash2 + (hash2 >> 27)) ^ pint[1];
                        pint += 2;
                        len -= 4;
                    }

                    if (len > 0)
                    {
                        hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ pint[0];
                    }

                    return hash1 + (hash2 * 1566083941);
                }
            }
        }

        private static string GetOperatingSystemString()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "macOS";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }
            else
            {
                return "Unknown";
            }
        }
        
        public void TrackEvent(string eventName)
        {
            if (IsUserOptedIn)
            {
                this._telemetryClient.TrackEvent(eventNamePrefix + eventName);
                this._telemetryClient.Flush();
            }
        }

        public void TrackEvent(string eventName, Dictionary<string, string> properties)
        {
            if (IsUserOptedIn)
            {
                this._telemetryClient.TrackEvent(eventNamePrefix + eventName, properties);
                this._telemetryClient.Flush();
            }
        }

        public void TrackError(string eventName, Exception exceptionObject)
        {
            this.TrackError(eventName, exceptionObject.ToString());
        }

        public void TrackError(string eventName, string exceptionString)
        {
            if (IsUserOptedIn)
            {
                var properties = new Dictionary<string, string>();
                properties.Add("ExceptionString", exceptionString);

                this._telemetryClient.TrackEvent(eventNamePrefix + eventName, properties);
                this._telemetryClient.Flush();
            }
        }
    }
}
