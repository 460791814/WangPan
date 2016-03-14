using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CommonTool;
using WangPan.Models;

namespace WangPan.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
          
            int  pageIndex = Utils.GetInt(Request["page"]);

            if (pageIndex == 0) pageIndex = 1;
            string wd = Request["wd"]==null?"美人鱼":Request["wd"];
           int googleIndex=(pageIndex-1)*10;
           string htmlContent = HttpHelper.SendGet("https://www.google.com.hk/search?q=site:pan.baidu.com++" + System.Web.HttpUtility.UrlEncode(wd) + "&safe=strict&start=" + googleIndex + "&tbs=lr:lang_1zh-CN&lr=lang_zh-CN");
             htmlContent = htmlContent.Replace("\n", "").Replace("&nbsp;", "");;
              

            //获取结果
             string resultStatsReg = "<div id=\"resultStats\">(.*?)</div>";
             string resultStats = GetHtmlByReg(htmlContent, resultStatsReg);
           
            //获取内容
             string contentReg = "<div data-async-context=\"(.*?)\" id=\"ires\">(.*?)</div>.*?<!--z-->";
             string content = GetHtmlByReg(htmlContent, contentReg);
            List<E_Content> contentList=new List<E_Content>();
             Regex r = new Regex("<div class=\"g\">(.*?)<!--n--></div>");
             if (r.IsMatch(content))
             {
                 var matcheArr = r.Matches(content);
                 foreach (Match item in matcheArr)
                 {
                     E_Content eContent = new E_Content();
                     string oneContent = item.Groups[1].Value;

                     string h3Reg = "<h3 class=\"r\">(.*?)</h3>";
                     string h3content = GetHtmlByReg(oneContent, h3Reg, 1);
                     string contentTitleReg = "<a href=\"([\\s\\S]*?)\".*?\">([\\s\\S]*?)</a>";
                     eContent.title = GetHtmlByReg(oneContent, contentTitleReg, 2);
                     eContent.DataHref = GetHtmlByReg(oneContent, contentTitleReg, 1);
                     string contentIntroReg="<span class=\"st\">(.*?)</span>";
                     eContent.Intro = GetHtmlByReg(oneContent, contentIntroReg, 1);
                     contentList.Add(eContent);
                 }
             }
            
                 
           
            //获取分页
             string pageContentReg = "<div id=\"navcnt\">(.*?)</div>";
             string pageContent = GetHtmlByReg(htmlContent, pageContentReg);
           int pageCount=  GetPage(pageContent);
           ViewBag.wd = wd;
           ViewBag.pageHtml = ShowCenterPage(pageCount, 10, pageIndex, 10, "/Search/Search");
           ViewData["contentlist"] = contentList;
        
            return View();
        }
        public string GetHtmlByReg(string html, string reg, int i=0)
        {

            string result = "";
            Regex r = new Regex(reg);
            if (r.IsMatch(html))
            {
                Match m = r.Match(html);
                result = m.Groups[i].Value;
            }
            return result;
        }
        /// <summary>
        /// 获取页面的最后一个页标
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int  GetPage(string content)
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
        

        public  string ShowCenterPage(int pageCount, int pageSize, int currentPage, int displayNum,string linkPage)
        {
       

 

            var queryStr = new StringBuilder();
            var sNextPageNo = ""; //跳转的页码
            foreach (
               var name in
                  Request.QueryString.Cast<string>()
                       .Where(name => (name + "").ToLower() != "page"))
                queryStr.Append("&amp;" + name + "=" +
                              Server.UrlEncode(Request.QueryString[name]));
            //设置当前页
            if (pageCount < 1) pageCount = 1;
            if (currentPage < 1) currentPage = 1;
            if (currentPage > pageCount) currentPage = pageCount;
            var pageStr =
                new StringBuilder(
                    string.Format(
                        "<a  class=\"n\" href=\"{0}?page={1}{2}\">上一页</a>", linkPage,
                        currentPage == 1 ? 1 : currentPage - 1, queryStr));
            var iBegin = currentPage;
            var iEnd = currentPage;
            var i = displayNum;
            while (true)
            {
                if (iEnd < pageCount)
                {
                    iEnd++;
                    i--;
                }
                if (iBegin > 1 && i > 1)
                {
                    iBegin--;
                    i--;
                }
                if ((iBegin <= 1 && iEnd >= pageCount) || i <= 1) break;
            }

            #region//跳转页的页码

            if (displayNum == 1)
            {
                sNextPageNo = "";
            }
            sNextPageNo = currentPage >= pageCount ? "" : (currentPage + 1).ToString();

            #endregion

           pageStr.AppendFormat(" <a href=\"" + linkPage + "?page=1" + queryStr + "\"{0}>1{1}</a>",
               currentPage == 1 ? " class=this" : string.Empty, iBegin > 2 ? ".." : string.Empty);
            for (i = iBegin <= 2 ? 2 : iBegin; i < iEnd; i++)
            {
                if (i == currentPage)
                    pageStr.Append(" <a class=\"this\">" + i + "</a>");
                else
                    pageStr.Append(" <a href=\"" + linkPage + "?page=" + i + queryStr + "\">" + i + "</a> ");
            }

            if (pageCount > 1)
                pageStr.AppendFormat(
                    " <a href=\"" + linkPage + "?page=" + pageCount + queryStr + "\"{0}>{1}" + pageCount + "</a>",
                    currentPage == pageCount ? " class=this" : string.Empty, iEnd == pageCount ? string.Empty : "..");
            pageStr.AppendFormat(" <a class=\"n\" href=\"{0}?page={1}{2}\">下一页</a>", linkPage,
                currentPage == pageCount ? currentPage : currentPage + 1, queryStr);

            //pageStr.AppendFormat(" <a class=\"br-none\">共<i>{0}</i>页 到第</a>", pageCount);
            //pageStr.AppendFormat(
            //    " <a class=\"br-none\"><input class=\"paper\" type=\"text\" size=\"5\" maxlength=\"9\" value=\"{0}\" /></a>",
            //    sNextPageNo);
            //pageStr.Append(" <a class=\"br-none\">页</a>");
            //pageStr.Append(
            //    " <a class=\"ensure-bt\" href=\"javascript:void(0);\" onclick=\"if(!$(this).parent().find('.paper:text:last').val()){alert('请输入要跳转的页码');}else{location.href='" +
            //    linkPage + "?page='+$(this).parent().find(\'.paper:text:last\').val()+'" + queryStr + "';}\" >确定</a>");

            return pageStr.ToString();
        }
	}
}