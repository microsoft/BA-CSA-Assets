# Load the environment script
. "$PSScriptRoot\..\Common\EnterprisePolicyOperations.ps1"

function RemoveSubnetInjectionEnterprisePoliciesInSubscription
{
     param(
        [Parameter(
            Mandatory=$true,
            HelpMessage="The subscriptionId"
        )]
        [string]$subscriptionId,
        [Parameter(
            Mandatory=$true,
            HelpMessage="The armId"
        )]
        [string]$armId
    )

    Write-Host "Logging In..." -ForegroundColor Green
    $connect = AzureLogin $subscriptionId
    if ($false -eq $connect)
    {
        return
    }

    Write-Host "Logged In..." -ForegroundColor Green
    $status = RemoveEnterprisePolicy $subscriptionId $armId
    Write-Host $status

}
RemoveSubnetInjectionEnterprisePoliciesInSubscription