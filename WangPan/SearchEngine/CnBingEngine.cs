using CommonTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WangPan.Interface;
using WangPan.Models;

namespace WangPan.SearchEngine
{
    public class CnBingEngine : ISearchEngine
    {
        private int pageIndex;
        private string keyWord;
        private string htmlContent = "";

        public CnBingEngine()
        {

            try
            {
                var queryStr = new StringBuilder();

                foreach (
                   var name in
                    HttpContext.Current.Request.QueryString.Cast<string>()
                           .Where(name => (name + "").ToLower() != "page"))
                    queryStr.Append("&" + name + "=" +
                                  HttpContext.Current.Request.QueryString[name]);

                string url = "http://cn.bing.com/search?";

                url = url + queryStr.ToString().Replace("&amp;", "&");

                htmlContent = HttpHelper.SendGet(url);
                htmlContent = htmlContent.Replace("\n", "").Replace("&nbsp;", ""); ;
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 获取结果信息
        /// </summary>
        /// <returns></returns>
        public string GetResultStatus()
        {

            //获取结果
            string resultStatsReg = "<span class=\"sb_count\">(.*?)</span>";
            string resultStats = EngineTool.GetHtmlByReg(htmlContent, resultStatsReg);
            return resultStats;
        }
        public List<Models.E_Content> GetContent()
        {



            //获取内容
            string contentReg = "<ol id=\"b_results\">(.*?)</ol>";
            string content = EngineTool.GetHtmlByReg(htmlContent, contentReg);
            List<E_Content> contentList = new List<E_Content>();
            Regex r = new Regex("<li class=\"b_algo\".*?>(.*?)</li>");
            if (r.IsMatch(content))
            {
                var matcheArr = r.Matches(content);
                foreach (Match item in matcheArr)
                {
                    E_Content eContent = new E_Content();
                    string oneContent = item.Groups[1].Value;
                    string contentTitleReg = "<h2><a href=\"([\\s\\S]*?)\".*?>([\\s\\S]*?)</a></h2>";
                    eContent.title = EngineTool.GetHtmlByReg(oneContent, contentTitleReg, 2);
                    eContent.DataHref = EngineTool.GetHtmlByReg(oneContent, contentTitleReg, 1);
                    string contentIntroReg = "<p>(.*?)</p>";
                    eContent.Intro = EngineTool.GetHtmlByReg(oneContent, contentIntroReg, 1);
                    string citeReg = "<cite>(.*?)</cite>";
                    string cite = EngineTool.GetHtmlByReg(oneContent, citeReg, 1);
                    eContent.Cite = cite;
                    contentList.Add(eContent);
                }
            }
            return contentList;
        }

        public string GetPage()
        {
            //获取分页
            string pageContentReg = "<nav aria-label=\"navigation\" role=\"navigation\">.*?(<ul.*?>.*?</ul>)</nav>";
            string pageContent = EngineTool.GetHtmlByReg(htmlContent, pageContentReg, 1);


            return pageContent.Replace("search?", "Search/search?")
                .Replace("class=\"sw_prev\"", "class=\"n\"")
                .Replace("class=\"sw_next\"", "class=\"n\"")
                .Replace("class=\"sb_pagS\"", "class=\"this\"")
                .Replace("class=\"sb_pagP\"", "class=\"n\"")
                .Replace("class=\"sb_pagN\"", "class=\"n\"");
        }
    }
}