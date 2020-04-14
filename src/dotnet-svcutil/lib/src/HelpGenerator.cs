// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Tools.ServiceModel.Svcutil
{
    internal static class HelpGenerator
    {
        private static StringBuilder s_helpBuilder;

        internal static string GenerateHelpText()
        {
            s_helpBuilder = new StringBuilder();
            HelpGenerator.WriteUsage();
            s_helpBuilder.AppendLine();
            s_helpBuilder.AppendLine();
            HelpGenerator.WriteCodeGenerationHelp();
            s_helpBuilder.AppendLine();
            s_helpBuilder.AppendLine();
            HelpGenerator.WriteExamples();
            s_helpBuilder.AppendLine();
            s_helpBuilder.AppendLine();
            return s_helpBuilder.ToString();
        }

        private static void WriteUsage()
        {
            s_helpBuilder.AppendLine(SR.GetString(SR.HelpUsage1));
            s_helpBuilder.AppendLine();
            s_helpBuilder.AppendLine(SR.GetString(SR.HelpUsage2));
        }

        private static void WriteCodeGenerationHelp()
        {
            HelpCategory helpCategory = new HelpCategory(SR.GetString(SR.HelpUsageCategory))
            {
                Inputs = new ArgumentInfo[] {
                    ArgumentInfo.CreateInputHelpInfo(SR.GetString(SR.HelpInputMetadataDocumentPath), SR.GetString(SR.HelpCodeGenerationSyntaxInput1)),
                    ArgumentInfo.CreateInputHelpInfo(SR.GetString(SR.HelpInputUrl),                  SR.GetString(SR.HelpCodeGenerationSyntaxInput2)),
                    ArgumentInfo.CreateInputHelpInfo(SR.GetString(SR.HelpInputEpr),                  SR.GetString(SR.HelpCodeGenerationSyntaxInput3))
                },

                Options = new ArgumentInfo[] {
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.OutputDirectory.Name,   SR.ParametersDirectory,                           SR.GetString(SR.HelpDirectoryFormat, CommandProcessorOptions.Switches.OutputDirectory.Abbreviation)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.NoLogo.Name,                                                              SR.GetString(SR.HelpNologoFormat, CommandProcessorOptions.Switches.NoLogo.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Verbosity.Name,         SR.ParametersVerbosity,                           SR.GetString(SR.HelpVerbosityFormat, string.Join(", ", System.Enum.GetNames(typeof(Verbosity))), CommandProcessorOptions.Switches.Verbosity.Abbreviation)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.Help.Name,                                                                SR.GetString(SR.HelpHelpFormat, CommandProcessorOptions.Switches.Help.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.ProjectFile.Name,       SR.ParametersProjectFile,                         SR.GetString(SR.HelpProjectFileFormat, CommandProcessorOptions.Switches.ProjectFile.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.OutputFile.Name,        SR.ParametersOut,                                 SR.GetString(SR.HelpOutFormat, CommandProcessorOptions.Switches.OutputFile.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Namespace.Name,         SR.ParametersNamespace,                           SR.GetString(SR.HelpNamespaceFormat, CommandProcessorOptions.Switches.Namespace.Abbreviation), true),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.MessageContract.Name,                                                     SR.GetString(SR.HelpMessageContractFormat, CommandProcessorOptions.Switches.MessageContract.Abbreviation), true),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.EnableDataBinding.Name,                                                   SR.GetString(SR.HelpEnableDataBindingFormat, CommandProcessorOptions.Switches.EnableDataBinding.Abbreviation)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.Internal.Name,                                                            SR.GetString(SR.HelpInternalFormat, CommandProcessorOptions.Switches.Internal.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Reference.Name,         SR.ParametersReference,                           SR.GetString(SR.HelpReferenceCodeGenerationFormat, CommandProcessorOptions.Switches.Reference.Abbreviation), true),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.NoTypeReuse.Name,                                                         SR.GetString(SR.HelpNoTypeReuseFormat, CommandProcessorOptions.Switches.NoTypeReuse.Abbreviation, CommandProcessorOptions.Switches.Reference.Name, CommandProcessorOptions.Switches.CollectionType.Name)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.CollectionType.Name,    SR.ParametersCollectionType,                      SR.GetString(SR.HelpCollectionTypeFormat, CommandProcessorOptions.Switches.CollectionType.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.ExcludeType.Name,       SR.ParametersExcludeType,                         SR.GetString(SR.HelpExcludeTypeCodeGenerationFormat, CommandProcessorOptions.Switches.ExcludeType.Abbreviation)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.NoStdlib.Name,                                                            SR.GetString(SR.HelpNostdlibFormat, CommandProcessorOptions.Switches.NoStdlib.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Serializer.Name,        SerializerMode.Auto.ToString(),                   SR.GetString(SR.HelpAutoSerializerFormat, CommandProcessorOptions.Switches.Serializer.Abbreviation), true),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Serializer.Name,        SerializerMode.DataContractSerializer.ToString(), SR.GetString(SR.HelpDataContractSerializer)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Serializer.Name,        SerializerMode.XmlSerializer.ToString(),          SR.GetString(SR.HelpXmlSerializer)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.Sync.Name,                                                                SR.GetString(SR.HelpSyncFormat, CommandProcessorOptions.Switches.Sync.Abbreviation)),
                    ArgumentInfo.CreateFlagHelpInfo(     CommandProcessorOptions.Switches.Wrapped.Name,                                                             SR.GetString(SR.HelpWrappedFormat, CommandProcessorOptions.Switches.Wrapped.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.Update.Name,            SR.ParametersWebServiceReferenceName,             SR.GetString(SR.HelpUpdateWebServiceReferenceFormat, CommandProcessorOptions.Switches.Update.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.RuntimeIdentifier.Name, SR.ParametersRuntimeIdentifier,                   SR.GetString(SR.HelpRuntimeIdentifierFormat, CommandProcessorOptions.Switches.RuntimeIdentifier.Abbreviation)),
                    ArgumentInfo.CreateParameterHelpInfo(CommandProcessorOptions.Switches.TargetFramework.Name,   SR.ParametersTargetFramework,                     SR.GetString(SR.HelpTargetFrameworkFormat, CommandProcessorOptions.Switches.TargetFramework.Abbreviation)),
                }
            };

            helpCategory.WriteHelp();
        }

        private static void WriteExamples()
        {
            HelpCategory helpCategory = new HelpCategory(SR.GetString(SR.HelpExamples));
            helpCategory.WriteHelp();

            WriteExample(SR.GetString(SR.HelpExamples2), SR.GetString(SR.HelpExamples3));
            WriteExample(SR.GetString(SR.HelpExamples8), SR.GetString(SR.HelpExamples9));
        }

        private static void WriteExample(string syntax, string explanation)
        {
            ParagraphHelper paragraphHelper = new ParagraphHelper();
            s_helpBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, " {0}", syntax));
            s_helpBuilder.AppendLine(paragraphHelper.AddIndentation(string.Format(CultureInfo.InvariantCulture, "    {0}", explanation), indentLength: 4));
            s_helpBuilder.AppendLine();
        }

        private class ArgumentInfo
        {
            private const string argHelpPrefix = " ";
            private const string argHelpSeperator = " - ";

            internal static ArgumentInfo CreateInputHelpInfo(string input, string helpText, bool beginGroup = false)
            {
                return new ArgumentInfo()
                {
                    _name = input,
                    _helpText = helpText,
                    _beginGroup = beginGroup
                };
            }

            internal static ArgumentInfo CreateFlagHelpInfo(string option, string helpText, bool beginGroup = false)
            {
                return new ArgumentInfo()
                {
                    _name = String.Format(CultureInfo.InvariantCulture, "{0}{1}", CommandSwitch.FullSwitchIndicator, option),
                    _helpText = helpText,
                    _beginGroup = beginGroup
                };
            }

            internal static ArgumentInfo CreateParameterHelpInfo(string option, string optionUse, string helpText, bool beginGroup = false)
            {
                return new ArgumentInfo()
                {
                    _name = String.Format(CultureInfo.InvariantCulture, "{0}{1} {2}", CommandSwitch.FullSwitchIndicator, option, optionUse),
                    _helpText = helpText,
                    _beginGroup = beginGroup
                };
            }

            private bool _beginGroup;
            private string _name;
            private string _helpText;

            public bool BeginGroup
            {
                get { return _beginGroup; }
                set { _beginGroup = value; }
            }

            public string Name
            {
                get { return _name; }
            }
            public string HelpText
            {
                set { _helpText = value; }
            }

            private string GenerateHelp(string pattern)
            {
                return string.Format(CultureInfo.InvariantCulture, pattern, _name, _helpText);
            }

            private static int CalculateMaxNameLength(ArgumentInfo[] arguments)
            {
                int maxNameLength = 0;
                foreach (ArgumentInfo argument in arguments)
                {
                    if (argument.Name.Length > maxNameLength)
                    {
                        maxNameLength = argument.Name.Length;
                    }
                }
                return maxNameLength;
            }

            public static void WriteArguments(ArgumentInfo[] arguments)
            {
                int maxArgumentnLength = CalculateMaxNameLength(arguments);
                int helpTextIndent = argHelpPrefix.Length + maxArgumentnLength + argHelpSeperator.Length;
                string helpPattern = argHelpPrefix + "{0, -" + maxArgumentnLength + "}" + argHelpSeperator + "{1}";

                ParagraphHelper paragraphHelper = new ParagraphHelper();

                foreach (ArgumentInfo argument in arguments)
                {
                    if (argument.BeginGroup)
                        s_helpBuilder.AppendLine();

                    string optionHelp = argument.GenerateHelp(helpPattern);
                    s_helpBuilder.AppendLine(paragraphHelper.AddIndentation(optionHelp, helpTextIndent));
                }
            }
        }

        private class HelpCategory
        {
            static HelpCategory()
            {
                try
                {
                    if (Console.WindowWidth > 75)
                        s_nameMidpoint = Console.WindowWidth / 3;
                    else
                        s_nameMidpoint = 25;
                }
                catch
                {
                    s_nameMidpoint = 25;
                }
            }

            private static int s_nameMidpoint;

            private int _nameStart;
            private string _name;
            private string _description = null;
            private string _syntax = null;
            private ArgumentInfo[] _options;
            private ArgumentInfo[] _inputs;

            public HelpCategory(string name)
            {
                Debug.Assert(!string.IsNullOrEmpty(name), "Help category name should have a valid value!");
                if (name == null)
                {
                    name = string.Empty;
                }
                _name = name;
                _nameStart = s_nameMidpoint - (name.Length / 2);
            }

            public ArgumentInfo[] Options
            {
                get { return _options; }
                set { _options = value; }
            }

            public ArgumentInfo[] Inputs
            {
                get { return _inputs; }
                set { _inputs = value; }
            }

            public void WriteHelp()
            {
                s_helpBuilder.AppendLine(new string(' ', _nameStart) + _name);
                s_helpBuilder.AppendLine();

                if (_inputs != null)
                {
                    ArgumentInfo.WriteArguments(_inputs);
                    s_helpBuilder.AppendLine();
                }

                if (_options != null)
                {
                    s_helpBuilder.AppendLine(SR.GetString(SR.HelpOptions));
                    s_helpBuilder.AppendLine();
                    ArgumentInfo.WriteArguments(_options);
                    s_helpBuilder.AppendLine();
                }
            }
        }
    }

    // Helper class to insert whitespace into a string so multiple lines will line up correctly in the console window.
    internal class ParagraphHelper
    {
        private int _indentLength;
        private int _cursorLeft;
        private int _lineWidth;
        private StringBuilder _stringBuilder;

        public string AddIndentation(string text, int indentLength)
        {
            _indentLength = indentLength;
            this.Reset();
            this.AppendParagraph(text);
            return _stringBuilder.ToString();
        }

        private void Reset()
        {
            _stringBuilder = new StringBuilder();
            _cursorLeft = GetConsoleCursorLeft();
            _lineWidth = GetBufferWidth();
        }

        private void AppendParagraph(string text)
        {
            int index = 0;
            while (index < text.Length)
            {
                this.AppendWord(text, ref index);
                this.AppendWhitespace(text, ref index);
            }
        }

        private void AppendWord(string text, ref int index)
        {
            // If we're at the beginning of a new line we should indent.
            if ((_cursorLeft == 0) && (index != 0))
                AppendIndent();

            int wordLength = FindWordLength(text, index);

            // Now that we know how long the string is we can:
            //   1. print it on the current line if we have enough space
            //   2. print it on the next line if we don't have space 
            //      on the current line and it will fit on the next line
            //   3. print whatever will fit on the current line 
            //      and overflow to the next line.
            if (wordLength < this.HangingLineWidth)
            {
                if (wordLength > this.BufferWidth)
                {
                    this.AppendLineBreak();
                    this.AppendIndent();
                }
                _stringBuilder.Append(text, index, wordLength);
                _cursorLeft += wordLength;
            }
            else
            {
                AppendWithOverflow(text, ref index, ref wordLength);
            }

            index += wordLength;
        }

        private void AppendWithOverflow(string test, ref int start, ref int wordLength)
        {
            do
            {
                _stringBuilder.Append(test, start, this.BufferWidth);
                start += this.BufferWidth;
                wordLength -= this.BufferWidth;
                this.AppendLineBreak();

                if (wordLength > 0)
                    this.AppendIndent();
            } while (wordLength > this.BufferWidth);

            if (wordLength > 0)
            {
                _stringBuilder.Append(test, start, wordLength);
                _cursorLeft += wordLength;
            }
        }

        private void AppendWhitespace(string text, ref int index)
        {
            while ((index < text.Length) && char.IsWhiteSpace(text[index]))
            {
                if (BufferWidth == 0)
                {
                    this.AppendLineBreak();
                }

                // For each whitespace character:
                //   1. If we're at a newline character we insert 
                //      a new line and reset the cursor.
                //   2. If the whitespace character is at the beginning of a new 
                //      line, we insert an indent instead of the whitespace
                //   3. Insert the whitespace 
                if (AtNewLine(text, index))
                {
                    this.AppendLineBreak();
                    index += Environment.NewLine.Length;
                }
                else if (_cursorLeft == 0 && index != 0)
                {
                    AppendIndent();
                    index++;
                }
                else
                {
                    _stringBuilder.Append(text[index]);
                    index++;
                    _cursorLeft++;
                }
            }
        }

        private void AppendIndent()
        {
            _stringBuilder.Append(' ', _indentLength);
            _cursorLeft += _indentLength;
        }

        private void AppendLineBreak()
        {
            if (BufferWidth != 0)
                _stringBuilder.AppendLine();
            _cursorLeft = 0;
        }

        private int BufferWidth
        {
            get
            {
                return _lineWidth - _cursorLeft;
            }
        }

        private int HangingLineWidth
        {
            get
            {
                return _lineWidth - _indentLength;
            }
        }

        private static int FindWordLength(string text, int index)
        {
            for (int end = index; end < text.Length; end++)
            {
                if (char.IsWhiteSpace(text[end]))
                    return end - index;
            }
            return text.Length - index;
        }

        private static bool AtNewLine(string text, int index)
        {
            if ((index + Environment.NewLine.Length) > text.Length)
            {
                return false;
            }

            for (int i = 0; i < Environment.NewLine.Length; i++)
            {
                if (Environment.NewLine[i] != text[index + i])
                {
                    return false;
                }
            }

            return true;
        }

        private static int GetConsoleCursorLeft()
        {
            try
            {
                return Console.CursorLeft;
            }
            catch
            {
                return 0;
            }
        }

        private static int GetBufferWidth()
        {
            try
            {
                int bufferWidth = Console.BufferWidth;

                if (bufferWidth > 0)
                {
                    return Console.BufferWidth;
                }
                else
                {
                    return int.MaxValue;
                }
            }
            catch
            {
                return int.MaxValue;
            }
        }
    }
}
