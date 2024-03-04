#  This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.  
#  THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
#  INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.  
#  We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object 
#  code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software 
#  product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the 
#  Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims 
#  or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code.
Install-Module -Name PoshRSJob

$azureAdPowerShellId = "1950a258-227b-4e31-a9cf-717495945fc2"
$tenantId = "<azure ad tenant>" 
$username = "<maker uuid>"
$unsecurePass = "<maker password>"
$flowName = '<flow name>'
$environmentName = '<environment id found in ppac>'


#connect with Invoke
$token = [System.Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes(":$($PAT)"))
$header = @{"Content-Type" = "application/x-www-form-urlencoded"}
$projectsUrl = "https://login.windows.net/common/oauth2/token"

$username = [System.Web.HttpUtility]::UrlEncode($username)
$unsecurePass = [System.Web.HttpUtility]::UrlEncode($unsecurePass)
$authbody = "resource=https%3A%2F%2Fmanagement.azure.com%2F&client_id=$($azureAdPowerShellId)&grant_type=password&username=$($username)&scope=openid&password=$($unsecurePass)";


$projects = Invoke-RestMethod -Uri $projectsUrl -Method Post -ContentType "application/x-www-form-urlencoded" -Body $authbody
#getrefreshtoken
Write-Host $projects.refresh_token

$authbody = "resource=https%3A%2F%2Fservice.flow.microsoft.com%2F&client_id=$($azureAdPowerShellId)&grant_type=refresh_token&refresh_token=$($projects.refresh_token)";

#get flow service token
$projectsUrl = "https://login.windows.net/$($tenantId)/oauth2/token"
$projects = Invoke-RestMethod -Uri $projectsUrl -Method Post -ContentType "application/x-www-form-urlencoded" -Body $authbody
#set flow token for future use
$flowToken = $projects.access_token;
$header = @{authorization = "Bearer $flowToken"}

#Define Script Block for use with Resource Pool
$scriptBlock = {
Param($flowName, $runningFlow, $header, $environmentName)

        Invoke-RestMethod -Uri "https://us.api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/$($environmentName)/flows/$($flowName)/triggers/Recurrence/histories/$($_.name)/resubmit?api-version=2016-11-01" -Method Post -Headers $header
        

        #Return object for Resource Pool
        [pscustomobject]@{
            flow=$Using:flowName
            runningFlow=$_
            header=$Using:header
            environmentName=$Using:environmentName
        }

}

#Get Flow Runs and Check for NextLink
$nextLink = "https://us.api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/$($environmentName)/flows/$($flowName)/runs?api-version=2016-11-01&`$filter=Status%20eq%20%27failed%27"

while ($nextLink -ne $null) {
    $flowRuns =  Invoke-RestMethod -Uri $nextLink -Method Get -Headers $header
    #foreach ($runningFlow in $flowRuns.value){
        Write-Host "Begin Resource Pool job with Throttle = 2" -ForegroundColor Green
        $flowRuns.value | Start-RSJob -ScriptBlock $scriptBlock -ArgumentList $flowName, $_, $header, $environmentName -Throttle 2
        #Invoke-RestMethod -Uri "https://us.api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/$($environmentName)/flows/$($flowName)/runs/$($runningFlow.name)/cancel?api-version=2016-11-01" -Method Post -Headers $header
    #}
    if ($flowRuns.nextLink -ne $null){
        $nextLink = $flowRuns.nextLink
    }
    else{
        $nextLink = $null
    }
}





