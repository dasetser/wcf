// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace MS.Internal.Xml.XPath
{
    using System;
    using Microsoft.Xml;
    using Microsoft.Xml.XPath;
    using System.Diagnostics;
    using System.Globalization;
    internal class Group : AstNode
    {
        private AstNode _groupNode;

        public Group(AstNode groupNode)
        {
            _groupNode = groupNode;
        }
        public override AstType Type { get { return AstType.Group; } }
        public override XPathResultType ReturnType { get { return XPathResultType.NodeSet; } }

        public AstNode GroupNode { get { return _groupNode; } }
    }
}


