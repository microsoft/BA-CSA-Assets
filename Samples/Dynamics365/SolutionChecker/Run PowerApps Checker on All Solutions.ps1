#  This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.  
#  THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
#  INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.  
#  We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object 
#  code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software 
#  product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the 
#  Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims 
#  or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code.

#Import Micrsoft.Xrm.Data.Powershell module 
Import-Module Microsoft.Xrm.Data.Powershell

#folder location
$folder = "C:\temp"

# this uses an application user - see here for more details: https://docs.microsoft.com/en-us/power-platform/admin/create-users-assign-online-security-roles#create-an-application-user
$oAuthClientId = "<add client id>"
$password = "<add client secret>"

#Solution Checker application user - see here for more details: https://learn.microsoft.com/en-us/powershell/powerapps/get-started-powerapps-checker?view=pa-ps-latest
$solutionCheckerClientId = "<add client id>"
$solutionCheckerPassword = "<add client secret>"

#tenant and environment
$tenantId = "<add tenant id>"
$environmentId = "https://<orgname>.crm.dynamics.com"

$conn = Connect-CrmOnline -ClientSecret $password -OAuthClientId $oAuthClientId -ServerUrl $environmentId

# Retrieve the record again and display the result 
$solutions = Get-CrmRecords -conn $conn -EntityLogicalName solution -Fields friendlyname,uniquename -FilterAttribute ismanaged -FilterOperator eq -FilterValue $false

foreach($solution in $solutions.CrmRecords) {
    #Avoid running on default solution
    if ($solution.friendlyname -ne 'Common Data Services Default Solution'){
        $exportedSolution = Export-CrmSolution -conn $conn -SolutionName $solution.uniquename -Managed -SolutionFilePath $folder -SolutionZipFileName "$($solution.uniquename).zip"


        #https://learn.microsoft.com/en-us/powershell/powerapps/get-started-powerapps-checker?view=pa-ps-latest
        $rulesets = Get-PowerAppsCheckerRulesets -Geography UnitedStates
        $rulesetToUse = $rulesets | where Name -EQ 'AppSource Certification'
        $analyzeResult = Invoke-PowerAppsChecker -Geography UnitedStates -ClientApplicationId $solutionCheckerClientId `
            -TenantId $tenantId -ClientApplicationSecret (ConvertTo-SecureString -AsPlainText -Force -String $solutionCheckerPassword) -Ruleset $rulesetToUse -FileUnderAnalysis "$($folder)\$($solution.uniquename).zip" `
            -OutputDirectory $folder
    }

}