pac auth create -u https://orgb58198d5.crm.dynamics.com -id $(clientid)  -cs $(secret) -t $(tenant)
pac paportal list
pac auth select --index 1

git checkout ("$(Build.SourceBranch)" -replace "^refs/heads/", "")
##dir $(Agent.BuildDirectory)/s

dir $(Build.Repository.LocalPath)/portals

pac paportal upload --path $(Build.Repository.LocalPath)/portals/customer-self-service
