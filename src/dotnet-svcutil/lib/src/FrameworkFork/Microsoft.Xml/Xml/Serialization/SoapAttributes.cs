// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Serialization
{
    using System;
    using System.Reflection;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Generic;

    internal enum SoapAttributeFlags
    {
        Enum = 0x1,
        Type = 0x2,
        Element = 0x4,
        Attribute = 0x8,
    }

    /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes"]/*' />
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public class SoapAttributes
    {
        private bool _soapIgnore;
        private SoapTypeAttribute _soapType;
        private SoapElementAttribute _soapElement;
        private SoapAttributeAttribute _soapAttribute;
        private SoapEnumAttribute _soapEnum;
        private object _soapDefaultValue = null;

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapAttributes"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapAttributes()
        {
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapAttributes1"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapAttributes(IEnumerable<Attribute> attributes)
        {
            foreach (var attrib in attributes)
            {
                if (attrib is SoapIgnoreAttribute || attrib is ObsoleteAttribute)
                {
                    _soapIgnore = true;
                    break;
                }
                else if (attrib is SoapElementAttribute)
                {
                    _soapElement = (SoapElementAttribute)attrib;
                }
                else if (attrib is SoapAttributeAttribute)
                {
                    _soapAttribute = (SoapAttributeAttribute)attrib;
                }
                else if (attrib is SoapTypeAttribute)
                {
                    _soapType = (SoapTypeAttribute)attrib;
                }
                else if (attrib is SoapEnumAttribute)
                {
                    _soapEnum = (SoapEnumAttribute)attrib;
                }
                else if (attrib is DefaultValueAttribute)
                {
                    _soapDefaultValue = ((DefaultValueAttribute)attrib).Value;
                }
            }
            if (_soapIgnore)
            {
                _soapElement = null;
                _soapAttribute = null;
                _soapType = null;
                _soapEnum = null;
                _soapDefaultValue = null;
            }
        }

        internal SoapAttributeFlags SoapFlags
        {
            get
            {
                SoapAttributeFlags flags = 0;
                if (_soapElement != null) flags |= SoapAttributeFlags.Element;
                if (_soapAttribute != null) flags |= SoapAttributeFlags.Attribute;
                if (_soapEnum != null) flags |= SoapAttributeFlags.Enum;
                if (_soapType != null) flags |= SoapAttributeFlags.Type;
                return flags;
            }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapType"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapTypeAttribute SoapType
        {
            get { return _soapType; }
            set { _soapType = value; }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapEnum"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapEnumAttribute SoapEnum
        {
            get { return _soapEnum; }
            set { _soapEnum = value; }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapIgnore"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public bool SoapIgnore
        {
            get { return _soapIgnore; }
            set { _soapIgnore = value; }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapElement"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapElementAttribute SoapElement
        {
            get { return _soapElement; }
            set { _soapElement = value; }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapAttribute"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public SoapAttributeAttribute SoapAttribute
        {
            get { return _soapAttribute; }
            set { _soapAttribute = value; }
        }

        /// <include file='doc\SoapAttributes.uex' path='docs/doc[@for="SoapAttributes.SoapDefaultValue"]/*' />
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public object SoapDefaultValue
        {
            get { return _soapDefaultValue; }
            set { _soapDefaultValue = value; }
        }
    }
}
