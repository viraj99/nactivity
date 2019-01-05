﻿using System;
using System.Collections.Generic;

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
namespace org.activiti.engine.impl
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using org.activiti.engine.@delegate.@event;
    using org.activiti.engine.@delegate.@event.impl;
    using org.activiti.engine.impl.asyncexecutor;
    using org.activiti.engine.impl.cfg;
    using org.activiti.engine.impl.interceptor;
    using Sys;

    public class ProcessEngineImpl : IProcessEngine
    {
        protected internal string name;
        protected internal IRepositoryService repositoryService;
        protected internal IRuntimeService runtimeService;
        protected internal IHistoryService historicDataService;
        protected internal ITaskService taskService;
        protected internal IManagementService managementService;
        protected internal IDynamicBpmnService dynamicBpmnService;
        protected internal IAsyncExecutor asyncExecutor;
        protected internal ICommandExecutor commandExecutor;
        protected internal IDictionary<Type, ISessionFactory> sessionFactories;
        protected internal ITransactionContextFactory transactionContextFactory;
        protected internal ProcessEngineConfigurationImpl processEngineConfiguration;

        private ILogger<ProcessEngineImpl> log = ProcessEngineServiceProvider.LoggerService<ProcessEngineImpl>();

        public ProcessEngineImpl(ProcessEngineConfigurationImpl processEngineConfiguration)
        {
            this.processEngineConfiguration = processEngineConfiguration;
            this.name = processEngineConfiguration.ProcessEngineName;
            this.repositoryService = processEngineConfiguration.RepositoryService;
            this.runtimeService = processEngineConfiguration.RuntimeService;
            this.historicDataService = processEngineConfiguration.HistoryService;
            this.taskService = processEngineConfiguration.TaskService;
            this.managementService = processEngineConfiguration.ManagementService;
            this.dynamicBpmnService = processEngineConfiguration.DynamicBpmnService;
            this.asyncExecutor = processEngineConfiguration.AsyncExecutor;
            this.commandExecutor = processEngineConfiguration.CommandExecutor;
            this.sessionFactories = processEngineConfiguration.SessionFactories;
            this.transactionContextFactory = processEngineConfiguration.TransactionContextFactory;

            if (processEngineConfiguration.UsingRelationalDatabase && !string.ReferenceEquals(processEngineConfiguration.DatabaseSchemaUpdate, null))
            {
                commandExecutor.execute(processEngineConfiguration.SchemaCommandConfig, new SchemaOperationsProcessEngineBuild());
            }

            if (string.ReferenceEquals(name, null))
            {
                log.LogInformation("default activiti ProcessEngine created");
            }
            else
            {
                log.LogInformation($"ProcessEngine {name} created");
            }

            ProcessEngineFactory.registerProcessEngine(this);

            if (asyncExecutor != null && asyncExecutor.AutoActivate)
            {
                asyncExecutor.start();
            }

            if (processEngineConfiguration.ProcessEngineLifecycleListener != null)
            {
                processEngineConfiguration.ProcessEngineLifecycleListener.onProcessEngineBuilt(this);
            }

            processEngineConfiguration.EventDispatcher.dispatchEvent(ActivitiEventBuilder.createGlobalEvent(ActivitiEventType.ENGINE_CREATED));
        }

        public virtual void close()
        {
            ProcessEngineFactory.unregister(this);
            if (asyncExecutor != null && asyncExecutor.Active)
            {
                asyncExecutor.shutdown();
            }

            commandExecutor.execute(processEngineConfiguration.SchemaCommandConfig, new SchemaOperationProcessEngineClose());

            if (processEngineConfiguration.ProcessEngineLifecycleListener != null)
            {
                processEngineConfiguration.ProcessEngineLifecycleListener.onProcessEngineClosed(this);
            }

            processEngineConfiguration.EventDispatcher.dispatchEvent(ActivitiEventBuilder.createGlobalEvent(ActivitiEventType.ENGINE_CLOSED));
        }

        // getters and setters
        // //////////////////////////////////////////////////////

        public virtual string Name
        {
            get
            {
                return name;
            }
        }

        public virtual IManagementService ManagementService
        {
            get
            {
                return managementService;
            }
        }

        public virtual ITaskService TaskService
        {
            get
            {
                return taskService;
            }
        }

        public virtual IHistoryService HistoryService
        {
            get
            {
                return historicDataService;
            }
        }

        public virtual IRuntimeService RuntimeService
        {
            get
            {
                return runtimeService;
            }
        }

        public virtual IRepositoryService RepositoryService
        {
            get
            {
                return repositoryService;
            }
        }

        public virtual IDynamicBpmnService DynamicBpmnService
        {
            get
            {
                return dynamicBpmnService;
            }
        }

        public virtual ProcessEngineConfiguration ProcessEngineConfiguration
        {
            get
            {
                return processEngineConfiguration;
            }
        }
    }

}