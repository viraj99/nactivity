﻿using System.Collections.Generic;

/*
 * Licensed under the Apache License, Version 2.0 (the "License");
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

namespace Sys.Workflow.Engine.Impl.JobExecutors
{
    public sealed class TimerDeclarationType
	{
	  public static readonly TimerDeclarationType DATE = new TimerDeclarationType("DATE", InnerEnum.DATE, Calendars.DueDateBusinessCalendar.NAME);
	  public static readonly TimerDeclarationType DURATION = new TimerDeclarationType("DURATION", InnerEnum.DURATION, Calendars.DurationBusinessCalendar.NAME);
	  public static readonly TimerDeclarationType CYCLE = new TimerDeclarationType("CYCLE", InnerEnum.CYCLE, Calendars.CycleBusinessCalendar.NAME);

	  private static readonly IList<TimerDeclarationType> valueList = new List<TimerDeclarationType>();

	  static TimerDeclarationType()
	  {
		  valueList.Add(DATE);
		  valueList.Add(DURATION);
		  valueList.Add(CYCLE);
	  }

	  public enum InnerEnum
	  {
		  DATE,
		  DURATION,
		  CYCLE
	  }

	  public readonly InnerEnum innerEnumValue;
	  private readonly string nameValue;
	  private readonly int ordinalValue;
	  private static int nextOrdinal = 0;

	  public readonly string calendarName;

	  internal TimerDeclarationType(string name, InnerEnum innerEnum, string calendarName)
	  {
		this.calendarName = calendarName;

		  nameValue = name;
		  ordinalValue = nextOrdinal++;
		  innerEnumValue = innerEnum;
	  }

		public static IList<TimerDeclarationType> Values()
		{
			return valueList;
		}

		public int Ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static TimerDeclarationType ValueOf(string name)
		{
			foreach (TimerDeclarationType enumInstance in valueList)
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}