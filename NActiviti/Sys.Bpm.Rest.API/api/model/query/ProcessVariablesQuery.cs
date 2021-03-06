﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Workflow.Cloud.Services.Api.Model
{
    public class ProcessVariablesQuery
    {
        public string Id { get; set; }

        public string ProcessInstanceId { get; set; }

        public string TaskId { get; set; }

        public string VariableName { get; set; }

        public bool ExcludeTaskVariables { get; set; }
    }
}
