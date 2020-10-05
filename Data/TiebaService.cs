using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Data
{
    public class TiebaService
    {
        public static object Get(string kw)
        {
            kw = System.Web.HttpUtility.UrlEncode(kw, Encoding.GetEncoding("GB2312"));
            return kw;
        }
    }
}
