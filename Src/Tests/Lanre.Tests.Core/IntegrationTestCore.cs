// Copyright (c) Lanre. All rights reserved.

namespace Lanre.Tests.Core
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Lanre.Data.Contexts.Lanre;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Xunit;

    public class IntegrationTestCore
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly string _url;

        public IntegrationTestCore(string url)
            : this(url, typeof(TestApiStartup), null)
        {
        }

        protected IntegrationTestCore(string url, Type startup)
            : this(url, startup, null)
        {
        }

        protected IntegrationTestCore(string url, Type startup, Action<IServiceCollection> configureServices)
        {
            this._server = ServerFactory.Server(startup, configureServices);

            this.Context = this.Server.Host.Services.GetService(typeof(LanreContext)) as LanreContext;
            this._client = this.Server.CreateClient();
            this._url = url;
        }

        protected LanreContext Context { get; private set; }

        protected TestServer Server => this._server;

        protected HttpClient Client => this._client;

        protected string Url => this._url;

        protected void AddTokenHeader(string token)
        {
            this.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<TResult> GetAsync<TResult>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            var entities = await this.ActionAsync<TResult>(this.Client.GetAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<TResult> DeleteAsync<TResult, TData>(
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            var entities = await this.ActionAsync<TResult>(this.Client.DeleteAsync, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<TResult> PostAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            var entities = await this.ActionAsync<TResult, TData>(this.Client.PostAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        protected async Task<TResult> PutAsync<TResult, TData>(
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            var entities = await this.ActionAsync<TResult, TData>(this.Client.PutAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
            return entities;
        }

        // protected async Task<TResult> PatchAsync<TResult, TData>(
        //     TData data,
        //     string url = "",
        //     bool successStatusCode = true,
        //     HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
        //     bool deserialize = true)
        //     where TResult : class
        // {
        //     var entities = await this.ActionAsync<TResult, TData>(this.Client.PatchAsync, data, url, successStatusCode, expectedStatusCode, deserialize);
        //     return entities;
        // }
        protected async Task<TResult> ActionAsync<TResult, TData>(
            Func<string, HttpContent, Task<HttpResponseMessage>> action,
            TData data,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            url = this.FormatUrl(url);
            var serializedData = this.Serialize(data);
            var response = await action(url, serializedData);

            if (successStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }

            if (expectedStatusCode.HasValue)
            {
                Assert.Equal(expectedStatusCode, response.StatusCode);
            }

            if (deserialize)
            {
                var entities = await this.Deserialize<TResult>(response);
                return entities;
            }

            return null;
        }

        protected async Task<TResult> ActionAsync<TResult>(
            Func<string, Task<HttpResponseMessage>> action,
            string url = "",
            bool successStatusCode = true,
            HttpStatusCode? expectedStatusCode = HttpStatusCode.OK,
            bool deserialize = true)
            where TResult : class
        {
            url = this.FormatUrl(url);

            var response = await action(url);
            if (successStatusCode)
            {
                response.EnsureSuccessStatusCode();
            }

            if (expectedStatusCode.HasValue)
            {
                Assert.Equal(expectedStatusCode, response.StatusCode);
            }

            if (deserialize)
            {
                var entities = await this.Deserialize<TResult>(response);
                return entities;
            }

            return null;
        }

        protected StringContent Serialize<TData>(TData data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        protected async Task<TResult> Deserialize<TResult>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<TResult>(responseString);
            return entities;
        }

        protected string FormatUrl(string url)
        {
            return string.IsNullOrEmpty(url) ? this.Url : url;
        }
    }
}
