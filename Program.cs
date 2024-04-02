// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Vert_Interview_Project;
using Vert_Interview_Project.Model.Response;
using Vert_Interview_Project.Service;

Console.WriteLine("Welcome to Vertmarkets Inc. Magazine Store.");
var vetex_Base_Url = Environment.GetEnvironmentVariable("Vetex_Base_Url") ?? "";
IVertMagazineService vertMagazineService = new VertMagazineService(vetex_Base_Url);
if(string.IsNullOrEmpty(vertMagazineService.token))
{
    Console.WriteLine("Sorry Vertmarkets Inc. Magazine Store is Unavailable. Sorry for the inconvenience");
}
var _business = new Business(vertMagazineService);
while (true)
{
    Console.WriteLine("Please Choose the option to See the Data:");
    Console.WriteLine("1. Categories:");
    Console.WriteLine("2. Magazine By Category wise:");
    Console.WriteLine("3. Subscribers:");
    Console.WriteLine("4. Identify all subscribers that are subscribed to at least one magazine in each category:");

    string value = Console.ReadLine() ?? "";
    if (value == "1")
    {
        Console.WriteLine("List of Categories:");
        await _business.GetCategoryAndDisplayOnBoard();
    }
    else if (value == "2")
    {
        var categoryList = await _business.GetCategoryAndDisplayOnBoard(false);
        Console.WriteLine("List of Magazine based on category:");
        await _business.GetTheCatgegoryBasedMagazineAndDisplayOnBoard(categoryList);
    }
    else if (value == "3")
    {
        Console.WriteLine("List of Subscribers:");
        await _business.GetAllTheSubscriberWithMagazineAndCategoryAndDisplayOnBoard();
    }
    else if (value == "4")
    {
        Console.WriteLine("Identify all subscribers that are subscribed to at least one magazine in each category:");
        await _business.IdenityTheEachCategoryBasedMazaineUserAndPostTheAnswer();
    }
    Console.WriteLine("\n\n");
}
