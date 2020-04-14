// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Xml;
using System.Collections;

namespace Microsoft.Xml.Schema
{
    using System;
    using Microsoft.Xml;


    /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo"]/*' />
    public interface IXmlSchemaInfo
    {
        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.Validity"]/*' />
        XmlSchemaValidity Validity { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.IsDefault"]/*' />
        bool IsDefault { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.IsNil"]/*' />
        bool IsNil { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.MemberType"]/*' />
        XmlSchemaSimpleType MemberType { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.SchemaType"]/*' />
        XmlSchemaType SchemaType { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.SchemaElement"]/*' />
        XmlSchemaElement SchemaElement { get; }

        /// <include file='doc\IXmlSchemaInfo.uex' path='docs/doc[@for="IXmlSchemaInfo.SchemaAttribute"]/*' />
        XmlSchemaAttribute SchemaAttribute { get; }
    }
}
