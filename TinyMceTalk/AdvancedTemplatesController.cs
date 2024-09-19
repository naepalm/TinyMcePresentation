using Microsoft.AspNetCore.Mvc;
using TinyMceTalk.Models;
using TinyMceTalk.Models.Generated;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace TinyMceTalk
{
    public class AdvancedTemplatesController : UmbracoApiController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        public AdvancedTemplatesController(IUmbracoContextAccessor umbracoContextAccessor) 
        { 
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        [HttpGet]
        public IEnumerable<AdvancedTemplateCategoryResponse>? GetTemplates()
        {
            var templates = new List<AdvancedTemplateCategoryResponse>();

            if (_umbracoContextAccessor != null && _umbracoContextAccessor.TryGetUmbracoContext(out var ctx))
            {
                var allCategories = ctx.Content?.GetAtRoot().OfType<DataFolder>().FirstOrDefault()?
                    .FirstChild<AdvancedTemplateFolder>()?.Children<AdvancedTemplateCategory>();

                if (allCategories != null)
                {
                    var categories = allCategories.WhereNotNull().Select(category => new AdvancedTemplateCategoryResponse
                    {
                        Id = Udi.Create("document", category.Key).ToString(),
                        Title = category.Name,
                        Items = category.Children<AdvancedTemplate>()?.WhereNotNull().Select(advTemplate => new AdvancedTemplateResponse
                        {
                            Id = Udi.Create("document", advTemplate.Key).ToString(),
                            Title = advTemplate.Name,
                            Content = advTemplate.TemplateContent?.ToString()
                        })
                    });

                    templates.AddRange(categories);
                }
            }

            return templates;
        }
    }
}
