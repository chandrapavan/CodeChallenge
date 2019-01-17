using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VertMarkets.Models;
using VertMarkets.RequestResponseInfo;
using VertMarkets.ResponseModels;
using VertMarkets.ResponseRequestModels;

namespace VertMarkets
{
    class Program
    {
        public static async Task Main(string[] args)
        {
           await  MyAsyncFunc();
        }

        public static async Task MyAsyncFunc()
        {
            var url = WebApiUrl.GetConnectionStringForToken();
            var tokenResponse = await RestSharpImp.CallApiGenericGetMethod(new TokenRequestResponseInfo(), url); 
            if (!tokenResponse.Success) return;
            var token = tokenResponse.Token;             
            var magazineIdsByCategories = GetMagazineIdsByCategoriesAsync(await GetCategoriesAsync(token), token); // we get categories with associated magazine lists
            var Ids = GetIdsSubscribedEachCategory(await GetSubscribersAsync(token), await magazineIdsByCategories); // Ids of customers who subscribed for atleast one magazine of each category
            var answerResponse = await PostAnswerToAPIAsync(Ids, token); // sends the response to api
            PrintPropertiesOfAnswerObject(answerResponse);
        }

        public static async Task<CategoryResponse> GetCategoriesAsync(string token)
        {
            var url = WebApiUrl.GetConnectionStringForCategories(token);
            var categoryResponse = await RestSharpImp.CallApiGenericGetMethod( new CategoryRequestResponseInfo(), url);
            return  categoryResponse;
        }

        public static async Task<List<Subscribers>> GetSubscribersAsync(string token)
        {
            var url = WebApiUrl.GetConnectionStringForSubscribers(token);
            var subscribersResponse = await RestSharpImp.CallApiGenericGetMethod(new SubscribersRequestResponseInfo(), url);
            return subscribersResponse.data.Select(x => new Subscribers { Id = x.id, MagazineIds = x.magazineIds }).ToList();
        }


        public static async Task<List<MagazineIdsByCategory>> GetMagazineIdsByCategoriesAsync(CategoryResponse categories,string token)
        {
            var magazineIdsByCategories = new List<MagazineIdsByCategory>();
            foreach(var category in categories.Data)
            {
                var url = WebApiUrl.GetConnectionStringForMagazines(token, category);
                var magazineResponse = await RestSharpImp.CallApiGenericGetMethod(new MagazineRequestResponseInfo(), url);
                var magazineIdByCategory = magazineResponse.Data.GroupBy(x => x.Category).
                    Select(x => new MagazineIdsByCategory
                    {
                        Name = x.Key,
                        Ids = x.Select(z => z.Id).ToList()
                    }).FirstOrDefault();

                magazineIdsByCategories.Add(magazineIdByCategory);
            }
            return magazineIdsByCategories;
        }


        public static  List<string> GetIdsSubscribedEachCategory(List<Subscribers> subscribers, List<MagazineIdsByCategory> magazineIdsByCategories)
        {
            var Ids = subscribers.Where(x => magazineIdsByCategories.All(y => y.Ids.Any(x.MagazineIds.Contains))).Select(x => x.Id).ToList();
            return Ids;
        }

        public static async Task<AnswerResponse> PostAnswerToAPIAsync(List<string> Ids, string token)
        {
            var url = WebApiUrl.GetConnectionStringForAnswer(token);
            var answerRequest = new AnswerRequest() {subscribers = Ids };
            var payload = new AnswerRequestResponseInfo(answerRequest);
            var response = await RestSharpImp.CallApiGenericPostMethod(payload, url);
            return response;
        }


        public static void PrintPropertiesOfAnswerObject(AnswerResponse answerResponse)
        {
            Console.WriteLine("answerCorrect: " + answerResponse.data.answerCorrect);
            Console.WriteLine("totalTime: " + answerResponse.data.totalTime);
            Console.WriteLine("shouldBe: " + answerResponse.data.shouldBe);
            Console.WriteLine("success: " + answerResponse.success);
            Console.WriteLine("token: " + answerResponse.token);
            Console.WriteLine("message: " + answerResponse.message);
            Console.ReadLine();
        }

    }
}
