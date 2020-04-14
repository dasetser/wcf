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
    ///       Represents a class field member.
    ///    </para>
    /// </devdoc>
    [
        //  ClassInterface(ClassInterfaceType.AutoDispatch),
        ComVisible(true),
    // Serializable,
    ]
    public class CodeMemberField : CodeTypeMember
    {
        private CodeTypeReference _type;
        private CodeExpression _initExpression;

        /// <devdoc>
        ///    <para>
        ///       Initializes a new <see cref='Microsoft.CodeDom.CodeMemberField'/>.
        ///    </para>
        /// </devdoc>
        public CodeMemberField()
        {
        }

        /// <devdoc>
        ///    <para>
        ///       Initializes a new <see cref='Microsoft.CodeDom.CodeMemberField'/> with the specified member field type and
        ///       name.
        ///    </para>
        /// </devdoc>
        public CodeMemberField(CodeTypeReference type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public CodeMemberField(string type, string name)
        {
            Type = new CodeTypeReference(type);
            Name = name;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public CodeMemberField(Type type, string name)
        {
            Type = new CodeTypeReference(type);
            Name = name;
        }

        /// <devdoc>
        ///    <para>
        ///       Gets or sets the member field type.
        ///    </para>
        /// </devdoc>
        public CodeTypeReference Type
        {
            get
            {
                if (_type == null)
                {
                    _type = new CodeTypeReference("");
                }
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <devdoc>
        ///    <para>
        ///       Gets or sets the initialization expression for the member field.
        ///    </para>
        /// </devdoc>
        public CodeExpression InitExpression
        {
            get
            {
                return _initExpression;
            }
            set
            {
                _initExpression = value;
            }
        }
    }
}
