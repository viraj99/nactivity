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
namespace org.activiti.engine.impl.persistence.entity.data.impl.cachematcher
{


    /// 
    public class InactiveExecutionsInActivityAndProcInstMatcher : CachedEntityMatcherAdapter<IExecutionEntity>
    {

        public override bool isRetained(IExecutionEntity executionEntity, object parameter)
        {
            IDictionary<string, object> paramMap = (IDictionary<string, object>)parameter ?? new Dictionary<string, object>();
            paramMap.TryGetValue("activityId", out object activityId);
            paramMap.TryGetValue("processInstanceId", out object processInstanceId);

            return executionEntity.ProcessInstanceId != null &&
                string.Compare(executionEntity.ProcessInstanceId, processInstanceId?.ToString(), true) == 0 &&
                !executionEntity.IsActive &&
                executionEntity.ActivityId != null &&
                string.Compare(executionEntity.ActivityId, activityId?.ToString(), true) == 0;
        }

    }
}