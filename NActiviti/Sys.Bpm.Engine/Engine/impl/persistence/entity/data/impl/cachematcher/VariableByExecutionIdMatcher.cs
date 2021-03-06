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
using System;
using System.Collections.Generic;

namespace Sys.Workflow.Engine.Impl.Persistence.Entity.Data.Impl.Cachematcher
{

    /// 
    public class VariableByExecutionIdMatcher : CachedEntityMatcherAdapter<IVariableInstanceEntity>
    {

        public override bool IsRetained(IVariableInstanceEntity variableInstanceEntity, object parameter)
        {
            if (variableInstanceEntity is null || variableInstanceEntity.ExecutionId is null || parameter is null)
            {
                return false;
            }

            string value;
            if (parameter is KeyValuePair<string, object> p)
            {
                value = p.Value?.ToString();
            }
            else
            {
                value = parameter.ToString();
            }

            return variableInstanceEntity.ExecutionId.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}