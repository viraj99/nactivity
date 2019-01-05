﻿using System;

/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace org.activiti.bpmn.converter.child
{

    using org.activiti.bpmn.converter.util;
    using org.activiti.bpmn.model;

    /// 
    public class MessageEventDefinitionParser : BaseChildElementParser
    {

        public override string ElementName
        {
            get
            {
                return org.activiti.bpmn.constants.BpmnXMLConstants.ELEMENT_EVENT_MESSAGEDEFINITION;
            }
        }

        public override void parseChildElement(XMLStreamReader xtr, BaseElement parentElement, BpmnModel model)
        {
            if (!(parentElement is Event))
            {
                return;
            }

            MessageEventDefinition eventDefinition = new MessageEventDefinition();
            BpmnXMLUtil.addXMLLocation(eventDefinition, xtr);
            eventDefinition.MessageRef = xtr.getAttributeValue(org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_MESSAGE_REF);
            eventDefinition.MessageExpression = xtr.getAttributeValue(org.activiti.bpmn.constants.BpmnXMLConstants.ACTIVITI_EXTENSIONS_NAMESPACE, org.activiti.bpmn.constants.BpmnXMLConstants.ATTRIBUTE_MESSAGE_EXPRESSION);

            if (!string.IsNullOrWhiteSpace(eventDefinition.MessageRef))
            {

                int indexOfP = eventDefinition.MessageRef.IndexOf(':');
                if (indexOfP != -1)
                {
                    string prefix = eventDefinition.MessageRef.Substring(0, indexOfP);
                    string resolvedNamespace = model.getNamespace(prefix);
                    string messageRef = eventDefinition.MessageRef.Substring(indexOfP + 1);

                    if (string.ReferenceEquals(resolvedNamespace, null))
                    {
                        // if it's an invalid prefix will consider this is not a namespace prefix so will be used as part of the stringReference
                        messageRef = prefix + ":" + messageRef;
                    }
                    else if (!resolvedNamespace.Equals(model.TargetNamespace, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // if it's a valid namespace prefix but it's not the targetNamespace then we'll use it as a valid namespace
                        // (even out editor does not support defining namespaces it is still a valid xml file)
                        messageRef = resolvedNamespace + ":" + messageRef;
                    }
                    eventDefinition.MessageRef = messageRef;
                }
                else
                {
                    eventDefinition.MessageRef = eventDefinition.MessageRef;
                }
            }

            BpmnXMLUtil.parseChildElements(org.activiti.bpmn.constants.BpmnXMLConstants.ELEMENT_EVENT_MESSAGEDEFINITION, eventDefinition, xtr, model);

            ((Event)parentElement).EventDefinitions.Add(eventDefinition);
        }
    }

}