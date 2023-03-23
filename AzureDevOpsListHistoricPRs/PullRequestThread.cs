﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsListHistoricPRs
{

    public class ListOfPullRequestThreads
    {
        public PullRequestThread[] value { get; set; }
        public int count { get; set; }
    }

    public class PullRequestThread
    {
        public Pullrequestthreadcontext pullRequestThreadContext { get; set; }
        public int id { get; set; }
        public DateTime publishedDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public Comment[] comments { get; set; }
        public string status { get; set; }
        public Threadcontext threadContext { get; set; }
        public Properties properties { get; set; }
        public object identities { get; set; }
        public bool isDeleted { get; set; }
        public _Links _links { get; set; }
    }

    public class Pullrequestthreadcontext
    {
        public Iterationcontext iterationContext { get; set; }
        public int changeTrackingId { get; set; }
    }

    public class Iterationcontext
    {
        public int firstComparingIteration { get; set; }
        public int secondComparingIteration { get; set; }
    }

    public class Threadcontext
    {
        public string filePath { get; set; }
    }

    public class Properties
    {
        public MicrosoftTeamfoundationDiscussionSupportsmarkdown MicrosoftTeamFoundationDiscussionSupportsMarkdown { get; set; }
        public MicrosoftTeamfoundationDiscussionUniqueid MicrosoftTeamFoundationDiscussionUniqueID { get; set; }
    }

    public class MicrosoftTeamfoundationDiscussionSupportsmarkdown
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class MicrosoftTeamfoundationDiscussionUniqueid
    {
        public string type { get; set; }
        public string value { get; set; }
    }

 

    public class Self
    {
        public string href { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public int parentCommentId { get; set; }
        public Author author { get; set; }
        public string content { get; set; }
        public DateTime publishedDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public DateTime lastContentUpdatedDate { get; set; }
        public string commentType { get; set; }
        public object[] usersLiked { get; set; }
        public _Links2 _links { get; set; }
    }

    public class Author
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links2
    {
        public Self1 self { get; set; }
        public Repository1 repository { get; set; }
        public Threads threads { get; set; }
        public Pullrequests pullRequests { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

    public class Repository1
    {
        public string href { get; set; }
    }

    public class Threads
    {
        public string href { get; set; }
    }

    public class Pullrequests
    {
        public string href { get; set; }
    }

}
