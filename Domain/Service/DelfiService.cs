using Domain.Models;
using OpenQA.Selenium;
using System;
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
        public List<DelfiArticleModel> GetMainPageCompareModel(string homePage, By mainDiv, By articleTitle, By articleCommentCount, out string exeption)
        {
            exeption = string.Empty;
            driver.ChromeDriver.Navigate().GoToUrl(homePage);
            var delfiArticles = new List<DelfiArticleModel>();
            try
            {
                IReadOnlyCollection<IWebElement> elements = driver.ChromeDriver.FindElements(mainDiv);
                foreach (var item in elements)
                {
                    var article = new DelfiArticleModel()
                    {
                        Title = item.FindElements(articleTitle).FirstOrDefault()?.Text ?? "",
                        CommentCount = item.FindElements(articleCommentCount).FirstOrDefault()?.Text ?? "(0)",
                        Path = item.FindElements(articleTitle).FirstOrDefault().GetAttribute("href")
                    };
                    delfiArticles.Add(article);
                }
            }
            catch (Exception e)
            {
                exeption = e.ToString();
            }
            return delfiArticles;
        }

        public DelfiArticleModel GetDelfiArticleModel(string articleHref, By articleTitle, By articleCommentCount, out string exc)
        {
            exc = string.Empty;
            var model = new DelfiArticleModel() { Path = articleHref };
            try
            {
                driver.ChromeDriver.Navigate().GoToUrl(articleHref);
                model.Title = driver.ChromeDriver.FindElements(articleTitle).FirstOrDefault()?.Text ?? "";
                model.CommentCount = driver.ChromeDriver.FindElements(articleCommentCount).FirstOrDefault()?.Text ?? "(0)";
            }
            catch(Exception e)
            {
                exc = e.ToString();
            }
            return model;
        }
        public void Closebrowser()
        {
            driver.ChromeDriver.Close();
        }
    }
}
