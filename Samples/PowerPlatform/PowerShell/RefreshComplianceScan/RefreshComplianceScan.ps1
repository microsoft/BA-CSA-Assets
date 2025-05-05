# Load the environment script
. "$PSScriptRoot\Common\AzurePolicyOperations.ps1"
#. "$PSScriptRoot\ValidateVnetLocationForEnterprisePolicy.ps1"

#Import-Module Az.Advisor

function RefreshAzureScan
{
     param(
        [Parameter(
            Mandatory=$true,
            HelpMessage="The Policy subscription"
        )]
        [string]$subscriptionId,

        [Parameter(
            Mandatory=$true,
            HelpMessage="The Policy resource group"
        )]
        [string]$resourceGroup 

    )


    Write-Host "Logging In..." -ForegroundColor Green
    $connect = AzureLogin -subscription $subscriptionId
    Connect-AzAccount -Subscription $subscriptionId
    $costSummary = Get-AzAdvisorRecommendation -Category Cost
    $summary = Get-AzPolicyStateSummary -SubscriptionId $subscriptionId -ResourceGroupName $resourceGroup

    #$job = Start-AzPolicyComplianceScan -AsJob -ResourceGroupName $resourceGroup
    #$job | Wait-Job
}
RefreshAzureScan