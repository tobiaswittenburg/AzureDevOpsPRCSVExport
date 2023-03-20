/* Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to
reproduce and distribute the object code form of the Sample Code, provided that. You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include
a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’
fees, that arise or result from the use or distribution of the Sample Code */

using AzureDevOpsListHistoricPRs;
using System.Collections.Generic;

Console.WriteLine("Sample console application to get all Pull Requests from a repository.!");

string PAT = string.Empty;
string ProjectName = string.Empty;
string Organization = string.Empty;
string RepositoryID = string.Empty;

var PRs = new Rootobject();

var azureDevOpsInteraction = new AzDOInteraction();

if (args.Length == 4)
{
    PAT = args[0];
    ProjectName = args[1];
    Organization = args[2];
    RepositoryID = args[3];

    Console.WriteLine("PAT: " + PAT);
    Console.WriteLine("ProjectName: "+ ProjectName);
    Console.WriteLine("Organization: " + Organization);
    Console.WriteLine("RepositoryID: " + RepositoryID);
}
else 
{
    Console.WriteLine("Please use the following order for arguments: PAT, ProjectName, Organization, RepositoryID");
}


try
{
    PRs = await azureDevOpsInteraction.GetAllPullRequestsAsync(Organization, ProjectName, RepositoryID, PAT);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

var ResultList = new List<ResultObject>();

foreach(Value v in PRs.value)
{
    ResultList.Add(new ResultObject(v.pullRequestId, v.description));
}

foreach (ResultObject result in ResultList)
{
    string csv = string.Format("{0},{1}\n", result.ID, result.Description);
    File.AppendAllText(@"C:\Temp\PRs.csv", csv);
}

