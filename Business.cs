using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vert_Interview_Project.Model.Response;
using Vert_Interview_Project.Service;

namespace Vert_Interview_Project
{
    /// <summary>
    /// Represents the business logic for handling magazine-related bussniess operations.
    /// </summary>
    public class Business
    {
        IVertMagazineService _vertMagazineService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="vertMagazineService">The instance to be injected.</param>
        public Business(IVertMagazineService vertMagazineService)
        {
            _vertMagazineService = vertMagazineService;
        }

        /// <summary>
        /// Retrieves categories and optionally displays them.
        /// </summary>
        /// <param name="isDisplay">Flag to determine if the categories should be displayed.</param>
        /// <returns>The list of categories.</returns>

        public async Task<List<string>> GetCategoryAndDisplayOnBoard(bool isDisplay = true)
        {
            var getCategoryList = await _vertMagazineService?.GetCategory();
            if (getCategoryList != null)
            {
                if (getCategoryList?.Data == null || getCategoryList?.Data?.Count <= 0)
                {
                    Console.WriteLine("Sorry No Categories available.");
                }
                if (isDisplay)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    int i = 1;
                    foreach (var category in getCategoryList?.Data)
                    {
                        Console.WriteLine(i.ToString() + ": " + category);
                        i++;
                    }
                    Console.ResetColor();
                }
            }
            return getCategoryList?.Data;
        }

        /// <summary>
        /// Retrieves magazines based on provided categories and optionally displays them.
        /// </summary>
        /// <param name="categoryList">The list of categories to retrieve magazines for.</param>
        /// <param name="isDisplay">Flag to determine if the magazines should be displayed.</param>
        /// <returns>The list of magazines.</returns>

        public async Task<List<GetMagazineResponseModel.DataModel>> GetTheCatgegoryBasedMagazineAndDisplayOnBoard(List<string> categoryList, bool isDisplay = true)
        {
            var getFullMagazineList = new List<GetMagazineResponseModel.DataModel>();
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var category in categoryList)
            {
                if (isDisplay)
                    Console.WriteLine("*Category:" + category);
                var getMagazineList = await _vertMagazineService?.GetMagazine(category);

                if (getMagazineList != null)
                {
                    if (isDisplay)
                    {
                        if (getMagazineList?.Data == null || getMagazineList?.Data?.Count <= 0)
                        {
                            Console.WriteLine("----Sorry No Magazine available for this Category.");
                        }
                        int i = 1;
                        foreach (var magazine in getMagazineList?.Data)
                        {
                            Console.WriteLine("    " + i.ToString() + ": " + magazine?.Name);
                            i++;
                        }
                    }
                    getFullMagazineList.AddRange(getMagazineList?.Data);
                }
            }
            Console.ResetColor();
            return getFullMagazineList;
        }

        /// <summary>
        /// Retrieves subscribers, their subscribed magazines, and displays them.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>

        public async Task GetAllTheSubscriberWithMagazineAndCategoryAndDisplayOnBoard()
        {
            var getSubscribersList = await _vertMagazineService?.GetSubscriber();
            if (getSubscribersList != null)
            {
                if (getSubscribersList?.Data == null || getSubscribersList?.Data?.Count <= 0)
                {
                    Console.WriteLine("Sorry No subscribers available.");
                }
                int i = 1;
                var categoryList = await GetCategoryAndDisplayOnBoard(false);
                var magzineList = await GetTheCatgegoryBasedMagazineAndDisplayOnBoard(categoryList, false);

                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var subscriber in getSubscribersList?.Data)
                {
                    Console.WriteLine(i.ToString() + ": " + subscriber?.FirstName + " " + subscriber?.LastName);
                    var subscriberMagzineList = magzineList?.Where(x => subscriber?.MagazineIds.Contains(x.Id) ?? false)?.ToList();
                    foreach (var subscriberMagzine in subscriberMagzineList)
                    {
                        Console.WriteLine("   " + subscriberMagzine?.Category + " - " + subscriberMagzine?.Name);
                    }
                    i++;
                }
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Identifies subscribers who have subscribed to all available categories and posts the result.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task IdenityTheEachCategoryBasedMazaineUserAndPostTheAnswer()
        {
            var getSubscribersList = await _vertMagazineService?.GetSubscriber();
            var identifiedSubscriber = new Vert_Interview_Project.Model.Request.PostAnswerRequestModel() { Subscribers = new List<string>() };
            if (getSubscribersList != null)
            {
                if (getSubscribersList?.Data == null || getSubscribersList?.Data?.Count <= 0)
                {
                    Console.WriteLine("Sorry No subscribers available.");
                }
                int i = 1;
                var categoryList = await GetCategoryAndDisplayOnBoard(false);
                var categoryListCount = categoryList?.Count ?? 0;
                var magzineList = await GetTheCatgegoryBasedMagazineAndDisplayOnBoard(categoryList, false);

                foreach (var subscriber in getSubscribersList?.Data)
                {
                    //Console.WriteLine(i.ToString() + ": " + subscriber?.FirstName + " " + subscriber?.LastName);
                    var subscriberMagzineList = magzineList?.Where(x => subscriber?.MagazineIds.Contains(x.Id) ?? false)?.ToList();
                    var subscriberCategoryCount = subscriberMagzineList?.Select(x => x.Category).Distinct()?.Count();
                    if (categoryListCount == subscriberCategoryCount)
                    {
                        identifiedSubscriber?.Subscribers?.Add(subscriber?.Id);
                    }
                    i++;
                }
                var getAnswerList = await _vertMagazineService?.PostAnswer(identifiedSubscriber);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(JsonConvert.SerializeObject(getAnswerList?.Data));
                Console.ResetColor();
            }
        }
    }
}
