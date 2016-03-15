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
       List<E_Content> GetContent();
       string GetPage();
    }
}
