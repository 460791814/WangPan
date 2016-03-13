using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTool
{
   public  class DBUtil
    {
       /// <summary>
       /// 根据名称获取地区ID
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public int GetAreaId(string str)
       {
           switch (str)
           {
               case "中国":
                   return 1;
               case "日本":
                   return 2;
               case "韩国":
                   return 3;
               case "美国":
                   return 4;
               default:
                   return 0;
           }
       }
       /// <summary>
       /// 根据地区ID获取地区名称
       /// </summary>
       /// <param name="i"></param>
       /// <returns></returns>
       public string GetAreaById(int i)
       {
           switch (i)
           {
               case 1:
                   return "中国";
               case 2:
                   return "日本";
               case 3:
                   return "韩国";
               case 4:
                   return "美国";

               default:
                   return "未知";
           }
       }
       /// <summary>
       /// 根据类型名称获取ID
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public int GetTypeId(string str)
       {
           switch (str)
           {
               case "电影":
                   return 1;
               case "音乐":
                   return 2;
               case "程序":
                   return 3;
               case "文档":
                   return 4;
               default:
                   return 0;
           }
       }
       /// <summary>
       /// 根据类型ID获取名称
       /// </summary>
       /// <param name="i"></param>
       /// <returns></returns>
       public string GetTypeById(int i)
       {
           switch (i)
           {
               case 1:
                   return "电影";
               case 2:
                   return "音乐";
               case 3:
                   return "程序";
               case 4:
                   return "文档";

               default:
                   return "未知";
           }
       }
       /// <summary>
       /// 返货资料的类型样式
       /// </summary>
       /// <param name="i"></param>
       /// <returns></returns>
       public string GetSoftTypeStr(int i)
       {
           switch (i)
           {
               case 1:
                   return "doc";
               case 2:
                   return "mp3";
               case 3:
                   return "swf";
               default:
                   return "other";

           }
       }
    }
}
