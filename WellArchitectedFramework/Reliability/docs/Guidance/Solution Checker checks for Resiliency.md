---
title: Dataverse Code Review Guidance for Resilient Applications
author: Ali Youssefi
weight: 1
geekdocCollapseSection: true
#slug: Reliability
#geekdocHidden: true
#geekdocHiddenTocTree: true
geekdocAlign: "left"
---

Code Reviews are helpful to identify violations in customizations.

Organizations can run a code review called [Solution Checker](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/use-powerapps-checker) against their solutions. I recommend reviewing the last time Solution Checker was run. If its been a while or a recent code release was performed, run Solution Checker again. Ideally, Solution Checker is run on each commit or extraction from a development environment.

# Recommendations
- Enable Managed Environments for Dynamics 365 production environments.
- Enable Solution Checker enforcement on test or build environments.
- Track analysis results and assign work items for each.

# Solution Checker Guidelines for Resiliency

| Rule                                                                                                                                                                                                                                                              | Notes                                                           |
| ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------- |
| [Avoid duplicate Dataverse plug-in registrations](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/best-practices/business-logic/do-not-duplicate-plugin-step-registration?client=PAChecker&error=meta-remove-dup-reg&source=featuredocs)     | Can create SQL deadlocking [Query Link](Link)                                     |
| [Remove deactivated or disabled customizations](https://learn.microsoft.com/en-us/power-apps/developer/model-driven-apps/best-practices/business-logic/remove-deactivated-disabled-configurations?client=PAChecker&error=meta-remove-inactive&source=featuredocs) | Carrying unneeded code can cause confusion and slow deployments |
| [Interact with HTTP and HTTPS resources asynchronously](https://learn.microsoft.com/en-us/power-apps/developer/model-driven-apps/best-practices/business-logic/interact-http-https-resources-asynchronously)                                                      | Browsers are moving away from sync calls                        |
| [Avoid Modals](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-modals)                                                                                                                                         | Deprecated                                                      |
| [Avoid DOM Form](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-dom-form)                                                                                                                                     | Not supported                                                   |
| [Avoid DOM Form Events](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-dom-form-event)                                                                                                                        | Not supported                                                   |
| [Avoid CRM 2011 OData](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-crm2011-service-odata)                                                                                                                  | Deprecated                                                      |
| [Avoid CRM 2011 SOAP](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-crm2011-service-soap)                                                                                                                    | Deprecated                                                      |
| [Avoid Browser Specific APIs](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-browser-specific-api)                                                                                                            | IE or legacy browser functionality should be removed            |
| [Avoid Unpublished Functionality](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-unpub-api)                                                                                                                   | Can be flaky and cause code to break                            |
| [Avoid window.top](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-window-top)                                                                                                                                 | Not guaranteed to be correct DOM level                          |
| [Use Relative URI](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-relative-uri)                                                                                                                                 | Can hard code incorrect url                                     |
| [Use Navigation API](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-navigation-api)                                                                                                                             | Deprecated                                                      |
| [Use Client Context](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-client-context)                                                                                                                             | Deprecated                                                      |
| [Use Offline](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-offline)                                                                                                                                           | Deprecated                                                      |
| [Do not make parent assumption](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/do-not-make-parent-assumption)                                                                                                       | Can be flaky and cause code to break                            |
| [Use Org Setting](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-org-setting)                                                                                                                                   | Deprecated                                                      |
| [Use Global Context](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-global-context)                                                                                                                             | Deprecated                                                      |
| [Use Grid API](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-grid-api)                                                                                                                                         | Deprecated                                                      |
| [Use Utility Dialogs](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-utility-dialogs)                                                                                                                           | Deprecated                                                      |
| [Avoid IsActivityType](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/avoid-isactivitytype)                                                                                                                         | Deprecated                                                      |
| [Avoid SilverLight](https://learn.microsoft.com/en-us/power-platform/important-changes-coming?client=PAChecker&error=meta-avoid-silverlight&source=featuredocs)                                                                                                   | Deprecated                                                      |
| [Use STRICT mode](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Strict_mode)                                                                                                                                                                  | Helps with resiliency of JS                                     |
| [Use strict equality operators](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Equality_comparisons_and_sameness)                                                                                                                                        | Helps with resiliency of JS                                     |
| [Avoid with](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/with)                                                                                                                                                                   | Deprecated                                                      |
| [Use App Side Pane API](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/powerapps-checker/rules/web/use-appsidepane-api)                                                                                                                         | Deprecated                                                      |
| Address HIGH formula issues with Canvas Apps                                                                                                                                                                                                                      |                                                                 |
| Avoid AutoStart in Canvas Apps                                                                                                                                                                                                                                    |                                                                 |

# How to track for specific scans of a solution
Organizations can track the results of Solution Checker through Analysis Jobs. The list above is a great place to start to assess your production solutions resiliency. 

```
//Check analysis jobs within a specific environment

https://[org].crm.dynamics.com/api/data/v9.1/msdyn_analysisjobs

//Check analysis results within a specific environment

https://[org].crm.dynamics.com/api/data/v9.1/msdyn_analysisresults

//Check Results for a specific solution name (analysis component)

https://[org].crm.dynamics.com/api/data/v9.1/msdyn_analysisresults?$filter=msdyn_AnalysisComponentId/msdyn_componentname eq '<solutionName>'
```

# 