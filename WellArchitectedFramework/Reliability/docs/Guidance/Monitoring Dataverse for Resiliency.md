---
title: Monitoring Dataverse for Resiliency
author: Ali Youssefi
weight: 1
geekdocCollapseSection: true
#slug: Reliability
#geekdocHidden: true
#geekdocHiddenTocTree: true
geekdocAlign: "left"
---

### References
[Monitoring the Power Platform]()


# Monitoring Dataverse for Resiliency


## Dataverse Connector Legacy Decommission


## OData v2.0 Service Decommission

## API Throttled Users

## Plug-In Failures – Async/Sync

## Async Processing Backlog

## SQL Timeouts

## AsyncOperation Data Size

## Failing Power Automate Flows
### Dataverse Enabled Flow History - Customer Configurable
[Solution aware Power Automate flows can be stored within elastic tables in Dataverse.](https://learn.microsoft.com/en-us/power-automate/dataverse/cloud-flow-run-metadata). This allows admins, who may not have access to tools like the Center of Excellence or Azure resources, to view flow run history.

This can impact storage/capacity so read the documentation on settings to reduce or eliminate if capacity is strained.

Also, while Microsoft provides a best effort, not all runs are guaranteed to show up. Consider Application Insights or the Process Simple API for a more accurate review.
#### Sample
[Dataverse - Query for Failed Flows in the past hour](../../samples/PowerAutomate/Queries/Dataverse%20Web%20API%20-%20FlowRunsWithStatusOfFailedInLastHour.txt)
### Process Simple
Process Simple can be used by administrators to review flow logs, resubmit in bulk, etc. While not technically supported this API is used by the Power Automate service and is reliable.
#### Sample
Collect Queued Flows
