
namespace Products.Services
{
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Products.Helpers;
    using Products.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            var result = new Response();
            result.isSuccess = false;
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
                result.isSuccess = true;
            }

            return result;
        }

        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                                new StringContent(string.Format("grant_type=password&username={0}&password={1}", username, password), 
                                Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public async Task<Response> Get<T>(string urlBase, string servicePrefix,string controller, string tokenType, string accessToken, int id)
        {

            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);  
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, id);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    isSuccess = true,
                    Message = "OK",
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetList<T>(
                        string urlBase, 
                        string servicePrefix, 
                        string controller, 
                        string tokenType, 
                        string accessToken)
        {

            try
            {
                var client = new HttpClient();
                ///Para consumir el servicio con seguridad, solo usuarios desconocidos, por eso usa el token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = result,
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    isSuccess = true,
                    Message = "OK",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase, string servicePrefix, string controller,
            string tokenType, string accessToken, int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    isSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase, 
            string servicePrefix, 
            string controller, 
            string tokenType, 
            string accessToken, 
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.isSuccess = false;
                    return error;
                }
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    isSuccess = true,
                    Message = Languages.RecordAdded,
                    Result = newRecord
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> Post<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    isSuccess = true,
                    Message = Languages.RecordAdded,
                    Result = newRecord
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<Response> Put<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, model.GetHashCode());
                var response = await client.PutAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    isSuccess = true,
                    Message = Languages.RecordUpdated,
                    Result = newRecord
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response> Delete<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, T model)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, model.GetHashCode());
                var response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        isSuccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }
            
                return new Response
                {
                    isSuccess = true,
                    Message = Languages.RecordDeleted,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    isSuccess = false,
                    Message = ex.Message
                };
            }
        }

    }
}
