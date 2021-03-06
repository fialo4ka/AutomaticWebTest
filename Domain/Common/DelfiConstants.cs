﻿using OpenQA.Selenium;

namespace Domain.Common
{
    public class DelfiConstants
    {
        public static string COURSE_HOME_PAGE = "http://javaguru.lv/";

        public static string DELFI_HOME_PAGE = "http://rus.delfi.lv/";
        public static string DELFI_HOME_PAGE_MOB = "http://m.rus.delfi.lv/";

        public static int NEWS_COUNT_TO_COMPARE = 5;

        //main main
        public static By XPATH_DIV = By.XPath("//h3[@class='top2012-title']");
        public static By XPATH_TITLES = By.XPath(".//a[@class='top2012-title']");
        public static By XPATH_ARTICLES_COMMENT_COUNT = By.XPath(".//a[@class='comment-count']");

        //mobile main
        public static By XPATH_DIV_MOB = By.XPath("//div[@class='md-mosaic-title']");
        public static By XPATH_ARTICLE_MOB = By.XPath(".//a[@class='md-scrollpos']");
        public static By XPATH_ARTICLES_COMMENT_COUNT_MOB = By.XPath(".//*[@class='commentCount']");

        //pages
        public static By XPATH_PAGE_TITLE = By.XPath("//div[@class='article-title']/h1");
        public static By XPATH_PAGE_COMMENT_COUNT = By.XPath("//a[@class='comment-count']");

        public static By XPATH_PAGE_COMMENT_COUNT_MOB = By.XPath("//*[@class='commentCount']");
    }
}
