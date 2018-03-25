using Domain.Common;
using Domain.Models;
using Domain.Service;
using log4net;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebTest
{
    public class DelfiArticleCompare
    {
        public List<DelfiArticleModel> Main { get; set; }

        public List<DelfiArticleModel> Mobile { get; set; }

        public List<DelfiArticleModel> PageList { get; set; }

        public List<DelfiArticleModel> PageMobList { get; set; }

        private DelfiService _delfiService;
		private static readonly ILog log = LogManager.GetLogger("FileAppender");

		public DelfiArticleCompare()
        {
            _delfiService = new DelfiService();
            GetMainArticleList();
            GetMobileArticleList();
            PageList = GetArticlesFromPages(Main);
            PageMobList = GetArticlesFromPages(Mobile, true);
            _delfiService.Closebrowser();
            Console.WriteLine("****All tests finished");
        }

        [Test]
        public void GetMainArticleList()
        {
			log.Info("tests start for GetMainArticleList");
            Main = _delfiService.GetMainPageCompareModel(DelfiConstants.DELFI_HOME_PAGE, DelfiConstants.XPATH_DIV,
                DelfiConstants.XPATH_TITLES, DelfiConstants.XPATH_ARTICLES_COMMENT_COUNT, out var exc);
            var msg = Main != null && Main.Any() && String.IsNullOrEmpty(exc) ?
                (Main.Where(x => String.IsNullOrEmpty(x.Title)).Any() ? "Test finished, but an error ocured in some articles Title"
                : $"Test sucssesfully finidhed") : $"Test not finished: {exc}";
			log.Info($"****Get Main Article List: {msg}");
        }

        [Test]
        public void GetMobileArticleList()
        {
			log.Info("tests start for GetMainArticleList");
			Mobile = _delfiService.GetMainPageCompareModel(DelfiConstants.DELFI_HOME_PAGE_MOB,DelfiConstants.XPATH_DIV_MOB,
                DelfiConstants.XPATH_ARTICLE_MOB, DelfiConstants.XPATH_ARTICLES_COMMENT_COUNT_MOB, out var exc);
            var msg = Mobile != null && Mobile.Any() && String.IsNullOrEmpty(exc) ?
                (!Mobile.Where(x => String.IsNullOrEmpty(x.Title)).Any() ? $"Test sucssesfully finidhed"
                    : "Test finished, but an error ocured in some articles Title")
                : $"Test not finished: {exc}";
			log.Info($"****Get Mobile Article List: {msg}");
        }

        [Test]
        public List<DelfiArticleModel> GetArticlesFromPages(List<DelfiArticleModel> model, bool isMob = false)
        {
			log.Info("tests start for GetArticlesFromPages");

			var resultList = new List<DelfiArticleModel>();
            if (!model.Any())
                return null;
            for (var i = 0; i < DelfiConstants.NEWS_COUNT_TO_COMPARE; i++)
            {
                if (model[i] != null && !String.IsNullOrEmpty(model[i].Path))
                {
                    var item = _delfiService.GetDelfiArticleModel(model[i].Path,
                        DelfiConstants.XPATH_PAGE_TITLE, isMob ? DelfiConstants.XPATH_PAGE_COMMENT_COUNT_MOB : DelfiConstants.XPATH_PAGE_COMMENT_COUNT, out var exc);
                    if (!String.IsNullOrEmpty(exc)) log.Info($"for {i}- article an error accured {exc}");
					resultList.Add(item);
                }
            }
			log.Info("tests finished for GetArticlesFromPages");
			return resultList;
        }
    }
}
