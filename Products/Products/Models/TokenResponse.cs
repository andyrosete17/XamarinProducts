
namespace Products.Models
{
    using Newtonsoft.Json;
    using System;

    public class TokenResponse
    {
        /// <summary>
        /// JsonProperty propertyname indicate the name of the property received, and them you can change the name
        /// </summary>
        [JsonProperty(PropertyName ="access_token")]
        public string  AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string  Username { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
    }
}
