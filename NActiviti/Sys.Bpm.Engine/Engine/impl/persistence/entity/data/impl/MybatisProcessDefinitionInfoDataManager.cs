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
namespace org.activiti.engine.impl.persistence.entity.data.impl
{
    using org.activiti.engine.impl.cfg;
    using System.Collections.Generic;

    /// 
    public class MybatisProcessDefinitionInfoDataManager : AbstractDataManager<IProcessDefinitionInfoEntity>, IProcessDefinitionInfoDataManager
    {

        public MybatisProcessDefinitionInfoDataManager(ProcessEngineConfigurationImpl processEngineConfiguration) : base(processEngineConfiguration)
        {
        }

        public override Type ManagedEntityClass
        {
            get
            {
                return typeof(ProcessDefinitionInfoEntityImpl);
            }
        }

        public override IProcessDefinitionInfoEntity create()
        {
            return new ProcessDefinitionInfoEntityImpl();
        }

        public virtual IProcessDefinitionInfoEntity findProcessDefinitionInfoByProcessDefinitionId(string processDefinitionId)
        {
            return DbSqlSession.selectOne<ProcessDefinitionInfoEntityImpl, IProcessDefinitionInfoEntity>("selectProcessDefinitionInfoByProcessDefinitionId", new KeyValuePair<string, object>("processDefinitionId", processDefinitionId));
        }
    }

}