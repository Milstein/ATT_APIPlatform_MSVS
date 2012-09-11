namespace AttNewTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using AttNewTest.Enums;
    using AttNewTest.Parser;
    using AttNewTest.Parser.ParserTypes;

    public class MySmsService 
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public Uri EndPoint { get; set; }
        
        public Uri Address { get; set; }

        public Uri ProxyAddress { get; set; }

        public OAuthToken ClientCredential { get; set; }

        public SmsResponse SendSms(Sms sms)
        {
            if (sms.Addresses.Count == 0 || string.IsNullOrEmpty(sms.Message))
            {
                throw new ArgumentException("Invalid sms format.");
            }

            foreach (var address in sms.Addresses)
            {
                sms.TelAdresses = new List<string>();
                sms.TelAdresses.Add(this.IsValidMSISDN(address));
            }

            var smsForSending = new OutBoundSms { Address = sms.TelAdresses, Message = sms.Message };
            string body = JsonParser<OutBoundSms>.SerializeToJson(smsForSending);

            string relativeUri = string.Format("{0}{1}",EndPoint.AbsoluteUri,"/rest/sms/2/messaging/outbox");
            var head = new StringBuilder();
            this.GetClientCredentials();
            head.Append(string.Format("?Authorization=Bearer {0}", this.ClientCredential.AccessToken));

            return SmsResponse.Parse(this.Send(relativeUri, head, body));
        }

        #region Private non-refactored yet members

        private string IsValidMSISDN(string number)
        {
            string input = number;
            const string Pattern = @"^\d{3}-\d{3}-\d{4}$";
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentException("invalid number");
            }

            string str2 = Regex.IsMatch(input, Pattern) ? input.Replace("-", string.Empty) : input;

            long result = 0L;
            if ((str2.Length == 0x10) && str2.StartsWith("tel:+1"))
            {
                str2 = str2.Substring(6, 10);
            }
            else if ((str2.Length == 15) && str2.StartsWith("tel:1"))
            {
                str2 = str2.Substring(5, 10);
            }
            else if ((str2.Length == 14) && str2.StartsWith("tel:"))
            {
                str2 = str2.Substring(4, 10);
            }
            else if ((str2.Length == 12) && str2.StartsWith("+1"))
            {
                str2 = str2.Substring(2, 10);
            }
            else if ((str2.Length == 11) && str2.StartsWith("1"))
            {
                str2 = str2.Substring(1, 10);
            }
            if ((str2.Length != 10) || !long.TryParse(str2, out result))
            {
                throw new ArgumentException("Invalid phone number");
            }
            number = number.Replace("-", string.Empty);
            return (number.StartsWith("tel:") ? number : ("tel:" + number));
        }

        private string Send(string relativeUri, StringBuilder headers, string body)
        {
            var method = HTTPMethods.POST;
            const string ContentType = "application/json";
            const string Accept = "application/json";

            byte[] bodyBytes = null;
            if (!string.IsNullOrEmpty(body))
            {
                bodyBytes = new UTF8Encoding().GetBytes(body);
            }
            return (string)this.SendBytesArray(relativeUri, headers, bodyBytes);
        }

        private object SendBytesArray(string relativeUri, StringBuilder headers, byte[] bodyBytes, bool isSend = false)
        {
            HTTPMethods method = HTTPMethods.POST;
            var client = new HttpClient();
            string requestUri = string.Format("{0}{1}", EndPoint.AbsolutePath, relativeUri);
            if (isSend)
            {
				requestUri = string.Format("{0}{1}", Address, relativeUri);
            }

			client.BaseAddress = new Uri(requestUri);
            Task<HttpResponseMessage> response = null;

            if (string.IsNullOrEmpty(relativeUri))
            {
                throw new ArgumentException("relativeUri is empty");
            }

            try
            {
                switch (method)
                {
                    case HTTPMethods.POST:
                    case HTTPMethods.PUT:
                        HttpContent content = new ByteArrayContent(bodyBytes);
						response = client.PostAsync(requestUri, content);
                        break;
                 }
                
            }
            catch (WebException webException)
            {
                string body = string.Empty;
                try
                {
                    using (var reader2 = new StreamReader(webException.Response.GetResponseStream()))
                    {
                        body = reader2.ReadToEnd();
                    }
                }
                catch 
                {
                    body = "Unable to get response";
                }
                throw new InvalidResponseException("Failed: " + body, webException, (response != null) ? response.Result.StatusCode : HttpStatusCode.BadRequest, body);
            }
            catch (Exception exception)
            {
                throw new InvalidResponseException("Failed: " + exception.Message, exception);
            }

			var streamTask = (StreamContent)response.Result.Content;
			var text = new StreamReader(streamTask.ReadAsStreamAsync().Result);
			text.ReadToEnd();
            return response.Result;
        }

        private void GetClientCredentials()
        {
            if ((this.ClientCredential == null) || this.ClientCredential.IsExpired())
            {
                if ((this.ClientCredential != null) && (this.ClientCredential.CreationTime > DateTime.Now.AddSeconds(-86400.0)))
                {
                    try
                    {
                        this.ClientCredential = this.GetRefreshedClientCredential();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    this.ClientCredential = this.GetRefreshedAuthorizeCredential();
                }
            }
        }

        private OAuthToken GetRefreshedClientCredential()
        {
            StringBuilder auth = new StringBuilder();
            auth.Append(string.Format("{0}{1}", Address, "/oauth/AccessToken"));
            auth.Append(string.Format("?client_id={0}", this.ClientId));
            auth.Append(string.Format("&client_secret={0}", this.ClientSecret));
            auth.Append("&grant_type=client_credentials");
            auth.Append("&scope=SMS");
            return OAuthToken.ParseJSON(this.DoPost("/oauth/AccessToken", auth));
        }

        private OAuthToken GetRefreshedAuthorizeCredential()
        {
            StringBuilder auth = new StringBuilder();
            auth.Append(string.Format("{0}{1}", Address, "/oauth/AccessToken"));
            auth.Append(string.Format("?client_id={0}", this.ClientId));
            auth.Append(string.Format("&client_secret={0}", this.ClientSecret));
            auth.Append("&grant_type=client_credentials");
            auth.Append("&scope=SMS");
            return OAuthToken.ParseJSON(this.DoPost("oauth/token", auth));
        }

        private string DoPost(string relativeUri, StringBuilder auth)
        {
            return this.Send(HTTPMethods.POST, relativeUri, auth);
        }

        private string Send(HTTPMethods method, string relativeUri, StringBuilder parameters)
        {
            return this.Send(method, relativeUri, parameters, "application/json");
        }

        private string Send(HTTPMethods method, string relativeUri, StringBuilder parameters, string accept)
        {
            string body = parameters.ToString();
            return this.Send(method, relativeUri, null, body, "application/x-www-form-urlencoded", accept);
        }

        private string Send(HTTPMethods method, string relativeUri, StringBuilder headers, string body, string contentType, string accept)
        {
            byte[] bodyBytes = null;
            if (!string.IsNullOrEmpty(body))
            {
                bodyBytes = new UTF8Encoding().GetBytes(body);
            }
            return (string)this.SendBytesArray(relativeUri, headers, bodyBytes, true);
        }

        #endregion
    }
}
