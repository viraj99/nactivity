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
namespace org.activiti.engine.impl.persistence.entity
{
    using Newtonsoft.Json;
    using org.activiti.engine.impl.bpmn.data;
    using org.activiti.engine.impl.context;

    /// 
    /// 
    [Serializable]
    public class ProcessDefinitionEntityImpl : AbstractEntity, IProcessDefinitionEntity
    {

        private const long serialVersionUID = 1L;

        protected internal string name;
        protected internal string description;
        protected internal string key;
        protected internal int version;
        protected internal string category;
        protected internal string deploymentId;
        protected internal string resourceName;
        protected internal string tenantId = ProcessEngineConfiguration.NO_TENANT_ID;
        protected internal int? historyLevel;
        protected internal string diagramResourceName;
        protected internal bool isGraphicalNotationDefined;
        protected internal IDictionary<string, object> variables;
        protected internal bool hasStartFormKey;
        protected internal int suspensionState = SuspensionState_Fields.ACTIVE.StateCode;
        protected internal bool isIdentityLinksInitialized;
        protected internal IList<IIdentityLinkEntity> definitionIdentityLinkEntities = new List<IIdentityLinkEntity>();
        protected internal IOSpecification ioSpecification;

        // Backwards compatibility
        protected internal string engineVersion;

        public override PersistentState PersistentState
        {
            get
            {
                PersistentState persistentState = new PersistentState();
                persistentState["suspensionState"] = this.suspensionState;
                persistentState["category"] = this.category;
                return persistentState;
            }
        }

        // getters and setters
        // //////////////////////////////////////////////////////
        [JsonIgnore]
        public virtual IList<IIdentityLinkEntity> IdentityLinks
        {
            get
            {
                var ctx = Context.CommandContext;
                if (!isIdentityLinksInitialized && ctx != null)
                {
                    definitionIdentityLinkEntities = ctx.IdentityLinkEntityManager.findIdentityLinksByProcessDefinitionId(id);
                    isIdentityLinksInitialized = true;
                }

                return definitionIdentityLinkEntities;
            }
        }

        public virtual string Key
        {
            get
            {
                return key;
            }
            set
            {
                this.key = value;
            }
        }


        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }


        public virtual string Description
        {
            set
            {
                this.description = value;
            }
            get
            {
                return description;
            }
        }


        public virtual string DeploymentId
        {
            get
            {
                return deploymentId;
            }
            set
            {
                this.deploymentId = value;
            }
        }


        public virtual int Version
        {
            get
            {
                return version;
            }
            set
            {
                this.version = value;
            }
        }


        public virtual string ResourceName
        {
            get
            {
                return resourceName;
            }
            set
            {
                this.resourceName = value;
            }
        }


        public virtual string TenantId
        {
            get
            {
                return tenantId;
            }
            set
            {
                this.tenantId = value;
            }
        }


        public virtual int? HistoryLevel
        {
            get
            {
                return historyLevel;
            }
            set
            {
                this.historyLevel = value;
            }
        }


        public virtual IDictionary<string, object> Variables
        {
            get
            {
                return variables;
            }
            set
            {
                this.variables = value;
            }
        }


        public virtual string Category
        {
            get
            {
                return category;
            }
            set
            {
                this.category = value;
            }
        }


        public virtual string DiagramResourceName
        {
            get
            {
                return diagramResourceName;
            }
            set
            {
                this.diagramResourceName = value;
            }
        }

        public virtual bool HasStartFormKey
        {
            get
            {
                return hasStartFormKey;
            }
            set
            {
                this.hasStartFormKey = value;
            }
        }


        public virtual bool IsGraphicalNotationDefined
        {
            get
            {
                return isGraphicalNotationDefined;
            }
            set
            {
                this.isGraphicalNotationDefined = value;
            }
        }


        public virtual int SuspensionState
        {
            get
            {
                return suspensionState;
            }
            set
            {
                this.suspensionState = value;
            }
        }


        public virtual bool Suspended
        {
            get
            {
                return suspensionState == SuspensionState_Fields.SUSPENDED.StateCode;
            }
        }

        public virtual string EngineVersion
        {
            get
            {
                return engineVersion;
            }
            set
            {
                this.engineVersion = value;
            }
        }


        public virtual IOSpecification IoSpecification
        {
            get
            {
                return ioSpecification;
            }
            set
            {
                this.ioSpecification = value;
            }
        }


        public override string ToString()
        {
            return "ProcessDefinitionEntity[" + id + "]";
        }

    }

}