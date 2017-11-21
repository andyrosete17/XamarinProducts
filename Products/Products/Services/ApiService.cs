
namespace Products.Services
{
    using Plugin.Connectivity;
    using Products.Helpers;
    using Products.Models;
    using System.Threading.Tasks;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            var result = new Response();
            result.isSucess = false;
            if (!CrossConnectivity.Current.IsConnected)
            {
                result.Message = Languages.InternetError;
            }

            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!response)
            {
                result.Message = Languages.InternetSettingsError;
            }
            else
            {
                result.isSucess = true;
            }

            return result;
        }
    }
}
