
$orgUrl = "<org>.crm.dynamics.com"
$clientId = ""
$clientSecret = ""
$tenantId = ""
$aadUrl = "https://login.microsoftonline.com/$($tenantId)/oauth2/token"


$authbody = "resource=https%3A%2F%2F$($orgUrl)%2F&client_id=$($clientId)&client_secret=$($clientSecret)&client_info=1&grant_type=client_credentials";
$aadLogin = Invoke-RestMethod -Uri $aadUrl -Method Post -ContentType "application/x-www-form-urlencoded" -Body $authbody
Write-Host $aadLogin.access_token
$header = @{authorization = "Bearer $($aadLogin.access_token)"}

#Get App Modules
$dataverseApi = "https://$($orgUrl)/api/data/v9.1/"
$appModulesResponse = Invoke-RestMethod -Uri "$($dataverseApi)appmodules?`$select=name,uniquename" -Method Get -Headers $header

$appModulesResponse.value | ForEach-Object {
Write-Host "Turning on In App Notificatons for " + $_.name
$saveSettingBody = @(@{
                    "AppUniqueName"="$($_.uniquename)";
                    "SettingName"="AllowNotificationsEarlyAccess";
                    "Value"="true";
                })
#Turn on App Notifications
$turnOnInAppNoti = Invoke-RestMethod -Uri "$($dataverseApi)SaveSettingValue" -Method Post -ContentType "application/json" -Headers $header -Body $(ConvertTo-Json $saveSettingBody)

}

