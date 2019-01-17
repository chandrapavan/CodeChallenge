using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VertMarkets.RequestResponseInfo;

namespace VertMarkets
{
    public class RestSharpImp
    {


        public static async Task<TO> CallApiGenericGetMethod<TO>(IRequestResponseInfo<TO> input, string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteGetTaskAsync(request);
            return  new JsonDeserializer().Deserialize<TO>(response);
        }

        public static async Task<TO> CallApiGenericPostMethod<TI,TO>(IRequestResponseInfo<TI, TO> input, string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(input.Payload);
            var response = await client.ExecutePostTaskAsync(request);
            return new JsonDeserializer().Deserialize<TO>(response);

        }

    }
}
