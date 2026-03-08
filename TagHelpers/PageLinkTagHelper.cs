
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.TagHelpers;



[HtmlTargetElement("div", Attributes = "page-model")] // Bu tag helper'ı sadece "div" elementlerine ve "page-model" attribute'una sahip olanlara uygulamak istiyoruz.
public class PageLinkTagHelper : TagHelper
{
    private IUrlHelperFactory _urlHelperFactory;
    public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }
    [ViewContext] // ViewContext'i almak için bu attribute'u kullanıyoruz.
    public ViewContext? ViewContext{ get; set; }
    public PageInfo? PageModel { get; set; }
    public string PageAction { get; set; } = string.Empty;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && PageModel != null)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext); // URL oluşturmak için IUrlHelper'ı kullanıyoruz.
            TagBuilder result = new TagBuilder("div"); //
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
    
}