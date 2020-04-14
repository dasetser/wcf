// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.Xml.Serialization
{
    using System;
    using Microsoft.Xml;
    internal class XmlSerializationPrimitiveWriter : Microsoft.Xml.Serialization.XmlSerializationWriter
    {
        internal void Write_string(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteNullTagLiteral(@"string", @"");
                return;
            }
            TopLevelElement();
            WriteNullableStringLiteral(@"string", @"", ((System.String)o));
        }

        internal void Write_int(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"int", @"");
                return;
            }
            WriteElementStringRaw(@"int", @"", Microsoft.Xml.XmlConvert.ToString((System.Int32)((System.Int32)o)));
        }

        internal void Write_boolean(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"boolean", @"");
                return;
            }
            WriteElementStringRaw(@"boolean", @"", Microsoft.Xml.XmlConvert.ToString((System.Boolean)((System.Boolean)o)));
        }

        internal void Write_short(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"short", @"");
                return;
            }
            WriteElementStringRaw(@"short", @"", Microsoft.Xml.XmlConvert.ToString((System.Int16)((System.Int16)o)));
        }

        internal void Write_long(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"long", @"");
                return;
            }
            WriteElementStringRaw(@"long", @"", Microsoft.Xml.XmlConvert.ToString((System.Int64)((System.Int64)o)));
        }

        internal void Write_float(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"float", @"");
                return;
            }
            WriteElementStringRaw(@"float", @"", Microsoft.Xml.XmlConvert.ToString((System.Single)((System.Single)o)));
        }

        internal void Write_double(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"double", @"");
                return;
            }
            WriteElementStringRaw(@"double", @"", Microsoft.Xml.XmlConvert.ToString((System.Double)((System.Double)o)));
        }

        internal void Write_decimal(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"decimal", @"");
                return;
            }
            WriteElementStringRaw(@"decimal", @"", Microsoft.Xml.XmlConvert.ToString((System.Decimal)((System.Decimal)o)));
        }

        internal void Write_dateTime(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"dateTime", @"");
                return;
            }
            WriteElementStringRaw(@"dateTime", @"", FromDateTime(((System.DateTime)o)));
        }

        internal void Write_unsignedByte(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"unsignedByte", @"");
                return;
            }
            WriteElementStringRaw(@"unsignedByte", @"", Microsoft.Xml.XmlConvert.ToString((System.Byte)((System.Byte)o)));
        }

        internal void Write_byte(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"byte", @"");
                return;
            }
            WriteElementStringRaw(@"byte", @"", Microsoft.Xml.XmlConvert.ToString((System.SByte)((System.SByte)o)));
        }

        internal void Write_unsignedShort(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"unsignedShort", @"");
                return;
            }
            WriteElementStringRaw(@"unsignedShort", @"", Microsoft.Xml.XmlConvert.ToString((System.UInt16)((System.UInt16)o)));
        }

        internal void Write_unsignedInt(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"unsignedInt", @"");
                return;
            }
            WriteElementStringRaw(@"unsignedInt", @"", Microsoft.Xml.XmlConvert.ToString((System.UInt32)((System.UInt32)o)));
        }

        internal void Write_unsignedLong(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"unsignedLong", @"");
                return;
            }
            WriteElementStringRaw(@"unsignedLong", @"", Microsoft.Xml.XmlConvert.ToString((System.UInt64)((System.UInt64)o)));
        }

        internal void Write_base64Binary(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteNullTagLiteral(@"base64Binary", @"");
                return;
            }
            TopLevelElement();
            WriteNullableStringLiteralRaw(@"base64Binary", @"", FromByteArrayBase64(((System.Byte[])o)));
        }

        internal void Write_guid(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"guid", @"");
                return;
            }
            WriteElementStringRaw(@"guid", @"", Microsoft.Xml.XmlConvert.ToString((System.Guid)((System.Guid)o)));
        }

        internal void Write_char(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteEmptyTag(@"char", @"");
                return;
            }
            WriteElementString(@"char", @"", FromChar(((System.Char)o)));
        }

        internal void Write_QName(object o)
        {
            WriteStartDocument();
            if (o == null)
            {
                WriteNullTagLiteral(@"QName", @"");
                return;
            }
            TopLevelElement();
            WriteNullableQualifiedNameLiteral(@"QName", @"", ((global::Microsoft.Xml.XmlQualifiedName)o));
        }

        protected override void InitCallbacks()
        {
        }
    }

    internal class XmlSerializationPrimitiveReader : Microsoft.Xml.Serialization.XmlSerializationReader
    {
        internal object Read_string()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id1_string && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    if (ReadNull())
                    {
                        o = null;
                    }
                    else
                    {
                        o = Reader.ReadElementString();
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_int()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id3_int && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToInt32(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_boolean()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id4_boolean && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToBoolean(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_short()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id5_short && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToInt16(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_long()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id6_long && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToInt64(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_float()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id7_float && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToSingle(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_double()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id8_double && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToDouble(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_decimal()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id9_decimal && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToDecimal(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_dateTime()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id10_dateTime && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = ToDateTime(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_unsignedByte()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id11_unsignedByte && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToByte(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_byte()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id12_byte && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToSByte(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_unsignedShort()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id13_unsignedShort && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToUInt16(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_unsignedInt()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id14_unsignedInt && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToUInt32(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_unsignedLong()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id15_unsignedLong && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToUInt64(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_base64Binary()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id16_base64Binary && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    if (ReadNull())
                    {
                        o = null;
                    }
                    else
                    {
                        o = ToByteArrayBase64(false);
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_guid()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id17_guid && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = Microsoft.Xml.XmlConvert.ToGuid(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_char()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id18_char && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    {
                        o = ToChar(Reader.ReadElementString());
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        internal object Read_QName()
        {
            object o = null;
            Reader.MoveToContent();
            if (Reader.NodeType == Microsoft.Xml.XmlNodeType.Element)
            {
                if (((object)Reader.LocalName == (object)_id1_QName && (object)Reader.NamespaceURI == (object)_id2_Item))
                {
                    if (ReadNull())
                    {
                        o = null;
                    }
                    else
                    {
                        o = ReadElementQualifiedName();
                    }
                }
                else
                {
                    throw CreateUnknownNodeException();
                }
            }
            else
            {
                UnknownNode(null);
            }
            return (object)o;
        }

        protected override void InitCallbacks()
        {
        }

        private System.String _id4_boolean;
        private System.String _id14_unsignedInt;
        private System.String _id15_unsignedLong;
        private System.String _id7_float;
        private System.String _id10_dateTime;
        private System.String _id6_long;
        private System.String _id9_decimal;
        private System.String _id8_double;
        private System.String _id17_guid;
        private System.String _id2_Item;
        private System.String _id13_unsignedShort;
        private System.String _id18_char;
        private System.String _id3_int;
        private System.String _id12_byte;
        private System.String _id16_base64Binary;
        private System.String _id11_unsignedByte;
        private System.String _id5_short;
        private System.String _id1_string;
        private System.String _id1_QName;

        protected override void InitIDs()
        {
            _id4_boolean = Reader.NameTable.Add(@"boolean");
            _id14_unsignedInt = Reader.NameTable.Add(@"unsignedInt");
            _id15_unsignedLong = Reader.NameTable.Add(@"unsignedLong");
            _id7_float = Reader.NameTable.Add(@"float");
            _id10_dateTime = Reader.NameTable.Add(@"dateTime");
            _id6_long = Reader.NameTable.Add(@"long");
            _id9_decimal = Reader.NameTable.Add(@"decimal");
            _id8_double = Reader.NameTable.Add(@"double");
            _id17_guid = Reader.NameTable.Add(@"guid");
            _id2_Item = Reader.NameTable.Add(@"");
            _id13_unsignedShort = Reader.NameTable.Add(@"unsignedShort");
            _id18_char = Reader.NameTable.Add(@"char");
            _id3_int = Reader.NameTable.Add(@"int");
            _id12_byte = Reader.NameTable.Add(@"byte");
            _id16_base64Binary = Reader.NameTable.Add(@"base64Binary");
            _id11_unsignedByte = Reader.NameTable.Add(@"unsignedByte");
            _id5_short = Reader.NameTable.Add(@"short");
            _id1_string = Reader.NameTable.Add(@"string");
            _id1_QName = Reader.NameTable.Add(@"QName");
        }
    }
}
