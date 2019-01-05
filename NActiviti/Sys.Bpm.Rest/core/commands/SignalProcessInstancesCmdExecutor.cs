﻿using org.activiti.cloud.services.api.commands;
using org.activiti.cloud.services.api.commands.results;
using org.springframework.messaging;
using org.springframework.messaging.support;
using System;

namespace org.activiti.cloud.services.core.commands
{
    public class SignalProcessInstancesCmdExecutor : CommandExecutor<SignalCmd>
    {

        private ProcessEngineWrapper processEngine;
        private MessageChannel<SignalProcessInstancesResults> commandResults;

        public SignalProcessInstancesCmdExecutor(ProcessEngineWrapper processEngine, MessageChannel<SignalProcessInstancesResults> commandResults)
        {
            this.processEngine = processEngine;
            this.commandResults = commandResults;
        }

        public virtual Type HandledType
        {
            get
            {
                return typeof(SignalCmd);
            }
        }

        public virtual void execute(SignalCmd cmd)
        {
            processEngine.signal(cmd);
            SignalProcessInstancesResults cmdResult = new SignalProcessInstancesResults(cmd.Id);
            commandResults.send(MessageBuilder<SignalProcessInstancesResults>.withPayload(cmdResult).build());
        }
    }

}