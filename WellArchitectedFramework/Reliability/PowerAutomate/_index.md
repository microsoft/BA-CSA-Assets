---
title: Power Automate Alerts
weight: 5
geekdocCollapseSection: true
slug: Reliability
#geekdocHidden: true
#geekdocHiddenTocTree: true
geekdocAlign: "left"
---

# How To Use
Use these alerts directly in your Azure Subscription. They require the enablement of [Application Insights for Power Automate](https://learn.microsoft.com/en-us/power-platform/admin/app-insights-cloud-flow). Once enabled, clicking the buttons below will direct you to the Azure Portal to create alerts automatically.

{{< alertList name="alertList" >}}
<script>
    console.log('-------- Hugo Debug: Theme = Twenty-Sixteen -------')
    console.log('HUGO_ENV: ', '{{ (getenv "HUGO_ENV") | default "NOT SET" }}')
    console.log('Long Env: ', '{{ (getenv "HUGO_ENV") | (getenv "ENVIRONMENT") | default .Site.Params.env | default "DEV" }}')
    console.log('Is Prod?: ', '{{ (eq (getenv "HUGO_ENV") "production" | or (eq .Site.Params.env "production")) }}')
    console.log('Page Kind: ', '{{.Page.Kind}}')
    console.log('Current Section: ', '{{.Page.CurrentSection | default "NOT SET" }}')
    console.log('Page Section: ', '{{.Page.Section | default "NOT SET" }}')
    console.log('Page Type: ', '{{.Page.Type}}')
    console.log('Page Dir: ', '{{.Page.Dir | default "/" }}{{ .File }}')
    console.log('Page Wd: ', '{{.Page.FuzzyWordCount}}')
    console.log('Page Home?: ', '{{.Page.IsHome}}')
    console.log('Page Node?: ', '{{.Page.IsNode}}')
    console.log('Page Page?: ', '{{.Page.IsPage}}')
    console.log('Page Date: ', '{{.Page.Date}}')
    console.log('Page Lastmod: ', '{{.Page.Lastmod}}')
    console.log('Page Date=Lastmod?: ', '{{ (eq .Page.Lastmod .Page.Date) }}')
    console.log('Page Date<>Lastmod?: ', '{{ (ne .Lastmod .Date) }}')
    console.log('Section Parent: ', '{{ with .Parent}}{{ .Dir | default "/" }}{{ .File }}{{end}}')
    console.log('Sharing Icons?: ', '{{ .Site.Params.Sharingicons | default "NOT DEFINED" }}')
    console.log('Comments?: ', '{{ .Params.comments | default "NOT DEFINED" }}')
    console.log('Disquss?: ', '{{ $.Site.DisqusShortname | default "NOT DEFINED" }}')
    console.log('Google Analytics?: ', '{{ .Site.GoogleAnalytics | default "NOT DEFINED" }}')
    console.log('Count of related pages: ', '{{if .Page.IsPage}}{{ len (.Site.RegularPages.Related .) }}{{end}}' )
    console.log(': ', '')
    console.log('---------------------------------------------------')
</script>