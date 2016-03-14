using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WangPan.Models
{
    public class E_Content
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Intro
        {
            get;
            set;
        }
        /// <summary>
        /// url源地址
        /// </summary>
        public string DataHref
        {
            get;
            set;
        }
    }
}