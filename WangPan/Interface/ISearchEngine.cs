using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WangPan.Models;

namespace WangPan.Interface
{
   public  interface ISearchEngine
    {
         /// <summary>
        /// 获取结果信息
        /// </summary>
        /// <returns></returns>
       string GetResultStatus();
       List<E_Content> GetContent();
       string GetPage();
    }
}
