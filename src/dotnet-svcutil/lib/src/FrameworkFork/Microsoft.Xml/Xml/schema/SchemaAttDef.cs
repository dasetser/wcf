// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Schema
{
    using System;
    using Microsoft.Xml;


    using System.Diagnostics;
    using System.Collections.Generic;

    /*
     * This class describes an attribute type and potential values.
     * This encapsulates the information for one Attdef * in an
     * Attlist in a DTD as described below:
     */
    internal sealed class SchemaAttDef : SchemaDeclBase, IDtdDefaultAttributeInfo
    {
        internal enum Reserve
        {
            None,
            XmlSpace,
            XmlLang
        };

        private String _defExpanded;  // default value in its expanded form

        private int _lineNum;
        private int _linePos;
        private int _valueLineNum;
        private int _valueLinePos;

        private Reserve _reserved = Reserve.None; // indicate the attribute type, such as xml:lang or xml:space   

#if SILVERLIGHT
        XmlTokenizedType tokenizedType;
#endif

#if !SILVERLIGHT
        private bool _defaultValueChecked = false;
        private bool _hasEntityRef;  // whether there is any entity reference in the default value
        private XmlSchemaAttribute _schemaAttribute;

        public static readonly SchemaAttDef Empty = new SchemaAttDef();
#endif

        //
        // Constructors
        //
        public SchemaAttDef(XmlQualifiedName name, String prefix)
            : base(name, prefix)
        {
        }

#if !SILVERLIGHT
        public SchemaAttDef(XmlQualifiedName name) : base(name, null)
        {
        }
        private SchemaAttDef() { }
#endif

        //
        // IDtdAttributeInfo interface
        //
        #region IDtdAttributeInfo Members
        string IDtdAttributeInfo.Prefix
        {
            get { return ((SchemaAttDef)this).Prefix; }
        }

        string IDtdAttributeInfo.LocalName
        {
            get { return ((SchemaAttDef)this).Name.Name; }
        }

        int IDtdAttributeInfo.LineNumber
        {
            get { return ((SchemaAttDef)this).LineNumber; }
        }

        int IDtdAttributeInfo.LinePosition
        {
            get { return ((SchemaAttDef)this).LinePosition; }
        }

        bool IDtdAttributeInfo.IsNonCDataType
        {
            get { return this.TokenizedType != XmlTokenizedType.CDATA; }
        }

        bool IDtdAttributeInfo.IsDeclaredInExternal
        {
            get { return ((SchemaAttDef)this).IsDeclaredInExternal; }
        }

        bool IDtdAttributeInfo.IsXmlAttribute
        {
            get { return this.Reserved != SchemaAttDef.Reserve.None; }
        }

        #endregion

        //
        // IDtdDefaultAttributeInfo interface
        //
        #region IDtdDefaultAttributeInfo Members
        string IDtdDefaultAttributeInfo.DefaultValueExpanded
        {
            get { return ((SchemaAttDef)this).DefaultValueExpanded; }
        }

        object IDtdDefaultAttributeInfo.DefaultValueTyped
        {
#if SILVERLIGHT
            get { return null; }
#else
            get { return ((SchemaAttDef)this).DefaultValueTyped; }
#endif
        }

        int IDtdDefaultAttributeInfo.ValueLineNumber
        {
            get { return ((SchemaAttDef)this).ValueLineNumber; }
        }

        int IDtdDefaultAttributeInfo.ValueLinePosition
        {
            get { return ((SchemaAttDef)this).ValueLinePosition; }
        }
        #endregion

        //
        // Internal properties
        //
        internal int LinePosition
        {
            get { return _linePos; }
            set { _linePos = value; }
        }

        internal int LineNumber
        {
            get { return _lineNum; }
            set { _lineNum = value; }
        }

        internal int ValueLinePosition
        {
            get { return _valueLinePos; }
            set { _valueLinePos = value; }
        }

        internal int ValueLineNumber
        {
            get { return _valueLineNum; }
            set { _valueLineNum = value; }
        }

        internal String DefaultValueExpanded
        {
            get { return (_defExpanded != null) ? _defExpanded : String.Empty; }
            set { _defExpanded = value; }
        }

        internal XmlTokenizedType TokenizedType
        {
            get
            {
#if SILVERLIGHT
                return tokenizedType;
#else
                return Datatype.TokenizedType;
#endif
            }
            set
            {
#if SILVERLIGHT
                tokenizedType = value;
#else
                this.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(value);
#endif
            }
        }

        internal Reserve Reserved
        {
            get { return _reserved; }
            set { _reserved = value; }
        }

#if !SILVERLIGHT
        internal bool DefaultValueChecked
        {
            get
            {
                return _defaultValueChecked;
            }
        }

        internal bool HasEntityRef
        {
            get { return _hasEntityRef; }
            set { _hasEntityRef = value; }
        }

        internal XmlSchemaAttribute SchemaAttribute
        {
            get { return _schemaAttribute; }
            set { _schemaAttribute = value; }
        }

        internal void CheckXmlSpace(IValidationEventHandling validationEventHandling)
        {
            if (datatype.TokenizedType == XmlTokenizedType.ENUMERATION &&
                (values != null) &&
                (values.Count <= 2))
            {
                String s1 = values[0].ToString();

                if (values.Count == 2)
                {
                    String s2 = values[1].ToString();

                    if ((s1 == "default" || s2 == "default") &&
                        (s1 == "preserve" || s2 == "preserve"))
                    {
                        return;
                    }
                }
                else
                {
                    if (s1 == "default" || s1 == "preserve")
                    {
                        return;
                    }
                }
            }
            validationEventHandling.SendEvent(new XmlSchemaException(ResXml.Sch_XmlSpace, string.Empty), XmlSeverityType.Error);
        }

        internal SchemaAttDef Clone()
        {
            return (SchemaAttDef)MemberwiseClone();
        }
#endif
    }
}
