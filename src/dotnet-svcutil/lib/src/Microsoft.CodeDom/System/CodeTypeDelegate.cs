// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.CodeDom
{
    using System.Diagnostics;
    using System;
    using Microsoft.Win32;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <devdoc>
    ///    <para>
    ///       Represents a class or nested class.
    ///    </para>
    /// </devdoc>
    [
        //  ClassInterface(ClassInterfaceType.AutoDispatch),
        ComVisible(true),
    // Serializable,
    ]
    public class CodeTypeDelegate : CodeTypeDeclaration
    {
        private CodeParameterDeclarationExpressionCollection _parameters = new CodeParameterDeclarationExpressionCollection();
        private CodeTypeReference _returnType;

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeTypeDelegate'/>.
        ///    </para>
        /// </devdoc>
        public CodeTypeDelegate()
        {
            TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
            TypeAttributes |= TypeAttributes.Class;
            BaseTypes.Clear();
            BaseTypes.Add(new CodeTypeReference("System.Delegate"));
        }

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeTypeDelegate'/>.
        ///    </para>
        /// </devdoc>
        public CodeTypeDelegate(string name) : this()
        {
            Name = name;
        }

        /// <devdoc>
        ///    <para>
        ///       Gets or sets the return type of the delegate.
        ///    </para>
        /// </devdoc>
        public CodeTypeReference ReturnType
        {
            get
            {
                if (_returnType == null)
                {
                    _returnType = new CodeTypeReference("");
                }
                return _returnType;
            }
            set
            {
                _returnType = value;
            }
        }

        /// <devdoc>
        ///    <para>
        ///       The parameters of the delegate.
        ///    </para>
        /// </devdoc>
        public CodeParameterDeclarationExpressionCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }
    }
}
