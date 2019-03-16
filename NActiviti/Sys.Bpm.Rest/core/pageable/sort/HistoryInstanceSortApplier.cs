﻿using org.activiti.api.runtime.shared.query;
using org.activiti.engine.history;
using org.activiti.engine.impl;
using org.activiti.engine.query;
using org.activiti.engine.task;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.activiti.cloud.services.core.pageable.sort
{
    /// <summary>
    /// 
    /// </summary>
    public class HistoryInstanceSortApplier : BaseSortApplier<IHistoricProcessInstanceQuery, IHistoricProcessInstance>
    {
        private IDictionary<string, IQueryProperty> orderByProperties = new Dictionary<string, IQueryProperty>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        public HistoryInstanceSortApplier()
        {
            orderByProperties["id"] = HistoricProcessInstanceQueryProperty.PROCESS_INSTANCE_ID_;
            orderByProperties["name"] = HistoricProcessInstanceQueryProperty.BUSINESS_KEY;
            orderByProperties["createTime"] = HistoricProcessInstanceQueryProperty.START_TIME;
        }

        /// <inheritdoc />
        protected internal override void applyDefaultSort(IHistoricProcessInstanceQuery query)
        {
            query.orderByProcessInstanceStartTime().asc();
        }

        /// <inheritdoc />
        protected internal override IQueryProperty getOrderByProperty(Sort.Order order)
        {
            orderByProperties.TryGetValue(order.Property, out IQueryProperty qp);

            return qp;
        }
    }
}