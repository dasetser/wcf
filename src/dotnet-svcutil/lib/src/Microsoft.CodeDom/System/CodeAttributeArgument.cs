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
    ///       Represents an argument for use in a custom attribute declaration.
    ///    </para>
    /// </devdoc>
    [
        //  ClassInterface(ClassInterfaceType.AutoDispatch),
        ComVisible(true),
    // Serializable,
    ]
    public class CodeAttributeArgument
    {
        private string _name;
        private CodeExpression _value;

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeAttributeArgument'/>.
        ///    </para>
        /// </devdoc>
        public CodeAttributeArgument()
        {
        }

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeAttributeArgument'/> using the specified value.
        ///    </para>
        /// </devdoc>
        public CodeAttributeArgument(CodeExpression value)
        {
            Value = value;
        }

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of <see cref='Microsoft.CodeDom.CodeAttributeArgument'/> using the specified name and
        ///       value.
        ///    </para>
        /// </devdoc>
        public CodeAttributeArgument(string name, CodeExpression value)
        {
            Name = name;
            Value = value;
        }

        /// <devdoc>
        ///    <para>
        ///       The name of the attribute.
        ///    </para>
        /// </devdoc>
        public string Name
        {
            get
            {
                return (_name == null) ? string.Empty : _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <devdoc>
        ///    <para>
        ///       The argument for the attribute.
        ///    </para>
        /// </devdoc>
        public CodeExpression Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
