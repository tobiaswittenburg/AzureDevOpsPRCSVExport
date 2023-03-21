# AzureDevOpsPRCSVExport

## About
This is a sample console application written in C# to get historic information from pull requests from Azure DevOps. It uses the rest api to get this information.

Before running yourself, please go into program.cs and add your PAT, your organization as well as your repository ID as well as your project name. This information is needed to access the correct repository for the pull request information.

The result is a .csv file, that you can then edit in Excel (or any other csv compatible application).

## Documentation

API Reference
https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-requests/get-pull-requests?view=azure-devops-rest-7.1&tabs=HTTP#pullrequeststatus


How to create PAT in Azure DevOps
https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops&tabs=Windows

How to get your Repository ID
I got mine from the Azuer CLI with 
  `az repos list --project "PROJECTNAME" `
There you find an entry "url" that looks like this "https://dev.azure.com/ORGNAME/SOMEGUID/_apis/git/repositories/REPOID

How to find your Org Name
That is the part after http://dev.azure.com/
