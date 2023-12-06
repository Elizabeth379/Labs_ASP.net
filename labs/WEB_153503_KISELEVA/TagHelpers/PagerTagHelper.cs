using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_153503_KISELEVA.TagHelpers
{
    [HtmlTargetElement("Pager", Attributes = "current-page, total-pages, is-admin")]
    public class PagerTagHelper : TagHelper
    {
        private LinkGenerator _linkGenerator;
        private IHttpContextAccessor _httpContextAccessor;

        public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName]
        public int CurrentPage { get; set; }

        [HtmlAttributeName]
        public int TotalPages { get; set; }

        [HtmlAttributeName]
        public bool IsAdmin { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            TagBuilder result = new TagBuilder("ul");
            result.AddCssClass("pagination justify-content-center");

            var prev = CurrentPage == 1 ? 1 : CurrentPage - 1;
            var next = CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;

            var prevUrlAdmin = _linkGenerator.GetPathByPage(_httpContextAccessor.HttpContext, page: "/Products/Index", values: new { area="Admin", pageNo = prev });
            var prevUrl = IsAdmin ? prevUrlAdmin : _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, "index", "product", new {pageNo = prev}) ;

            var liPrev = new TagBuilder("li");
            liPrev.AddCssClass(CurrentPage == 1 ? "page-item disabled" : "page-item");
            var aPrev = new TagBuilder("a");
            aPrev.AddCssClass("page-link");
            aPrev.MergeAttribute("href", prevUrl);
            aPrev.InnerHtml.Append("«");
            liPrev.InnerHtml.AppendHtml(aPrev);
            result.InnerHtml.AppendHtml(liPrev);

            for (int i = 1; i <= TotalPages; i++)
            {
                var li = new TagBuilder("li");
                li.AddCssClass(i == CurrentPage ? "page-item active" : "page-item");
                var a = new TagBuilder("a");
                a.AddCssClass("page-link");

                var urlAdmin = _linkGenerator.GetPathByPage(_httpContextAccessor.HttpContext, page: "/Products/Index", values: new { area = "Admin", pageNo = i });
                var url = IsAdmin ? urlAdmin : _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, "index", "product", new { pageNo = i });

                a.MergeAttribute("href", url);
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                result.InnerHtml.AppendHtml(li);
            }

            var liNext = new TagBuilder("li");
            liNext.AddCssClass(CurrentPage == TotalPages ? "page-item disabled" : "page-item");
            var aNext = new TagBuilder("a");
            aNext.AddCssClass("page-link");

            var nextUrlAdmin = _linkGenerator.GetPathByPage(_httpContextAccessor.HttpContext, page: "/Products/Index", values: new { area = "Admin", pageNo = next });
            var nexrUrl = IsAdmin ? nextUrlAdmin : _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, "index", "product", new { pageNo = next });

            aNext.MergeAttribute("href", nexrUrl);
            aNext.InnerHtml.Append("»");
            liNext.InnerHtml.AppendHtml(aNext);
            result.InnerHtml.AppendHtml(liNext);

            output.Content.AppendHtml(result);
        }
    }
}

