using Domain.Common;
using Domain.Helper;
using Domain.Models;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTest;

namespace ConsoleApp
{
    class Program
    {
		private static readonly ILog log = LogManager.GetLogger("FileAppender");

		static void Main(string[] args)
        {
			XmlConfigurator.Configure();

			log.Info("Start test.");
			var _delfiArticleCompare = new DelfiArticleCompare();
            int step;
            do
            {
                Console.WriteLine("****Please  chose next step: \n 1 - show results\n 2 - show results" +
                    "\n 3 - compare main pages results\n 4 - compare pages results\n 5 - exit");
                var str = Console.ReadLine();
                if (!int.TryParse(str, out step))
                    Console.WriteLine("Comand not valid");
                switch (step)
                {
                    case 1:
                        ShowResults(_delfiArticleCompare.Main,_delfiArticleCompare.Mobile);
                        break;
                    case 2:
                        ShowResults(_delfiArticleCompare.PageList, _delfiArticleCompare.PageMobList);
                        break;
                    case 3:
                        CompareMainPagesResults(_delfiArticleCompare);
                        break;
                    case 4:
                        ComparePagesResults(_delfiArticleCompare);
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Comand not valid");
                        break;
                }
            }
            while (step != 5);

            Console.ReadLine();
            Environment.Exit(0);
        }

        static void ShowResults(List<DelfiArticleModel> mainModel, List<DelfiArticleModel> mobModel)
        {
            if (mainModel != null && mobModel.Any())
            {
                Console.WriteLine("Main");
                for (var i = 0; i < DelfiConstants.NEWS_COUNT_TO_COMPARE; i++)
                {
                    Console.WriteLine($"[{i}] {mainModel[i]?.Title}\t{mainModel[i]?.CommentCount}");
                }
            }
            if (mobModel != null && mobModel.Any())
            {
                Console.WriteLine("Mobile");
                for (var i = 0; i < DelfiConstants.NEWS_COUNT_TO_COMPARE; i++)
                {
                    Console.WriteLine($"[{i}] {mobModel[i]?.Title}\t{mobModel[i]?.CommentCount}");
                }
            }
        }
        static void CompareMainPagesResults(DelfiArticleCompare delfiArticleCompare)
        {
            if (delfiArticleCompare.Main != null && delfiArticleCompare.Main.Any()
                && delfiArticleCompare.Mobile != null && delfiArticleCompare.Mobile.Any())
            {
                for (var i = 0; i < DelfiConstants.NEWS_COUNT_TO_COMPARE; i++)
                {
                    if (delfiArticleCompare.Mobile[i]?.Title == delfiArticleCompare.Main[i]?.Title)
                        Console.WriteLine($"{i} articles are equals");
                    else
                    {
                        Console.WriteLine($"{i} articles are NOT equals");
                        Console.WriteLine($"\tmain:{delfiArticleCompare.Main[i]?.Title}\tmobile:{delfiArticleCompare.Mobile[i]?.Title}");
                    }
                    if (delfiArticleCompare.Mobile[i]?.CommentCount.FromStringToInt() == delfiArticleCompare.Main[i]?.CommentCount.FromStringToInt())
                        Console.WriteLine($"{i} comment count are equals");
                    else
                    {
                        Console.WriteLine($"{i} comment count are NOT equals");
                        Console.WriteLine($"\tmain:{delfiArticleCompare.Main[i]?.CommentCount}\tmobile:{delfiArticleCompare.Mobile[i]?.CommentCount}");
                    }
                }
                return;
            }
            Console.WriteLine("Error occured in models");
        }

        static void ComparePagesResults(DelfiArticleCompare delfiArticleCompare)
        {
            if (delfiArticleCompare.PageList.Any() && delfiArticleCompare.PageMobList.Any())
            {
                for (var i = 0; i < DelfiConstants.NEWS_COUNT_TO_COMPARE; i++)
                {
                    if (delfiArticleCompare.PageList[i]?.Title == delfiArticleCompare.PageMobList[i]?.Title)
                        Console.WriteLine($"{i} articles are equals");
                    else
                    {
                        Console.WriteLine($"{i} articles are NOT equals");
                        Console.WriteLine($"\tmain:{delfiArticleCompare.PageList[i]?.Title}\tmobile:{delfiArticleCompare.PageMobList[i]?.Title}");
                    }
                    if (delfiArticleCompare.PageList[i]?.CommentCount.FromStringToInt() == delfiArticleCompare.PageMobList[i]?.CommentCount.FromStringToInt())
                        Console.WriteLine($"{i} comment count are equals {delfiArticleCompare.PageList[i]?.CommentCount.FromStringToInt()} {delfiArticleCompare.PageMobList[i]?.CommentCount.FromStringToInt()}");
                    else
					{
                        Console.WriteLine($"{i} comment count are NOT equals");
                        Console.WriteLine($"\tmain:{delfiArticleCompare.PageList[i]?.CommentCount}\tmobile:{delfiArticleCompare.PageMobList[i]?.CommentCount}");
                    }
                }
                return;
            }
            Console.WriteLine("Error occured in models");
        }

    }
}
