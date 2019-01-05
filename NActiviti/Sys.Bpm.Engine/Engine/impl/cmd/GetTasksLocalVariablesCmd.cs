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

namespace org.activiti.engine.impl.cmd
{

    using org.activiti.engine.impl.interceptor;
    using org.activiti.engine.impl.persistence.entity;

    /// 
    [Serializable]
    public class GetTasksLocalVariablesCmd : ICommand<IList<IVariableInstance>>
    {


        private const long serialVersionUID = 1L;
        protected internal ISet<string> taskIds;

        public GetTasksLocalVariablesCmd(ISet<string> taskIds)
        {
            this.taskIds = taskIds;
        }

        public virtual IList<IVariableInstance> execute(ICommandContext commandContext)
        {
            if (taskIds == null)
            {
                throw new ActivitiIllegalArgumentException("taskIds is null");
            }
            if (taskIds.Count == 0)
            {
                throw new ActivitiIllegalArgumentException("Set of taskIds is empty");
            }

            IList<IVariableInstance> instances = new List<IVariableInstance>();
            IList<IVariableInstanceEntity> entities = commandContext.VariableInstanceEntityManager.findVariableInstancesByTaskIds(taskIds);
            foreach (IVariableInstanceEntity entity in entities)
            {
                instances.Add(entity);
            }

            return instances;
        }

    }
}