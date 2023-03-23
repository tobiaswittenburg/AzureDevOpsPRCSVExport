/* Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment. THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to
reproduce and distribute the object code form of the Sample Code, provided that. You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include
a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’
fees, that arise or result from the use or distribution of the Sample Code */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureDevOpsListHistoricPRs
{
    public class AzDOInteraction
    {

    string pat = string.Empty;

    public AzDOInteraction(string PAT)
    {
        this.pat = PAT;    
    }

        /// <summary>
        /// Retrieves all Pull Requests from a repository
        /// Documentation: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-requests/get-pull-requests?view=azure-devops-rest-7.1&tabs=HTTP#pullrequeststatus
        /// </summary>
        /// <param name="organization">The Organization where the Project resides in.</param>
        /// <param name="project">The project with the repository.</param>
        /// <param name="RepositoryID">The repository ID. Can be obtained by az repos list --project "PROJECTNAME"</param>
        /// <param name="PAT">The Personal Access Token , from the Azure DevOps UI</param>
        /// <returns>ListOfPullRequests</returns>
        public async Task<List<PullRequest>> GetAllPullRequestsAsync(string organization, string project, string RepositoryID, string status)
        {
            string responseBody = string.Empty;

            bool isLastPage = false;
            int offset = 0;
            int batchSize = 50;
            List<PullRequest> pullRequests = new List<PullRequest>();

            while(isLastPage == false)
            {
          
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", pat))));
                    
                    string callURL = string.Format("https://dev.azure.com/{0}/_apis/git/repositories/{1}/pullrequests?searchCriteria.status={2}&$top={3}&$skip={4}&api-version=7.1-preview.1", organization, RepositoryID, status, batchSize, offset); 
                   
                    //string callURL = string.Format("https://dev.azure.com/{0}/{1}/_apis/git/repositories/{2}/pullrequests?searchCriteria.status={2}", organization, project, RepositoryID, status);
                    //string callURL = string.Format("GET https://dev.azure.com/{0}/{1}/_apis/git/repositories/{2}/pullrequests?api-version=7.1-preview.1", organization, project, RepositoryID);

                    using (HttpResponseMessage response = await client.GetAsync(callURL))
                    {
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();                  
                    }                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            ListOfPullRequests? ListOfPRs = JsonSerializer.Deserialize<ListOfPullRequests>(responseBody);

            if (ListOfPRs == null)
            {
                throw new Exception("Could not deserialize the response from Azure DevOps.");
            }
            else
            {
                if ((ListOfPRs.count == 0) || (offset > 2500))
                {
                    isLastPage = true;
                }
                pullRequests.AddRange(ListOfPRs.value);
                offset += batchSize;
            }  
            }          
            return pullRequests;
        }

        /// <summary>
        /// Retrieves all Pull Requests from a repository
        /// Documentation: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-requests/get-pull-requests?view=azure-devops-rest-7.1&tabs=HTTP#pullrequeststatus
        /// </summary>
        /// <param name="organization">The Organization where the Project resides in.</param>
        /// <param name="PRID">The Pull Request ID.</param>
        /// <returns>PullRequest</returns>
        public async Task<SinglePullRequest> GetPullRequestDetailsAsync(string organization, int PRID)
        {
            string responseBody = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", pat))));
                                       
                    //string callURL = string.Format("https://dev.azure.com/{0}/_apis/git/pullrequests/{1}?api-version=7.1-preview.1", organization, PRID);
                    string callURL = string.Format("https://dev.azure.com/{0}/_apis/git/pullrequests/{1}?", organization, PRID);

                    using (HttpResponseMessage response = await client.GetAsync(callURL))
                    {
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();                  
                    }                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var pullRequest = JsonSerializer.Deserialize<SinglePullRequest>(responseBody);

            if (pullRequest == null)
            {
                throw new Exception("Could not deserialize the response from Azure DevOps.");
            }
            else
            {
                return pullRequest;
            }     
    }

    public async Task<ListOfPullRequestThreads> GetPullRequestThreadsAsync(string organization, string project, string repositoryID, int PRID)
    {
        string responseBody = string.Empty;

          try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", pat))));
                                                          
                    string callURL = string.Format("https://dev.azure.com/{0}/{1}/_apis/git/repositories/{2}/pullRequests/{3}/threads?api-version=7.0", organization, project, repositoryID, PRID);

                    using (HttpResponseMessage response = await client.GetAsync(callURL))
                    {
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();                  
                    }                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            var pullRequest = JsonSerializer.Deserialize<ListOfPullRequestThreads>(responseBody);

            if (pullRequest == null)
            {
                throw new Exception("Could not deserialize the response from Azure DevOps.");
            }
            else
            {
                return pullRequest;
            }    

    }
}
}

