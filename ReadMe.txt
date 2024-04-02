Introduction
Welcome to the Vertmarkets Inc. Magazine Store! This project provides a console-based interface for managing magazine categories, magazines, subscribers, and identifying subscribers subscribed to magazines across various categories. It's a .NET project designed to showcase basic functionalities related to magazine management within a simulated environment.

About the Developer
Hi there! I'm Manimaran, having 11 years of experiance as a developer. I have spent 2 hours for this project to complete. Hope i have covered all the scenarios which is requesed for the module. I'm passionate about software development and enjoy creating applications that solve real-world problems. With a background in .Net, C#, MVC and MongoDb, Still there are lot need to implement in this smaller project to explore my skils set, for the basic project setup i have done everything into a single project.

I have tried some extra logic to make the UI better and aLAO for performance i have included the some internal variable to store the data to avoid duplicate API hits.

Based on my test case, the max response time to 7sec and sequence execution will complete in 3 sec.

How to Run the Project:
1) Using the Vert-Interview-Project.sln file opne the project in Visual studio 
2) .Net core 6.0 need to be install before run this project
3) Once Project file is ready, Check the launchSettings.json file for environment variable - "Vetex_Base_Url": "http://magazinestore.azurewebsites.net/"
4) Run the project using dotnet run in terminal or Play button the visual studio
5) Once you ran, Console pop up will be displayed.

How to Use:

*On Console window it will show the below 4 options to Choose:

Please Choose the option to See the Data:
1. Categories:
2. Magazine By Category wise:
3. Subscribers:
4. Identify all subscribers that are subscribed to at least one magazine in each category

* Choose this option 1 to view the list of available categories
* Choose this option 2 to view magazines categorized by their respective categories
* Choose this option 3 to view the list of all subscribers along with the magazines they are subscribed to.
* Choose this option 4 to view the PostAnswer Result