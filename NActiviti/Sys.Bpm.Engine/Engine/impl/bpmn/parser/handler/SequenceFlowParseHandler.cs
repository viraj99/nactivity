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
namespace Sys.Workflow.Engine.Impl.Bpmn.Parser.Handlers
{
    using Sys.Workflow.Bpmn.Models;

    /// 
    public class SequenceFlowParseHandler : AbstractBpmnParseHandler<SequenceFlow>
    {

        public const string PROPERTYNAME_CONDITION = "condition";
        public const string PROPERTYNAME_CONDITION_TEXT = "conditionText";

        protected internal override Type HandledType
        {
            get
            {
                return typeof(SequenceFlow);
            }
        }

        protected internal override void ExecuteParse(BpmnParse bpmnParse, SequenceFlow sequenceFlow)
        {
            Process process = bpmnParse.CurrentProcess;
            sequenceFlow.SourceFlowElement = process.GetFlowElement(sequenceFlow.SourceRef, true);
            sequenceFlow.TargetFlowElement = process.GetFlowElement(sequenceFlow.TargetRef, true);
        }

    }

}