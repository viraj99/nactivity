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

namespace org.activiti.engine.impl.db
{
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.persistence;
    using org.activiti.engine.impl.variable;
    using SmartSql.Abstractions.TypeHandler;
    using System;
    using System.Data;
    using System.Data.Common;

    /// 
    public class IbatisVariableTypeHandler : ITypeHandler<IVariableType>, ITypeHandler
    {

        protected internal IVariableTypes variableTypes;

        public virtual IVariableType getResult(DbDataReader rs, string columnName)
        {
            return getResult(rs, rs.GetOrdinal(columnName));
        }

        protected internal virtual IVariableTypes VariableTypes
        {
            get
            {
                if (variableTypes == null)
                {
                    variableTypes = Context.ProcessEngineConfiguration.VariableTypes;
                }
                return variableTypes;
            }
        }

        public virtual IVariableType getResult(DbDataReader rs, int columnIndex)
        {
            string typeName = rs.GetString(columnIndex);
            IVariableType type = VariableTypes.getVariableType(typeName);
            if (type == null)
            {
                throw new ActivitiException("unknown variable type name " + typeName);
            }
            return type;
        }


        public virtual object GetValue(IDataReader dataReader, string columnName, Type targetType)
        {
            int ordinal = dataReader.GetOrdinal(columnName);
            return GetValue(dataReader, ordinal, targetType);
        }

        public virtual object GetValue(IDataReader dataReader, int columnIndex, Type targetType)
        {
            string type = dataReader.GetString(columnIndex);
            if (string.IsNullOrWhiteSpace(type))
            {
                return null;
            }

            return VariableTypes.getVariableType(type);
        }

        public virtual void SetParameter(IDataParameter dataParameter, object parameterValue)
        {
            dataParameter.Value = ToParameterValue(parameterValue);
        }

        public object ToParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            if (value is IVariableType vt)
            {
                return vt.TypeName;
            }

            return value;
        }
    }

}