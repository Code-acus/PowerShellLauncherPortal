using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PowerShellLauncherPortal
{
    public class Device
    {
        public string DeviceName
        {
            get; 
            set;
        }
    }
    public class NinjaRmmApiHandler
    {
        private const string NinjaRmmApiUrl = "https://api.ninjarmm.com/v2/";
        private const string NinjaRmmApiKey = "your-ninjarmm-api-key";
        private const string NinjaRmmApiSecret = "your-ninjarmm-api-secret";
        
        public async Task<List<string>> GetComputerListAsync()
        {
            var client = new RestClient(NinjaRmmApiUrl);
            var request = new RestRequest("devices", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-Key", NinjaRmmApiKey);
            request.AddHeader("X-API-Secret", NinjaRmmApiSecret);

            var response = await client.ExecuteAsync(request);
            var devices = JsonConvert.DeserializeObject<List<Device>>(response.Content);
            
            var computerList = new List<string>();
            foreach (var device in devices)
            {
                computerList.Add(device.DeviceName);
            }

            return computerList;
        }
    }
}
    