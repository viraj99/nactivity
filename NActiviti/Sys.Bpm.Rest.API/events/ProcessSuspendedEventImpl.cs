﻿/*
 * Copyright 2018 Alfresco and/or its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
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
 *
 */

namespace Sys.Workflow.Cloud.Services.Events
{
    using Sys.Workflow.Cloud.Services.Api.Model;

    /// <summary>
    /// 
    /// </summary>
    public class ProcessSuspendedEventImpl : AbstractProcessEngineEvent, IProcessSuspendedEvent
    {

        private ProcessInstance processInstance;


        /// <summary>
        /// 
        /// </summary>
        public ProcessSuspendedEventImpl()
        {
        }

        /// <summary>
        /// 
        /// </summary>

        public ProcessSuspendedEventImpl(string appName, string appVersion, string serviceName, string serviceFullName, string serviceType, string serviceVersion, string executionId, string processDefinitionId, string processInstanceId, ProcessInstance processInstance) : base(appName, appVersion, serviceName, serviceFullName, serviceType, serviceVersion, executionId, processDefinitionId, processInstanceId)
        {
            this.processInstance = processInstance;
        }

        /// <summary>
        /// 
        /// </summary>

        public virtual ProcessInstance ProcessInstance
        {
            get
            {
                return processInstance;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override string EventType
        {
            get
            {
                return "ProcessSuspendedEvent";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            return "ProcessSuspendedEventImpl{" +
                        "processInstance=" + processInstance +
                        ", appName='" + AppName + '\'' +
                        ", appVersion='" + AppVersion + '\'' +
                        ", serviceName='" + ServiceName + '\'' +
                        ", serviceFullName='" + ServiceFullName + '\'' +
                        ", serviceType='" + ServiceType + '\'' +
                        ", serviceVersion='" + ServiceVersion + '\'' +
                        ", executionId='" + ExecutionId + '\'' +
                        ", processDefinitionId='" + ProcessDefinitionId + '\'' +
                        ", processInstanceId='" + ProcessInstanceId + '\'' +
                        ", timestamp=" + Timestamp +
                    '}';
        }
    }

}