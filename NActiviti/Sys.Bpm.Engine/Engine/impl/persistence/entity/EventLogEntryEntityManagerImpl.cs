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

    using org.activiti.engine.@event;
    using org.activiti.engine.impl.cfg;
    using org.activiti.engine.impl.persistence.entity.data;

    /// 
    public class EventLogEntryEntityManagerImpl : AbstractEntityManager<IEventLogEntryEntity>, IEventLogEntryEntityManager
	{

	  protected internal IEventLogEntryDataManager eventLogEntryDataManager;

	  public EventLogEntryEntityManagerImpl(ProcessEngineConfigurationImpl processEngineConfiguration, IEventLogEntryDataManager eventLogEntryDataManager) : base(processEngineConfiguration)
	  {
		this.eventLogEntryDataManager = eventLogEntryDataManager;
	  }

	  protected internal override IDataManager<IEventLogEntryEntity> DataManager
	  {
		  get
		  {
			return eventLogEntryDataManager;
		  }
	  }

	  public virtual IList<IEventLogEntry> findAllEventLogEntries()
	  {
		return eventLogEntryDataManager.findAllEventLogEntries();
	  }

	  public virtual IList<IEventLogEntry> findEventLogEntries(long startLogNr, long pageSize)
	  {
	   return eventLogEntryDataManager.findEventLogEntries(startLogNr, pageSize);
	  }

	  public virtual IList<IEventLogEntry> findEventLogEntriesByProcessInstanceId(string processInstanceId)
	  {
		return eventLogEntryDataManager.findEventLogEntriesByProcessInstanceId(processInstanceId);
	  }

	  public virtual void deleteEventLogEntry(long logNr)
	  {
		eventLogEntryDataManager.deleteEventLogEntry(logNr);
	  }

	  public virtual IEventLogEntryDataManager EventLogEntryDataManager
	  {
		  get
		  {
			return eventLogEntryDataManager;
		  }
		  set
		  {
			this.eventLogEntryDataManager = value;
		  }
	  }


	}

}