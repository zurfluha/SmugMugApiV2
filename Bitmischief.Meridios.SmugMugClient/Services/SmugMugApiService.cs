using Bitmischief.Meridios.SmugMug.Entities.Authentication;
using Bitmischief.Meridios.SmugMug.Entities.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bitmischief.Meridios.SmugMug.Constants;
using System.Diagnostics;
using Bitmischief.OAuth;

namespace Bitmischief.Meridios.SmugMug.Services
{
    public class SmugMugApiService
    {
        private OAuthToken oauthToken;

        public SmugMugApiService(OAuthToken _token)
        {
            oauthToken = _token;
        }

        public async Task<TResult> GetRequestAsync<TResult>(string requestUri)
        {
            using (HttpClient httpClient = GenerateOAuthHttpClient(oauthToken))
            using (HttpResponseMessage response = await httpClient.GetAsync(requestUri))
            using (StreamReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var responseContent = await streamReader.ReadToEndAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Received error {(int)response.StatusCode} from the server! Response: {responseContent}.");
                }

                if (string.IsNullOrEmpty(responseContent))
                {
                    return default(TResult);
                }

                var responseObj = JsonConvert.DeserializeObject<BaseSmugMugRequest>(responseContent);
                JObject jobject = null;
                JArray jarray = null;

                if(responseObj.Code == HttpStatusCode.OK)
                {
                    if(responseObj.Response.Locator == "User")
                    {
                        jobject = responseObj.Response.User as JObject;
                    }
                    else if (responseObj.Response.Locator == "Album")
                    {
                        jobject = responseObj.Response.Album as JObject;
                    }
                    else if(responseObj.Response.Locator == "Node")
                    {
                        jobject = responseObj.Response.Node as JObject;

                        // If that didn't work, let's see if it's an array
                        if(jobject == null)
                        {
                            jarray = responseObj.Response.Node as JArray;
                        }
                    }
                    else if(responseObj.Response.Locator == "AlbumImage")
                    {
                        jobject = responseObj.Response.AlbumImage as JObject;

                        // If that didn't work, let's see if it's an array
                        if (jobject == null)
                        {
                            jarray = responseObj.Response.AlbumImage as JArray;
                        }
                    }
                }

                if(jobject == null && jarray == null)
                {
                    return default(TResult);
                }
                
                if(jobject != null)
                {
                    return jobject.ToObject<TResult>();
                }
                else if(jarray != null)
                {
                    return await HandleEnumerableResultSetAndPaging<TResult>(oauthToken, responseObj, jarray);
                }
                else
                {
                    throw new InvalidOperationException("This shouldn't happen!");
                }
            }
        }

        private async Task<TResult> HandleEnumerableResultSetAndPaging<TResult>(OAuthToken oauthToken, BaseSmugMugRequest responseObj, JArray jarray)
        {
            var type = typeof(TResult).GetGenericArguments()[0];

            Type genericListType = typeof(List<>).MakeGenericType(type);
            var myList = (IList)Activator.CreateInstance(genericListType);

            foreach (JObject obj in jarray)
            {
                var objInCorrectType = obj.ToObject(type);
                myList.Add(objInCorrectType);
            }

            if (responseObj.Response.Pages != null && responseObj.Response.Pages.NextPage != null)
            {
                var nextPageUri = $"{SmugMugConstants.Addresses.SmugMug}{responseObj.Response.Pages.NextPage}";
                var nextPageList = (IList)await GetRequestAsync<TResult>(nextPageUri);
                foreach (var obj in nextPageList)
                {
                    myList.Add(obj);
                }
            }

            return (TResult)myList;
        }

        public async Task PostRequestAsync(string requestUri, object obj)
        {
            var content = JsonConvert.SerializeObject(obj);

            using (HttpClient httpClient = GenerateOAuthHttpClient(oauthToken))
            using (HttpContent httpContent = new StringContent(content))
            using (HttpResponseMessage response = await httpClient.PostAsync(requestUri, httpContent))
            using (StreamReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var responseContent = await streamReader.ReadToEndAsync();
                if(!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Received error {(int)response.StatusCode} from the server! Response: {responseContent}.");
                }
            }
        }

        public async Task PatchRequestAsync(string requestUri, object obj)
        {
            var content = JsonConvert.SerializeObject(obj);

            using (HttpClient httpClient = GenerateOAuthHttpClient(oauthToken))
            using (HttpContent httpContent = new StringContent(content))
            using (HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri))
            {
                message.Content = httpContent;
                using (HttpResponseMessage response = await httpClient.SendAsync(message))
                using (StreamReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                {

                    var responseContent = await streamReader.ReadToEndAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException($"Received error {(int)response.StatusCode} from the server! Response: {responseContent}.");
                    }
                }
            }
        }

        public async Task DeleteRequestAsync(string requestUri)
        {            
            using (HttpClient httpClient = GenerateOAuthHttpClient(oauthToken))
            using (HttpResponseMessage response = await httpClient.DeleteAsync(requestUri))
            using (StreamReader streamReader = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                var headers = string.Join("\n", httpClient.DefaultRequestHeaders);
                Trace.WriteLine($"REQUEST HEADERS: {headers}");

                var responseContent = await streamReader.ReadToEndAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Received error {(int)response.StatusCode} from the server! Response: {responseContent}.");
                }
            }
        }

        private HttpClient GenerateOAuthHttpClient(OAuthToken oauthToken)
        {
            OAuthMessageHandler handler = new OAuthMessageHandler(oauthToken.ApiKey, oauthToken.Secret, oauthToken.Token, oauthToken.TokenSecret);
            HttpClient client = new HttpClient(handler);

            // Make sure we request JSON data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
