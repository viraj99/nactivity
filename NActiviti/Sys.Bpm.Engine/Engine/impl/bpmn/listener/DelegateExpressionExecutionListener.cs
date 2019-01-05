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
namespace org.activiti.engine.impl.bpmn.listener
{

    using org.activiti.engine.@delegate;
    using org.activiti.engine.impl.bpmn.helper;
    using org.activiti.engine.impl.bpmn.parser;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.@delegate.invocation;
    using org.activiti.engine.impl.persistence.entity;

    /// 
    [Serializable]
    public class DelegateExpressionExecutionListener : IExecutionListener
    {

        protected internal IExpression expression;
        private readonly IList<FieldDeclaration> fieldDeclarations;

        public DelegateExpressionExecutionListener(IExpression expression, IList<FieldDeclaration> fieldDeclarations)
        {
            this.expression = expression;
            this.fieldDeclarations = fieldDeclarations;
        }

        public virtual void notify(IExecutionEntity execution)
        {
            object @delegate = DelegateExpressionUtil.resolveDelegateExpression(expression, execution, fieldDeclarations);
            if (@delegate is IExecutionListener)
            {
                Context.ProcessEngineConfiguration.DelegateInterceptor.handleInvocation(new ExecutionListenerInvocation((IExecutionListener)@delegate, execution));
            }
            else if (@delegate is IJavaDelegate)
            {
                Context.ProcessEngineConfiguration.DelegateInterceptor.handleInvocation(new JavaDelegateInvocation((IJavaDelegate)@delegate, execution));
            }
            else
            {
                throw new ActivitiIllegalArgumentException("Delegate expression " + expression + " did not resolve to an implementation of " + typeof(IExecutionListener) + " nor " + typeof(IJavaDelegate));
            }
        }

        /// <summary>
        /// returns the expression text for this execution listener. Comes in handy if you want to check which listeners you already have.
        /// </summary>
        public virtual string ExpressionText
        {
            get
            {
                return expression.ExpressionText;
            }
        }

    }

}