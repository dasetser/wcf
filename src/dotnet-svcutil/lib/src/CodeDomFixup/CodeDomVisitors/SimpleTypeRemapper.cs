// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Microsoft.CodeDom;

namespace Microsoft.Tools.ServiceModel.Svcutil
{
    internal class SimpleTypeRemapper : CodeDomVisitor
    {
        protected Type srcType;
        protected string destType;

        public SimpleTypeRemapper(Type srcType, Type destType) : this(srcType, destType.FullName) { }
        public SimpleTypeRemapper(Type srcType, string destType)
        {
            this.srcType = srcType;
            this.destType = destType;
        }

        protected override void Visit(CodeTypeReference typeref)
        {
            base.Visit(typeref);

            if (Match(typeref))
            {
                Map(typeref);
            }
        }

        protected virtual bool Match(CodeTypeReference typeref)
        {
            return CodeDomHelpers.MatchBaseType(typeref, this.srcType);
        }

        protected virtual void Map(CodeTypeReference typeref)
        {
            typeref.BaseType = destType;
        }
    }
}
