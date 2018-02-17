using Domain.Common;
using Domain.Models;
using Domain.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebTest
{
    public class DelfiArticleCompare 
	{
		public MainPageCompareModel Main { get; set; }

        public MainPageCompareModel Mobile { get; set; }

        public List<DelfiArticleModel> PageList { get; set; }

        public List<DelfiArticleModel> PageMobList { get; set; }

        private static int newsCountToCompare = 5;
        private DelfiService _delfiService;

        public DelfiArticleCompare()
        {
            _delfiService = new DelfiService();
            GetMainArticleList();
            PageList = GetArticlesFromPages(Main);
            GetMobileArticleList();
            PageMobList = GetArticlesFromPages(Mobile, true);
        }

        [Test]
        public string GetMainArticleList()
        {
            Main = _delfiService.GetMainPageCompareModel(DelfiConstants.DELFI_HOME_PAGE, 
                DelfiConstants.XPATH_TITLES, DelfiConstants.XPATH_ARTICLES_COMMENT_COUNT);
			var msg = Main.DelfiArticle.Any() ?
				(Main.DelfiArticle.Where(x => String.IsNullOrEmpty(x.Title)).Any() ? "Test finished, but an error ocured in some articles Title" 
				: $"Test sucssesfully finidhed") : "Test not finished";
			return $"Get Mobile Article List: {msg}";
		}

		[Test]
		public string GetMobileArticleList()
		{
			Mobile = _delfiService.GetMainPageCompareModel(DelfiConstants.DELFI_HOME_PAGE_MOB, 
                DelfiConstants.XPATH_ARTICLE_MOB, DelfiConstants.XPATH_ARTICLES_COMMENT_COUNT_MOB);
			var msg = Mobile.DelfiArticle.Any() ? 
				(!Mobile.DelfiArticle.Where(x => String.IsNullOrEmpty(x.Title)).Any() ? $"Test sucssesfully finidhed" 
					: "Test finished, but an error ocured in some articles Title") 
				: "Test not finished";
			return $"Get Mobile Article List: {msg}";
		}

        [Test]
        public List<DelfiArticleModel> GetArticlesFromPages(MainPageCompareModel model, bool mob = false)
        {
            var resultList = new List<DelfiArticleModel>();
            if (model == null || !model.DelfiArticle.Any())
                return null;
            for (var i =0; i<newsCountToCompare; i++)
            {
                if (model.DelfiArticle[i] != null && !String.IsNullOrEmpty(model.DelfiArticle[i].Path))
                {
                    var item = _delfiService.GetDelfiArticleModel(model.DelfiArticle[i].Path,
                        DelfiConstants.XPATH_PAGE_TITLE, DelfiConstants.XPATH_PAGE_COMMENT_COUNT);
                    resultList.Add(item);
                }     
            }
            return resultList;
        }
    }
}
