using Domain.Models;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
    public class DelfiService
    {
        private WebDriverSpecific driver;
        public DelfiService() => driver = new WebDriverSpecific();

        /// <summary>
        /// Get Main Page Compare Model
        /// </summary>
        /// <param name="homePage"></param>
        /// <param name="articleTitle"></param>
        /// <param name="articleCommentCount"></param>
        /// <returns></returns>
        public MainPageCompareModel GetMainPageCompareModel(string homePage, By articleTitle, By articleCommentCount)
        {
            var model = new MainPageCompareModel();
            driver.ChromeDriver.Navigate().GoToUrl(homePage);
            var delfiArticles = new List<DelfiArticleModel>();
            IReadOnlyCollection<IWebElement> elements = driver.ChromeDriver.FindElements(articleTitle);
            foreach (var item in elements)
            {
                var article = new DelfiArticleModel()
                {
                    Title = item?.Text ?? "",
                    CommentCount = item.FindElements(articleCommentCount).FirstOrDefault()?.Text ?? "(0)",
                    Path = item.GetAttribute("href")
                };
                delfiArticles.Add(article);
            }
            model.DelfiArticle = delfiArticles;
            return model;
        }

        public DelfiArticleModel GetDelfiArticleModel(string articleHref, By articleTitle, By articleCommentCount)
        {
            var model = new DelfiArticleModel() { Path = articleHref };
            driver.ChromeDriver.Navigate().GoToUrl(articleHref);
            model.Title = driver.ChromeDriver.FindElements(articleTitle).FirstOrDefault()?.Text ?? "";
            model.CommentCount = driver.ChromeDriver.FindElements(articleCommentCount).FirstOrDefault()?.Text ?? "(0)";

            return model;
        }
    }
}
