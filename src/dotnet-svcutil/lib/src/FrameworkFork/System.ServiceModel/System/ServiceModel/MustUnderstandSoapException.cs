// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Xml;
using System.Globalization;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
    internal class MustUnderstandSoapException : CommunicationException
    {
        // for serialization
        public MustUnderstandSoapException() { }

        private Collection<MessageHeaderInfo> _notUnderstoodHeaders;
        private EnvelopeVersion _envelopeVersion;

        public MustUnderstandSoapException(Collection<MessageHeaderInfo> notUnderstoodHeaders, EnvelopeVersion envelopeVersion)
        {
            _notUnderstoodHeaders = notUnderstoodHeaders;
            _envelopeVersion = envelopeVersion;
        }

        public Collection<MessageHeaderInfo> NotUnderstoodHeaders { get { return _notUnderstoodHeaders; } }
        public EnvelopeVersion EnvelopeVersion { get { return _envelopeVersion; } }

        internal Message ProvideFault(MessageVersion messageVersion)
        {
            string name = _notUnderstoodHeaders[0].Name;
            string ns = _notUnderstoodHeaders[0].Namespace;
            FaultCode code = new FaultCode(MessageStrings.MustUnderstandFault, _envelopeVersion.Namespace);
            FaultReason reason = new FaultReason(SRServiceModel.Format(SRServiceModel.SFxHeaderNotUnderstood, name, ns), CultureInfo.CurrentCulture);
            MessageFault fault = MessageFault.CreateFault(code, reason);
            string faultAction = messageVersion.Addressing.DefaultFaultAction;
            Message message = System.ServiceModel.Channels.Message.CreateMessage(messageVersion, fault, faultAction);
            if (_envelopeVersion == EnvelopeVersion.Soap12)
            {
                this.AddNotUnderstoodHeaders(message.Headers);
            }
            return message;
        }

        private void AddNotUnderstoodHeaders(MessageHeaders headers)
        {
            for (int i = 0; i < _notUnderstoodHeaders.Count; ++i)
            {
                headers.Add(new NotUnderstoodHeader(_notUnderstoodHeaders[i].Name, _notUnderstoodHeaders[i].Namespace));
            }
        }

        internal class NotUnderstoodHeader : MessageHeader
        {
            private string _notUnderstoodName;
            private string _notUnderstoodNs;

            public NotUnderstoodHeader(string name, string ns)
            {
                _notUnderstoodName = name;
                _notUnderstoodNs = ns;
            }

            public override string Name
            {
                get { return Message12Strings.NotUnderstood; }
            }

            public override string Namespace
            {
                get { return Message12Strings.Namespace; }
            }

            protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
            {
                writer.WriteStartElement(this.Name, this.Namespace);
                writer.WriteXmlnsAttribute(null, _notUnderstoodNs);
                writer.WriteStartAttribute(Message12Strings.QName);
                writer.WriteQualifiedName(_notUnderstoodName, _notUnderstoodNs);
                writer.WriteEndAttribute();
            }

            protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
            {
                // empty
            }
        }
    }
}

