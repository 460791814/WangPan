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
            int pageIndex = Utils.GetInt(Request["page"]);
            if (pageIndex == 0) pageIndex = 1;
            string wd = Request["wd"] ;
            ISearchEngine engine = new CnBingEngine(wd);
           
            ViewData["contentlist"] = engine.GetContent();

            string pageHtml = engine.GetPage();

            ViewBag.wd = wd;
            ViewBag.pageHtml = pageHtml;
         

            return View();
        }

    }
}