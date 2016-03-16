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
            switch (qs[0])
            {
                case "site:pan.baidu.com":
                    engine = new CnBingEngine();
                    hbtwd =qs[1];
                    break;
                default:
                    break;
            }
            ViewBag.ResultStatus = engine.GetResultStatus();

            ViewData["contentlist"] = engine.GetContent();

            string pageHtml = engine.GetPage();

            ViewBag.hbtwd = hbtwd;
            ViewBag.pageHtml = pageHtml;


            return View();
        }




    }
}