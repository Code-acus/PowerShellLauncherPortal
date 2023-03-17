using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PowerShellLauncherPortal
{
    public class SharePointHandler
    {
        private const string SharePointApiUrl = "https://your-sharepoint-site-url/_api/web/";
        private const string SharePointFolderPath = "/YourFolder/Path/To/Scripts/";

        public async Task<List<string>> GetScriptListAsync(string accessToken)
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUrl = $"{SharePointApiUrl}GetFolderByServerRelativeUrl('{SharePointFolderPath}')/Files";
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(content);
                var scriptFiles = jsonResult["value"].ToObject<List<JObject>>();

                List<string> scriptList = new List<string>();
                foreach (var scriptFile in scriptFiles)
                {
                    scriptList.Add(scriptFile["Name"].ToString());
                }

                return scriptList;
            }
            else
            {
                throw new Exception($"Error fetching scripts from SharePoint: {response.StatusCode}");
            }
        }
    }
}