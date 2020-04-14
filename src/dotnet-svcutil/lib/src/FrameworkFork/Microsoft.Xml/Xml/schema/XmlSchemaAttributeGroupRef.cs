// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Schema
{
    using System;
    using Microsoft.Xml;


    using System.Collections;
    using Microsoft.Xml.Serialization;

    /// <include file='doc\XmlSchemaAttributeGroupRef.uex' path='docs/doc[@for="XmlSchemaAttributeGroupRef"]/*' />
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
    {
        private XmlQualifiedName _refName = XmlQualifiedName.Empty;

        /// <include file='doc\XmlSchemaAttributeGroupRef.uex' path='docs/doc[@for="XmlSchemaAttributeGroupRef.RefName"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [XmlAttribute("ref")]
        public XmlQualifiedName RefName
        {
            get { return _refName; }
            set { _refName = (value == null ? XmlQualifiedName.Empty : value); }
        }
    }
}
