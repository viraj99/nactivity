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

	using org.activiti.engine.repository;

	/// 
	public interface IModelEntityManager : IEntityManager<IModelEntity>
	{

	  void insertEditorSourceForModel(string modelId, byte[] modelSource);

	  void insertEditorSourceExtraForModel(string modelId, byte[] modelSource);

	  IList<IModel> findModelsByQueryCriteria(ModelQueryImpl query, Page page);

	  long findModelCountByQueryCriteria(ModelQueryImpl query);

	  byte[] findEditorSourceByModelId(string modelId);

	  byte[] findEditorSourceExtraByModelId(string modelId);

	  IList<IModel> findModelsByNativeQuery(IDictionary<string, object> parameterMap, int firstResult, int maxResults);

	  long findModelCountByNativeQuery(IDictionary<string, object> parameterMap);

	  void updateModel(IModelEntity updatedModel);

	  void deleteEditorSource(IModelEntity model);

	  void deleteEditorSourceExtra(IModelEntity model);

	}
}