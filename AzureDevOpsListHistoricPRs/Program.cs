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
string OutputFilePath = string.Empty;
string PRStatus = string.Empty;

List<PullRequest> PRs = null;

if (args.Length == 6)
{
    PAT = args[0];
    ProjectName = args[1];
    Organization = args[2];
    RepositoryID = args[3];
    OutputFilePath = args[4];
    PRStatus = args[5];

    Console.WriteLine("PAT: " + PAT);
    Console.WriteLine("ProjectName: "+ ProjectName);
    Console.WriteLine("Organization: " + Organization);
    Console.WriteLine("RepositoryID: " + RepositoryID);
    Console.WriteLine("OutputFilePath: " + OutputFilePath);
    Console.WriteLine("PRStatus: " + PRStatus);
}
else 
{
    Console.WriteLine("Please use the following order for arguments: PAT, ProjectName, Organization, RepositoryID, OutputFilePath, PRStatus");
}

var azureDevOpsInteraction = new AzDOInteraction(PAT);

try
{
    PRs = await azureDevOpsInteraction.GetAllPullRequestsAsync(Organization, ProjectName, RepositoryID, PRStatus);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

foreach(PullRequest pullRequest in PRs)
{
    SinglePullRequest singlePR = await azureDevOpsInteraction.GetPullRequestDetailsAsync(Organization, pullRequest.pullRequestId);
    
    string currentDescription = string.Empty;
    string lastMergeCommitComment = string.Empty;
    if (pullRequest.description != null)
    {
        currentDescription = pullRequest.description.Replace(System.Environment.NewLine, "").Replace("\n"," ").Replace("\r"," ").Replace(","," ");
    }

    if ((singlePR != null) && (singlePR.lastMergeCommit != null) && (singlePR.lastMergeCommit.comment != null))
    {
        lastMergeCommitComment = singlePR.lastMergeCommit.comment.Replace(System.Environment.NewLine, "").Replace("\n", " ").Replace("\r", " ").Replace(",", " ");
    }

    ListOfPullRequestThreads threads = await azureDevOpsInteraction.GetPullRequestThreadsAsync(Organization, ProjectName, RepositoryID, pullRequest.pullRequestId);

    string commentContent = string.Empty;
    if (threads.count > 0)
    {
        foreach (PullRequestThread thread in threads.value)
        {
            if (thread.comments.Length > 0)
            {
                foreach (Comment comment in thread.comments)
                {
                    if (comment.content != null)
                    {
                        commentContent += (comment.author.ToString() + ": " + comment.content.Replace(System.Environment.NewLine, "").Replace("\n", " ").Replace("\r", " ").Replace(",", " "));
                    }
                }
            }
        }
    }


    string csv = string.Format("{0},{1},{2},{3}\n", pullRequest.pullRequestId, currentDescription, commentContent, pullRequest.closedDate.ToShortDateString());
    File.AppendAllText(OutputFilePath, csv);
}

