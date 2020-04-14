// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace System.ServiceModel
{
    using System.Collections.Generic;
    using System.ServiceModel.Dispatcher;
    using System.ServiceModel.Description;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.ServiceModel.Channels;

    [AttributeUsage(ServiceModelAttributeTargets.OperationBehavior)]
    public sealed class TransactionFlowAttribute : Attribute, IOperationBehavior
    {
        private TransactionFlowOption _transactions;

        public TransactionFlowAttribute(TransactionFlowOption transactions)
        {
            TransactionFlowBindingElement.ValidateOption(transactions);
            _transactions = transactions;
        }


        public TransactionFlowOption Transactions
        {
            get
            {
                return _transactions;
            }
        }

        internal static void OverrideFlow(BindingParameterCollection parameters, string action,
                                          MessageDirection direction, TransactionFlowOption option)
        {
            Dictionary<DirectionalAction, TransactionFlowOption> dictionary = EnsureDictionary(parameters);
            DirectionalAction da = new DirectionalAction(direction, action);

            if (dictionary.ContainsKey(da))
            {
                dictionary[da] = option;
            }
            else
            {
                dictionary.Add(da, option);
            }
        }

        private static Dictionary<DirectionalAction, TransactionFlowOption> EnsureDictionary(BindingParameterCollection parameters)
        {
            Dictionary<DirectionalAction, TransactionFlowOption> dictionary =
                parameters.Find<Dictionary<DirectionalAction, TransactionFlowOption>>();
            if (dictionary == null)
            {
                dictionary = new Dictionary<DirectionalAction, TransactionFlowOption>();
                parameters.Add(dictionary);
            }
            return dictionary;
        }

        private void ApplyBehavior(OperationDescription description, BindingParameterCollection parameters)
        {
            Dictionary<DirectionalAction, TransactionFlowOption> dictionary = EnsureDictionary(parameters);
            dictionary[new DirectionalAction(description.Messages[0].Direction, description.Messages[0].Action)] = _transactions;
        }
        void IOperationBehavior.Validate(OperationDescription description)
        {
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
        }

        void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
            if (parameters == null)
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
            ApplyBehavior(description, parameters);
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
        {
        }
    }
}
