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
namespace Sys.Workflow.Engine.Repository
{

    /// <summary>
    /// Represents a deployment that is already present in the process repository.
    /// 
    /// A deployment is a container for resources such as process definitions, images, forms, etc.
    /// 
    /// When a deployment is 'deployed' through the <seealso cref="Sys.Workflow.Engine.IRuntimeService"/>, the Activiti engine will recognize certain of such resource types and act upon them (eg process definitions
    /// will be parsed to an executable Java artifact).
    /// 
    /// To create a Deployment, use the <seealso cref="Sys.Workflow.Engine.Repository.IDeploymentBuilder"/>. A Deployment on itself is a <b>read-only</b> object and its content cannot be changed after deployment
    /// (hence the builder that needs to be used).
    /// 
    /// 
    /// 
    /// </summary>
    public interface IDeployment
    {

        string Id { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 部署时间
        /// </summary>
        DateTime DeploymentTime { get; }

        /// <summary>
        /// schemam目录名
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 流程key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 租户id
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// 业务键值
        /// </summary>
        string BusinessKey { get; }

        /// <summary>
        /// 业务路径
        /// </summary>
        string BusinessPath { get; }

        /// <summary>
        /// 启动表单
        /// </summary>
        string StartForm { get; }
    }

}