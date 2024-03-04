Write-Host "Hello World"

if (!(Get-Command pac -ErrorAction Ignore))
{
  Write-Host "downloading pac..."
  $packageId = switch("$(Agent.OS)") {
    "Linux" { "Microsoft.PowerApps.CLI.Core.linux-x64" }
    "Darwin" { "Microsoft.PowerApps.CLI.Core.osx-x64" }
    "Windows_NT" { "Microsoft.PowerApps.CLI" }
  }
  Write-Host "package id: $packageId"
  $id = $packageId.ToLower()
  $packageInfo = Invoke-RestMethod `
    "https://api.nuget.org/v3/registration5-semver1/$id/index.json"
  $version = $packageInfo.items[0].upper
  Write-Host "latest version: $version"
  Invoke-WebRequest `
    -Uri "https://api.nuget.org/v3-flatcontainer/$id/$version/$id.$version.nupkg" `
    -OutFile "$(Agent.TempDirectory)/$packageId.zip"
  Write-Host "downloaded $packageId.zip"
  Expand-Archive `
    "$(Agent.TempDirectory)/$packageId.zip" `
    "$(Agent.TempDirectory)/$packageId"
  Write-Host "extracted to $packageId"
  Copy-Item `
    "$(Agent.TempDirectory)/$packageId/tools" `
    "$(Agent.TempDirectory)/pac" `
    -Recurse
  Write-Host "copied tools subfolder to $(Agent.TempDirectory)/pac"

  cd $(Agent.TempDirectory)/pac 
  ./pac help
  if ("$(Agent.OS)" -eq "Windows_NT") {
    $(Agent.TempDirectory)/pac/pac telemetry disable
  }
##This adds pac directory to system paths
Write-Host "##vso[task.prependpath]$(Agent.TempDirectory)/pac"
}