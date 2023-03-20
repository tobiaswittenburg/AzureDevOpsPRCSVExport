/* Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to
reproduce and distribute the object code form of the Sample Code, provided that. You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include
a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’
fees, that arise or result from the use or distribution of the Sample Code */

public class PullRequest
{
    public Repository repository { get; set; }
    public int pullRequestId { get; set; }
    public int codeReviewId { get; set; }
    public string status { get; set; }
    public Createdby createdBy { get; set; }
    public DateTime creationDate { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string sourceRefName { get; set; }
    public string targetRefName { get; set; }
    public string mergeStatus { get; set; }
    public bool isDraft { get; set; }
    public string mergeId { get; set; }
    public Lastmergesourcecommit lastMergeSourceCommit { get; set; }
    public Lastmergetargetcommit lastMergeTargetCommit { get; set; }
    public Lastmergecommit lastMergeCommit { get; set; }
    public object[] reviewers { get; set; }
    public string url { get; set; }
    public bool supportsIterations { get; set; }
    public string artifactId { get; set; }
}

public class Repository
{
    public string id { get; set; }
    public string name { get; set; }
    public string url { get; set; }
    public Project project { get; set; }
    public int size { get; set; }
    public string remoteUrl { get; set; }
    public string sshUrl { get; set; }
    public string webUrl { get; set; }
    public bool isDisabled { get; set; }
    public bool isInMaintenance { get; set; }
}

public class Project
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string url { get; set; }
    public string state { get; set; }
    public int revision { get; set; }
    public string visibility { get; set; }
    public DateTime lastUpdateTime { get; set; }
}

public class Createdby
{
    public string displayName { get; set; }
    public string url { get; set; }
    public _Links _links { get; set; }
    public string id { get; set; }
    public string uniqueName { get; set; }
    public string imageUrl { get; set; }
    public string descriptor { get; set; }
}

public class _Links
{
    public Avatar avatar { get; set; }
}

public class Avatar
{
    public string href { get; set; }
}

public class Lastmergesourcecommit
{
    public string commitId { get; set; }
    public string url { get; set; }
}

public class Lastmergetargetcommit
{
    public string commitId { get; set; }
    public string url { get; set; }
}

public class Lastmergecommit
{
    public string commitId { get; set; }
    public Author author { get; set; }
    public Committer committer { get; set; }
    public string comment { get; set; }
    public string url { get; set; }
}

public class Author
{
    public string name { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}

public class Committer
{
    public string name { get; set; }
    public string email { get; set; }
    public DateTime date { get; set; }
}
