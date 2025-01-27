---
title: Collect Environment Storage in Azure Log Analytics
weight: 1
geekdocCollapseSection: true
---
# Summary
This Power Automate flow connects to AKV to get a S2S user secret then connects to the BAP API to collect environment storage. It sends the data to a custom table in Azure Log Analytics which can be monitored by Azure Monitor Alerts.

## Pre-Reqs
Azure Log Analytics
Azure Key Vault

## Install
1. Download Solution
2. Import unmanaged solution
3. Modify AKV connection reference if needed
4. Modify environment variable if needed

