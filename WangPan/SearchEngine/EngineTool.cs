using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WangPan.SearchEngine
{
    public  class EngineTool
    {
        /// <summary>
        /// 获取匹配结果
        /// </summary>
        /// <param name="html"></param>
        /// <param name="reg"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetHtmlByReg(string html, string reg, int i = 0)
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
       /// 分页
       /// </summary>
       /// <param name="pageCount"></param>
       /// <param name="pageSize"></param>
       /// <param name="currentPage"></param>
       /// <param name="displayNum"></param>
       /// <param name="linkPage"></param>
       /// <returns></returns>
        public static string ShowCenterPage(int pageCount, int pageSize, int currentPage, int displayNum, string linkPage)
        {
            var queryStr = new StringBuilder();
            var sNextPageNo = ""; //跳转的页码
            foreach (
               var name in
                HttpContext.Current.Request.QueryString.Cast<string>()
                       .Where(name => (name + "").ToLower() != "page"))
                queryStr.Append("&amp;" + name + "=" +
                              HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.QueryString[name]));
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

        /// <summary>
        /// 获取国内网盘集合
        /// </summary>
        /// <returns></returns>
        public static string[] GuoNeiWangPanArr = new string[] { 
        
			//"国内网盘|pan.baidu.com",
			"百度网盘|pan.baidu.com",
			"华为网盘|dl.vmall.com",
			"迅雷快传|kuai.xunlei.com",
			"QQ旋风|fenxiang.qq.com",
			"金山快盘|cloud.letv.com",
			"千脑网盘|qiannao.com",
			"360云盘|yunpan.cn",
		    "腾讯微云|share.weiyun.com",
			"一木禾网盘|yimuhe.com",
	     	"城通网盘|ctfile.com",
			"千军万马|7958.com",
			"YunFile网盘|yunfile.com",
	    	"vdisk威盘|vdisk.cn",
			"115网盘|115.com",
			"盛大网盘|www.colayun.com",
			"RayFile|www.rayfile.com",
        };
        public string[] GuoWaiWangPanArr = new string[] { 
           
			"国外网盘|www.dropbox.com",
			"hotfile.com|hotfile.com",
			"rapidshare.com|rapidshare.com",
			"oron.com|oron.com",
			"uploaded.to|uploaded.to",
			"easy-share.com|easy-share.com",
			"uploading.com|uploading.com",
			"turbobit.net|turbobit.net",
			"fileserve.com|fileserve.com",
			"enterupload.com|enterupload.com",
		 
        };
    }
}