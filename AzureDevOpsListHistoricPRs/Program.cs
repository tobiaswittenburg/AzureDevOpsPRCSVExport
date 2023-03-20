/* Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to
reproduce and distribute the object code form of the Sample Code, provided that. You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include
a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’
fees, that arise or result from the use or distribution of the Sample Code */

using AzureDevOpsListHistoricPRs;
using System.Collections.Generic;

Console.WriteLine("Sample console application to get all Pull Requests from a repository.!");

var PAT = string.Empty;
var ProjectName = string.Empty;
var Organization = string.Empty;
var RepositoryID = string.Empty;

var azureDevOpsInteraction = new AzDOInteraction();

var PRs = await azureDevOpsInteraction.GetAllPullRequestsAsync(Organization, ProjectName, RepositoryID, PAT);

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

