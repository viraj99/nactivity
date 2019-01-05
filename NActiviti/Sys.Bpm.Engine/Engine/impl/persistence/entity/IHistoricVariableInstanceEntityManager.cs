﻿using System.Collections.Generic;

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

    using org.activiti.engine.history;

    /// 
    public interface IHistoricVariableInstanceEntityManager : IEntityManager<IHistoricVariableInstanceEntity>
    {

        IHistoricVariableInstanceEntity copyAndInsert(IVariableInstanceEntity variableInstance);

        void copyVariableValue(IHistoricVariableInstanceEntity historicVariableInstance, IVariableInstanceEntity variableInstance);

        IList<IHistoricVariableInstance> findHistoricVariableInstancesByQueryCriteria(IHistoricVariableInstanceQuery historicProcessVariableQuery, Page page);

        IHistoricVariableInstanceEntity findHistoricVariableInstanceByVariableInstanceId(string variableInstanceId);

        long findHistoricVariableInstanceCountByQueryCriteria(IHistoricVariableInstanceQuery historicProcessVariableQuery);

        IList<IHistoricVariableInstance> findHistoricVariableInstancesByNativeQuery(IDictionary<string, object> parameterMap, int firstResult, int maxResults);

        long findHistoricVariableInstanceCountByNativeQuery(IDictionary<string, object> parameterMap);

        void deleteHistoricVariableInstancesByTaskId(string taskId);

        void deleteHistoricVariableInstanceByProcessInstanceId(string historicProcessInstanceId);

    }
}