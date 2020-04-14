// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.


namespace Microsoft.Xml
{
    using System;


    // NOTE: Absolute numbering is utilized in DtdParser. -HelenaK
    public enum XmlTokenizedType
    {
        CDATA = 0,
        ID = 1,
        IDREF = 2,
        IDREFS = 3,
        ENTITY = 4,
        ENTITIES = 5,
        NMTOKEN = 6,
        NMTOKENS = 7,
        NOTATION = 8,
        ENUMERATION = 9,
        QName = 10,
        NCName = 11,
        None = 12
    }
}
