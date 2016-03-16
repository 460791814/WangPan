using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CommonTool;
using WangPan.Models;
using WangPan.Interface;
using WangPan.SearchEngine;

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
            ISearchEngine engine = null;
       
            string q = Request["q"];
            if (string.IsNullOrEmpty(q)) { return View(); }
            q = q.Trim().Replace("  ", " ");
            string[] qs = q.Split(' ');
            string hbtwd = "";
            engine = new CnBingEngine();
            hbtwd = qs[1];
            //switch (qs[0])
            //{
            //    case "site:pan.baidu.com":
            //        engine = new CnBingEngine();
            //        hbtwd =qs[1];
            //        break;
            //    default:
            //        break;
            //}

            StringBuilder wangPanNav = new StringBuilder();
            
            for (int i = 0; i < EngineTool.GuoNeiWangPanArr.Length; i++)
            {
                string[] temp=EngineTool.GuoNeiWangPanArr[i].Split('|');
                if (qs[0].Contains(temp[1]))
                {
                    wangPanNav.Append("<a class=\"current\" style=\" margin-left:10px;font-size:12px\" href=\"/Search/Search?q=site:"+temp[1]+" "+hbtwd
                        +"\">"+temp[0]+"</a>");
                }
                else {
                    wangPanNav.Append("<a style=\" margin-left:10px;font-size:12px\" href=\"/Search/Search?q=site:" + temp[1] + " " + hbtwd
                          + "\">" + temp[0] + "</a>");
                }
            }
            ViewBag.ResultStatus = engine.GetResultStatus();

            ViewData["contentlist"] = engine.GetContent();
            ViewBag.GuoNeiWangPan = wangPanNav;
            string pageHtml = engine.GetPage();

            ViewBag.hbtwd = hbtwd;
            ViewBag.pageHtml = pageHtml;


            return View();
        }




    }
}