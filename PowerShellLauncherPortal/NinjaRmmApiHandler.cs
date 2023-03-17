namespace PowerShellLauncherPortal
{
    public class NinjaRmmApiHandler
    {
        private const string NinjaRmmApiUrl = "https://api.ninjarmm.com/v2/";
        private const string NinjaRmmApiKey = "your-ninjarmm-api-key";
        private const string NinjaRmmApiSecret = "your-ninjarmm-api-secret";

        public async Task<List<string>> GetComputerListAsync()
        {
            var client = new RestClient(NinjaRmmApiUrl);
            var request = new RestRequest("devices", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-Key", NinjaRmmApiKey);
            request.AddHeader("X-API-Secret", NinjaRmmApiSecret);

            var response = await client.ExecuteAsync(request);
            var devices = JsonConvert.DeserializeObject<List<   >>(
                response.Content);
                
            var computerList = new List<string>();
            foreach (var device in devices)
            {
                computerList.Add(device.Name);
            }
            
            return computerList;
        }   
    }
}

