// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Serialization
{
    using System.Reflection;
    using System.Collections;
    using System.IO;
    using Microsoft.Xml.Schema;
    using System;
    using System.ComponentModel;

    /// <include file='doc\XmlAttributeOverrides.uex' path='docs/doc[@for="XmlAttributeOverrides"]/*' />
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class XmlAttributeOverrides
    {
        private Hashtable _types = new Hashtable();

        /// <include file='doc\XmlAttributeOverrides.uex' path='docs/doc[@for="XmlAttributeOverrides.Add"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Add(Type type, XmlAttributes attributes)
        {
            Add(type, string.Empty, attributes);
        }

        /// <include file='doc\XmlAttributeOverrides.uex' path='docs/doc[@for="XmlAttributeOverrides.Add1"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public void Add(Type type, string member, XmlAttributes attributes)
        {
            Hashtable members = (Hashtable)_types[type];
            if (members == null)
            {
                members = new Hashtable();
                _types.Add(type, members);
            }
            else if (members[member] != null)
            {
                throw new InvalidOperationException(ResXml.GetString(ResXml.XmlAttributeSetAgain, type.FullName, member));
            }
            members.Add(member, attributes);
        }

        /// <include file='doc\XmlAttributeOverrides.uex' path='docs/doc[@for="XmlAttributeOverrides.this"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlAttributes this[Type type]
        {
            get
            {
                return this[type, string.Empty];
            }
        }

        /// <include file='doc\XmlAttributeOverrides.uex' path='docs/doc[@for="XmlAttributeOverrides.this1"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public XmlAttributes this[Type type, string member]
        {
            get
            {
                Hashtable members = (Hashtable)_types[type];
                if (members == null) return null;
                return (XmlAttributes)members[member];
            }
        }
    }
}

