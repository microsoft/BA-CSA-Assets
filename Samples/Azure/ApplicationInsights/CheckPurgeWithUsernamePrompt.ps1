Connect-AzAccount
Write-Host "Logged In"
$token = Get-AzAccessToken

Write-Output 'Token: ' $token.Token


$authHeader = @{
    'Content-Type'='application/json'
    'Authorization'='Bearer ' + $token.Token
}

# Invoke the REST API
$restUri = 'https://management.azure.com/subscriptions/527e910a-287d-42a6-af7d-995051d65bef/resourceGroups/D365-Telemetry/providers/microsoft.insights/components/D365-Telemetry-Insights/operations/purge-7dbc3c8c-6bc4-4916-9952-8b31ae212f3a?api-version=2015-05-01'
$response = Invoke-RestMethod -Uri $restUri -Method Get -Headers $authHeader