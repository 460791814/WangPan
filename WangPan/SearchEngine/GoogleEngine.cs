using CommonTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WangPan.Interface;
using WangPan.Models;

namespace WangPan.SearchEngine
{
    public class GoogleEngine : ISearchEngine
    {
        public int pageIndex;
        public string keyWord;
        public string htmlContent = "";
        public List<Models.E_Content> GetContent()
        {
            int googleIndex = (pageIndex - 1) * 10;
            htmlContent = HttpHelper.SendGet("https://www.google.com.hk/search?q=site:pan.baidu.com++" + System.Web.HttpUtility.UrlEncode(keyWord) + "&safe=strict&start=" + googleIndex + "&tbs=lr:lang_1zh-CN&lr=lang_zh-CN");
            htmlContent = htmlContent.Replace("\n", "").Replace("&nbsp;", ""); ;

           
            //获取结果
            string resultStatsReg = "<div id=\"resultStats\">(.*?)</div>";
            string resultStats = EngineTool.GetHtmlByReg(htmlContent, resultStatsReg);

            //获取内容
            string contentReg = "<div data-async-context=\"(.*?)\" id=\"ires\">(.*?)</div>.*?<!--z-->";
            string content = EngineTool.GetHtmlByReg(htmlContent, contentReg);
            List<E_Content> contentList = new List<E_Content>();
            Regex r = new Regex("<div class=\"g\">(.*?)<!--n--></div>");
            if (r.IsMatch(content))
            {
                var matcheArr = r.Matches(content);
                foreach (Match item in matcheArr)
                {
                    E_Content eContent = new E_Content();
                    string oneContent = item.Groups[1].Value;

                    string h3Reg = "<h3 class=\"r\">(.*?)</h3>";
                    string h3content = EngineTool.GetHtmlByReg(oneContent, h3Reg, 1);
                    string contentTitleReg = "<a href=\"([\\s\\S]*?)\".*?\">([\\s\\S]*?)</a>";
                    eContent.title = EngineTool.GetHtmlByReg(oneContent, contentTitleReg, 2);
                    eContent.DataHref = EngineTool.GetHtmlByReg(oneContent, contentTitleReg, 1);
                    string contentIntroReg = "<span class=\"st\">(.*?)</span>";
                    eContent.Intro = EngineTool.GetHtmlByReg(oneContent, contentIntroReg, 1);
                    contentList.Add(eContent);
                }
            }
            return contentList;
        }



        public string GetPage()
        {
            //获取分页
            string pageContentReg = "<div id=\"navcnt\">(.*?)</div>";
            string pageContent = EngineTool.GetHtmlByReg(htmlContent, pageContentReg);
            int pageCount = GetPage(pageContent);

            return EngineTool.ShowCenterPage(pageCount, 10, pageIndex, 10, "/Search/Search");
           
        }
        /// <summary>
        /// 获取页面的最后一个页标
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int GetPage(string content)
        {
            int lastPageIndex = 0;
            Regex r = new Regex("</span>(.*?)</a>");
            if (r.IsMatch(content))
            {
                var matcheArr = r.Matches(content);
                foreach (Match item in matcheArr)
                {
                    E_Content eContent = new E_Content();
                    string oneContent = item.Groups[1].Value;
                    int tempId = Utils.GetInt(oneContent);
                    if (tempId > lastPageIndex)
                    {
                        lastPageIndex = tempId;
                    }
                }
            }
            return lastPageIndex;
        }
        
    }
}