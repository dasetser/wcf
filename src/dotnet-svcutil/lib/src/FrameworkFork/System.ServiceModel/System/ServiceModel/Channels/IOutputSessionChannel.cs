// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace System.ServiceModel.Channels
{
    public interface IOutputSessionChannel
        : IOutputChannel, ISessionChannel<IOutputSession>
    {
    }
}
