// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.CodeDom
{
    using System.Diagnostics;
    using System;
    using Microsoft.Win32;
    using System.Collections;
    using System.Runtime.InteropServices;

    /// <devdoc>
    ///    <para>
    ///       Represents an
    ///       expression to invoke a method, to be called on a given target.
    ///    </para>
    /// </devdoc>
    [
        //  ClassInterface(ClassInterfaceType.AutoDispatch),
        ComVisible(true),
    // Serializable,
    ]
    public class CodeMethodInvokeExpression : CodeExpression
    {
        private CodeMethodReferenceExpression _method;
        private CodeExpressionCollection _parameters = new CodeExpressionCollection();

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeMethodInvokeExpression'/>.
        ///    </para>
        /// </devdoc>
        public CodeMethodInvokeExpression()
        {
        }

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeMethodInvokeExpression'/> using the specified target object, method name
        ///       and parameters.
        ///    </para>
        /// </devdoc>
        public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
        {
            _method = method;
            Parameters.AddRange(parameters);
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
        {
            _method = new CodeMethodReferenceExpression(targetObject, methodName);
            Parameters.AddRange(parameters);
        }

        /// <devdoc>
        ///    <para>
        ///       Gets or sets the name of the method to invoke.
        ///    </para>
        /// </devdoc>
        public CodeMethodReferenceExpression Method
        {
            get
            {
                if (_method == null)
                {
                    _method = new CodeMethodReferenceExpression();
                }
                return _method;
            }
            set
            {
                _method = value;
            }
        }

        /// <devdoc>
        ///    <para>
        ///       Gets or sets
        ///       the parameters to invoke the method with.
        ///    </para>
        /// </devdoc>
        public CodeExpressionCollection Parameters
        {
            get
            {
                return _parameters;
            }
        }
    }
}
