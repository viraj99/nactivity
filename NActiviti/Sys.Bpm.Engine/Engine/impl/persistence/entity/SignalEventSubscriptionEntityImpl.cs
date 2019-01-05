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

namespace org.activiti.engine.impl.persistence.entity
{

    using org.activiti.bpmn.model;

    /// 
    /// 
    [Serializable]
    public class SignalEventSubscriptionEntityImpl : EventSubscriptionEntityImpl, ISignalEventSubscriptionEntity
    {

        private const long serialVersionUID = 1L;

        // Using json here, but not worth of adding json dependency lib for this
        private const string CONFIGURATION_TEMPLATE = "'{'\"scope\":\"{0}\"'}'";

        public SignalEventSubscriptionEntityImpl()
        {
            eventType = SignalEventSubscriptionEntity_Fields.EVENT_TYPE;
        }

        public override string Configuration
        {
            set
            {
                if (!string.ReferenceEquals(value, null) && value.Contains("{\"scope\":"))
                {
                    this.configuration = value;
                }
                else
                {
                    this.configuration = string.Format(CONFIGURATION_TEMPLATE, value);
                }
            }
        }

        public virtual bool ProcessInstanceScoped
        {
            get
            {
                string scope = extractScopeFormConfiguration();
                return (!string.ReferenceEquals(scope, null)) && (Signal.SCOPE_PROCESS_INSTANCE.Equals(scope));
            }
        }

        public virtual bool GlobalScoped
        {
            get
            {
                string scope = extractScopeFormConfiguration();
                return (string.ReferenceEquals(scope, null)) || (Signal.SCOPE_GLOBAL.Equals(scope));
            }
        }

        protected internal virtual string extractScopeFormConfiguration()
        {
            if (string.ReferenceEquals(this.configuration, null))
            {
                return null;
            }
            else
            {
                return this.configuration.Substring(10, (this.configuration.Length - 2) - 10); // 10 --> length of {"scope": and -2 for removing"}
            }
        }

    }

}