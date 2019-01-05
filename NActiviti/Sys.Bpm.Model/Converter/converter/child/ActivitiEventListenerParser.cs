﻿/* Licensed under the Apache License, Version 2.0 (the "License");
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
    using org.activiti.bpmn.constants;
    using org.activiti.bpmn.converter.util;
    using org.activiti.bpmn.model;

    /// 
    public class ActivitiEventListenerParser : BaseChildElementParser
    {
        public override void parseChildElement(XMLStreamReader xtr, BaseElement parentElement, BpmnModel model)
        {
            EventListener listener = new EventListener();
            BpmnXMLUtil.addXMLLocation(listener, xtr);
            if (!string.IsNullOrWhiteSpace(xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_CLASS)))
            {
                listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_CLASS);
                listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_CLASS;
            }
            else if (!string.IsNullOrWhiteSpace(xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_DELEGATEEXPRESSION)))
            {
                listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_DELEGATEEXPRESSION);
                listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_DELEGATEEXPRESSION;
            }
            else if (!string.IsNullOrWhiteSpace(xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE)))
            {
                string eventType = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE);
                if (BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_SIGNAL.Equals(eventType))
                {
                    listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_THROW_SIGNAL_EVENT;
                    listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_SIGNAL_EVENT_NAME);
                }
                else if (BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_GLOBAL_SIGNAL.Equals(eventType))
                {
                    listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_THROW_GLOBAL_SIGNAL_EVENT;
                    listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_SIGNAL_EVENT_NAME);
                }
                else if (BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_MESSAGE.Equals(eventType))
                {
                    listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_THROW_MESSAGE_EVENT;
                    listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_MESSAGE_EVENT_NAME);
                }
                else if (BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_EVENT_TYPE_ERROR.Equals(eventType))
                {
                    listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_THROW_ERROR_EVENT;
                    listener.Implementation = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_THROW_ERROR_EVENT_CODE);
                }
                else
                {
                    listener.ImplementationType = ImplementationType.IMPLEMENTATION_TYPE_INVALID_THROW_EVENT;
                }
            }
            listener.Events = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_EVENTS);
            listener.EntityType = xtr.getAttributeValue(BpmnXMLConstants.ATTRIBUTE_LISTENER_ENTITY_TYPE);

            Process parentProcess = (Process)parentElement;
            parentProcess.EventListeners.Add(listener);
        }

        public override string ElementName
        {
            get
            {
                return BpmnXMLConstants.ELEMENT_EVENT_LISTENER;
            }
        }
    }

}