Import-Module Az.Advisor

function AzureLogin($subscription) {

    $connect = Connect-AzAccount -Subscription $subscription -

    if ($null -eq $connect)
    {
        Write-Host "Error connecting to Azure Account `n" -ForegroundColor Red
        return $false
    }

    return $true
}

function GetSummary($subscriptionId, $resourceGroupName){
    $summary = Get-AzPolicyStateSummary -SubscriptionId $subscriptionId -ResourceGroupName $resourceGroupName
}