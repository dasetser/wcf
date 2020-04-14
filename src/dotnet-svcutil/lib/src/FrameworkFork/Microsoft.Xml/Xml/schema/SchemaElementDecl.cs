// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Schema
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Collections.Generic;

    internal sealed class SchemaElementDecl : SchemaDeclBase, IDtdAttributeListInfo
    {
        private Dictionary<XmlQualifiedName, SchemaAttDef> _attdefs = new Dictionary<XmlQualifiedName, SchemaAttDef>();
        private List<IDtdDefaultAttributeInfo> _defaultAttdefs;
        private bool _isIdDeclared;
        private bool _hasNonCDataAttribute = false;

#if !SILVERLIGHT
        private bool _isAbstract = false;
        private bool _isNillable = false;
        private bool _hasRequiredAttribute = false;
        private bool _isNotationDeclared;
        private Dictionary<XmlQualifiedName, XmlQualifiedName> _prohibitedAttributes = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
        private ContentValidator _contentValidator;
        private XmlSchemaAnyAttribute _anyAttribute;
        private XmlSchemaDerivationMethod _block;
        private CompiledIdentityConstraint[] _constraints;
        private XmlSchemaElement _schemaElement;

        internal static readonly SchemaElementDecl Empty = new SchemaElementDecl();
#endif

        //
        // Constructor
        //
#if !SILVERLIGHT
        internal SchemaElementDecl()
        {
        }

        internal SchemaElementDecl(XmlSchemaDatatype dtype)
        {
            Datatype = dtype;
            _contentValidator = ContentValidator.TextOnly;
        }
#endif

        internal SchemaElementDecl(XmlQualifiedName name, String prefix)
        : base(name, prefix)
        {
        }

        //
        // Static methods
        //
#if !SILVERLIGHT
        internal static SchemaElementDecl CreateAnyTypeElementDecl()
        {
            SchemaElementDecl anyTypeElementDecl = new SchemaElementDecl();
            anyTypeElementDecl.Datatype = DatatypeImplementation.AnySimpleType.Datatype;
            return anyTypeElementDecl;
        }
#endif

        //
        // IDtdAttributeListInfo interface
        //
        #region IDtdAttributeListInfo Members

        string IDtdAttributeListInfo.Prefix
        {
            get { return ((SchemaElementDecl)this).Prefix; }
        }

        string IDtdAttributeListInfo.LocalName
        {
            get { return ((SchemaElementDecl)this).Name.Name; }
        }

        bool IDtdAttributeListInfo.HasNonCDataAttributes
        {
            get { return _hasNonCDataAttribute; }
        }

        IDtdAttributeInfo IDtdAttributeListInfo.LookupAttribute(string prefix, string localName)
        {
            XmlQualifiedName qname = new XmlQualifiedName(localName, prefix);
            SchemaAttDef attDef;
            if (_attdefs.TryGetValue(qname, out attDef))
            {
                return attDef;
            }
            return null;
        }

        IEnumerable<IDtdDefaultAttributeInfo> IDtdAttributeListInfo.LookupDefaultAttributes()
        {
            return _defaultAttdefs;
        }

        IDtdAttributeInfo IDtdAttributeListInfo.LookupIdAttribute()
        {
            foreach (SchemaAttDef attDef in _attdefs.Values)
            {
                if (attDef.TokenizedType == XmlTokenizedType.ID)
                {
                    return (IDtdAttributeInfo)attDef;
                }
            }
            return null;
        }
        #endregion

        //
        // SchemaElementDecl properties
        //
        internal bool IsIdDeclared
        {
            get { return _isIdDeclared; }
            set { _isIdDeclared = value; }
        }

        internal bool HasNonCDataAttribute
        {
            get { return _hasNonCDataAttribute; }
            set { _hasNonCDataAttribute = value; }
        }

#if !SILVERLIGHT
        internal SchemaElementDecl Clone()
        {
            return (SchemaElementDecl)MemberwiseClone();
        }

        internal bool IsAbstract
        {
            get { return _isAbstract; }
            set { _isAbstract = value; }
        }

        internal bool IsNillable
        {
            get { return _isNillable; }
            set { _isNillable = value; }
        }

        internal XmlSchemaDerivationMethod Block
        {
            get { return _block; }
            set { _block = value; }
        }

        internal bool IsNotationDeclared
        {
            get { return _isNotationDeclared; }
            set { _isNotationDeclared = value; }
        }

        internal bool HasDefaultAttribute
        {
            get { return _defaultAttdefs != null; }
        }

        internal bool HasRequiredAttribute
        {
            get { return _hasRequiredAttribute; }
            set { _hasRequiredAttribute = value; }
        }

        internal ContentValidator ContentValidator
        {
            get { return _contentValidator; }
            set { _contentValidator = value; }
        }

        internal XmlSchemaAnyAttribute AnyAttribute
        {
            get { return _anyAttribute; }
            set { _anyAttribute = value; }
        }

        internal CompiledIdentityConstraint[] Constraints
        {
            get { return _constraints; }
            set { _constraints = value; }
        }

        internal XmlSchemaElement SchemaElement
        {
            get { return _schemaElement; }
            set { _schemaElement = value; }
        }
#endif
        // add a new SchemaAttDef to the SchemaElementDecl
        internal void AddAttDef(SchemaAttDef attdef)
        {
            _attdefs.Add(attdef.Name, attdef);
#if !SILVERLIGHT
            if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
            {
                _hasRequiredAttribute = true;
            }
#endif
            if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
            { //Not adding RequiredFixed here
                if (_defaultAttdefs == null)
                {
                    _defaultAttdefs = new List<IDtdDefaultAttributeInfo>();
                }
                _defaultAttdefs.Add(attdef);
            }
        }

        /*
         * Retrieves the attribute definition of the named attribute.
         * @param name  The name of the attribute.
         * @return  an attribute definition object; returns null if it is not found.
         */
        internal SchemaAttDef GetAttDef(XmlQualifiedName qname)
        {
            SchemaAttDef attDef;
            if (_attdefs.TryGetValue(qname, out attDef))
            {
                return attDef;
            }
            return null;
        }

        internal IList<IDtdDefaultAttributeInfo> DefaultAttDefs
        {
            get { return _defaultAttdefs; }
        }

#if !SILVERLIGHT
        internal Dictionary<XmlQualifiedName, SchemaAttDef> AttDefs
        {
            get { return _attdefs; }
        }

        internal Dictionary<XmlQualifiedName, XmlQualifiedName> ProhibitedAttributes
        {
            get { return _prohibitedAttributes; }
        }

        internal void CheckAttributes(Hashtable presence, bool standalone)
        {
            foreach (SchemaAttDef attdef in _attdefs.Values)
            {
                if (presence[attdef.Name] == null)
                {
                    if (attdef.Presence == SchemaDeclBase.Use.Required)
                    {
                        throw new XmlSchemaException(ResXml.Sch_MissRequiredAttribute, attdef.Name.ToString());
                    }
                    else if (standalone && attdef.IsDeclaredInExternal && (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed))
                    {
                        throw new XmlSchemaException(ResXml.Sch_StandAlone, string.Empty);
                    }
                }
            }
        }
#endif
    }
}
