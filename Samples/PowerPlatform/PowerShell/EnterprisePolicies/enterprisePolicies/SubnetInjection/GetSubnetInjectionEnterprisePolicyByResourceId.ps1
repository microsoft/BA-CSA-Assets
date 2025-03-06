# Load the environment script
. "$PSScriptRoot\..\Common\EnterprisePolicyOperations.ps1"

function GetSubnetInjectionEnterprisePolicyByResourceId
{
     param(
        [Parameter(
            Mandatory=$true,
            HelpMessage="The Policy Id"
        )]
        [string]$enterprisePolicyArmId,

        [Parameter(
            Mandatory=$true,
            HelpMessage="The Subscription Id"
        )]
        [string]$subscriptionid
    )

    Write-Host "Logging In..." -ForegroundColor Green
    $connect = AzureLogin $subscriptionId
    if ($false -eq $connect)
    {
        return
    }

    Write-Host "Logged In..." -ForegroundColor Green

    $policy = GetEnterprisePolicy $enterprisePolicyArmId
    $policyString = $policy | ConvertTo-Json -Depth 7
    Write-Host $policyString

}
GetSubnetInjectionEnterprisePolicyByResourceId