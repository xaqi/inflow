using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Data.Tieba
{
    public class TiebaService
    {
        public static Post[] GetPosts(string kw, int pageIndex)
        {
            kw = System.Web.HttpUtility.UrlEncode(kw, Encoding.GetEncoding("GB2312"));
            var url = $@"http://tieba.baidu.com/f/search/res?isnew=1&kw=&qw={kw}&rn=20&un=&only_thread=0&sm=1&sd=&ed=&pn={pageIndex + 1}";

            var bs = new WebClient().DownloadData(url);
            var html = Encoding.GetEncoding("gbk").GetString(bs);


            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument(html);
            var lastPage = document.QuerySelector(".pager .last");

            var items = document.QuerySelectorAll(".s_post").Where(div => !div.InnerHtml.Contains("�")).Select(div =>
            {
                var titleA = div.QuerySelector(".p_title .bluelink");
                var title = titleA.Text();
                var link = titleA.GetAttribute("href");
                var c = div.QuerySelector(".p_content");
                var content = div.QuerySelector(".p_content").Text();
                var tb = div.QuerySelector("a.p_forum");
                string tbName = "", tbLink = "";

                if (tb != null)
                {
                    tbName = tb.Text();
                    tbLink = tb.GetAttribute("href");
                }

                string authName = "", authLink = "";
                var auth = div.QuerySelectorAll(".p_violet").LastOrDefault();
                if (auth != null)
                {
                    authName = auth.Text();
                    authLink = (auth.Parent as IElement).GetAttribute("href");
                }
                var date = div.QuerySelector(".p_date")?.Text();
                return new Post(title, link, content, tbName, tbLink, authName, authLink, date);
            }).ToArray();
            return items;
        }
    }

    public class Post
    {
        public string Title { get; }
        public string Link { get; }
        public string Content { get; }
        public string TbName { get; }
        public string TbLink { get; }
        public string AuthName { get; }
        public string AuthLink { get; }
        public string Date { get; }

        public Post(string title, string link, string content, string tbName, string tbLink, string authName, string authLink, string date)
        {
            Title = title;
            Link = link;
            Content = content;
            TbName = tbName;
            TbLink = tbLink;
            AuthName = authName;
            AuthLink = authLink;
            Date = date;
        }
    }
}
