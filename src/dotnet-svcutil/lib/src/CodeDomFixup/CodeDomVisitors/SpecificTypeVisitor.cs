// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.CodeDom;

namespace Microsoft.Tools.ServiceModel.Svcutil
{
    internal abstract class SpecificTypeVisitor : CodeDomVisitor
    {
        private CodeTypeDeclaration _currentType = null;
        protected CodeTypeDeclaration CurrentType
        {
            get { return _currentType; }
        }

        protected override void Visit(CodeTypeDeclaration type)
        {
            base.Visit(type);

            if (IsSpecificType(type))
            {
                EnterSpecificType(type);
            }
            else
            {
                ExitSpecificType();
            }
        }
        protected override void Visit(CodeCompileUnit cu)
        {
            base.Visit(cu);
            ExitSpecificType();
        }

        protected override void Visit(CodeNamespace ns)
        {
            base.Visit(ns);
            ExitSpecificType();
        }

        private void EnterSpecificType(CodeTypeDeclaration type)
        {
            _currentType = type;
            OnEnterSpecificType();
        }
        private void ExitSpecificType()
        {
            OnExitSpecificType();
            _currentType = null;
        }

        protected virtual void OnEnterSpecificType() { }
        protected virtual void OnExitSpecificType() { }

        protected abstract bool IsSpecificType(CodeTypeDeclaration type);
    }
}
