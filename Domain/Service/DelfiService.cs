using Domain.Models;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
	public class DelfiService
	{
		private WebDriverSpecific driver;
		private static readonly ILog log = LogManager.GetLogger("FileAppender");
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
			var delfiArticles = new List<DelfiArticleModel>();
			if (!CheckString(homePage))
				return delfiArticles;
			driver.ChromeDriver.Navigate().GoToUrl(homePage);
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
				log.Info(e);
				exeption = e.ToString();
			}
			return delfiArticles;
		}

		public DelfiArticleModel GetDelfiArticleModel(string articleHref, By articleTitle, By articleCommentCount, out string exc)
		{
			exc = string.Empty;
			var model = new DelfiArticleModel();
			if (!CheckString(articleHref))
				return model;
			model.Path = articleHref;
			try
			{
				driver.ChromeDriver.Navigate().GoToUrl(articleHref);
				model.Title = driver.ChromeDriver.FindElements(articleTitle).FirstOrDefault()?.Text ?? "";
				model.CommentCount = driver.ChromeDriver.FindElements(articleCommentCount).FirstOrDefault()?.Text ?? "(0)";
			}
			catch (Exception e)
			{
				log.Info(e);
				exc = e.ToString();
			}
			return model;
		}
		public void Closebrowser()
		{
			driver.ChromeDriver.Close();
		}

		private bool CheckString(string path)
		{
			if (path.Contains("http://") ||  path.Contains("https://"))
				return true;
			return false;
		}
	}
}
