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
using System.Collections.Generic;

namespace org.activiti.engine.impl.persistence.entity.data.impl.cachematcher
{

    /// 
    public class SubProcessInstanceExecutionBySuperExecutionIdMatcher : ISingleCachedEntityMatcher<IExecutionEntity>
    {

        public virtual bool isRetained(IExecutionEntity executionEntity, object parameter)
        {
            if (executionEntity == null || executionEntity.SuperExecutionId == null || parameter == null)
            {
                return false;
            }

            if (parameter is string)
            {
                return executionEntity.SuperExecutionId == parameter.ToString();
            }

            if (parameter is KeyValuePair<string, object> p)
            {
                return executionEntity.SuperExecutionId == p.Value?.ToString();
            }

            return executionEntity.SuperExecutionId == parameter.ToString();
            //return !string.ReferenceEquals(executionEntity.SuperExecutionId, null) && ((string)parameter).Equals(executionEntity.SuperExecutionId);
        }

    }
}