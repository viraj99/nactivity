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
namespace org.activiti.engine.impl.bpmn.behavior
{
    using Newtonsoft.Json.Linq;
    using org.activiti.engine.@delegate;
    using org.activiti.engine.impl.bpmn.helper;
    using org.activiti.engine.impl.bpmn.parser;
    using org.activiti.engine.impl.context;
    using org.activiti.engine.impl.@delegate;
    using org.activiti.engine.impl.@delegate.invocation;
    using org.activiti.engine.impl.persistence.entity;

    /// <summary>
    /// <seealso cref="IActivityBehavior"/> used when 'delegateExpression' is used for a serviceTask.
    /// 
    /// </summary>
    [Serializable]
    public class ServiceTaskDelegateExpressionActivityBehavior : TaskActivityBehavior
    {

        private const long serialVersionUID = 1L;

        protected internal string serviceTaskId;
        protected internal IExpression expression;
        protected internal IExpression skipExpression;
        private readonly IList<FieldDeclaration> fieldDeclarations;

        public ServiceTaskDelegateExpressionActivityBehavior(string serviceTaskId, IExpression expression, IExpression skipExpression, IList<FieldDeclaration> fieldDeclarations)
        {
            this.serviceTaskId = serviceTaskId;
            this.expression = expression;
            this.skipExpression = skipExpression;
            this.fieldDeclarations = fieldDeclarations;
        }

        public override void trigger(IExecutionEntity execution, string signalName, object signalData)
        {
            object @delegate = DelegateExpressionUtil.resolveDelegateExpression(expression, execution, fieldDeclarations);
            if (@delegate is ITriggerableActivityBehavior)
            {
                ((ITriggerableActivityBehavior)@delegate).trigger(execution, signalName, signalData);
            }
        }

        public override void execute(IExecutionEntity execution)
        {

            try
            {
                bool isSkipExpressionEnabled = SkipExpressionUtil.isSkipExpressionEnabled(execution, skipExpression);
                if (!isSkipExpressionEnabled || (isSkipExpressionEnabled && !SkipExpressionUtil.shouldSkipFlowElement(execution, skipExpression)))
                {

                    if (Context.ProcessEngineConfiguration.EnableProcessDefinitionInfoCache)
                    {
                        JToken taskElementProperties = Context.getBpmnOverrideElementProperties(serviceTaskId, execution.ProcessDefinitionId);
                        if (taskElementProperties != null && taskElementProperties[DynamicBpmnConstants_Fields.SERVICE_TASK_DELEGATE_EXPRESSION] != null)
                        {
                            string overrideExpression = taskElementProperties[DynamicBpmnConstants_Fields.SERVICE_TASK_DELEGATE_EXPRESSION].ToString();
                            if (!string.IsNullOrWhiteSpace(overrideExpression) && !overrideExpression.Equals(expression.ExpressionText))
                            {
                                expression = Context.ProcessEngineConfiguration.ExpressionManager.createExpression(overrideExpression);
                            }
                        }
                    }

                    object @delegate = DelegateExpressionUtil.resolveDelegateExpression(expression, execution, fieldDeclarations);
                    if (@delegate is IActivityBehavior)
                    {

                        if (@delegate is AbstractBpmnActivityBehavior)
                        {
                            ((AbstractBpmnActivityBehavior)@delegate).MultiInstanceActivityBehavior = MultiInstanceActivityBehavior;
                        }

                        Context.ProcessEngineConfiguration.DelegateInterceptor.handleInvocation(new ActivityBehaviorInvocation((IActivityBehavior)@delegate, execution));

                    }
                    else if (@delegate is IJavaDelegate)
                    {
                        Context.ProcessEngineConfiguration.DelegateInterceptor.handleInvocation(new JavaDelegateInvocation((IJavaDelegate)@delegate, execution));
                        leave(execution);

                    }
                    else
                    {
                        throw new ActivitiIllegalArgumentException("Delegate expression " + expression + " did neither resolve to an implementation of " + typeof(IActivityBehavior) + " nor " + typeof(IJavaDelegate));
                    }
                }
                else
                {
                    leave(execution);
                }
            }
            catch (Exception exc)
            {

                Exception cause = exc;
                BpmnError error = null;
                while (cause != null)
                {
                    if (cause is BpmnError)
                    {
                        error = (BpmnError)cause;
                        break;
                    }
                    cause = cause.InnerException;
                }

                if (error != null)
                {
                    ErrorPropagation.propagateError(error, execution);
                }
                else
                {
                    throw new ActivitiException(exc.Message, exc);
                }

            }
        }

    }

}