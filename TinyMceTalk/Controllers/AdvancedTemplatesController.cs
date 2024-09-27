using Microsoft.AspNetCore.Mvc;
using TinyMceTalk.Models;
using TinyMceTalk.Models.Generated;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace TinyMceTalk.Controllers
{
    public class AdvancedTemplatesController : UmbracoApiController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        public AdvancedTemplatesController(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public AdvancedTemplateResponse GetTemplate(string id)
        {
            var template = new AdvancedTemplateResponse();

            if (_umbracoContextAccessor != null && _umbracoContextAccessor.TryGetUmbracoContext(out var ctx))
            {
                if (Guid.TryParse(id, out var templateKey))
                {
                    var templateNode = ctx.Content?.GetById(templateKey);
                    if (templateNode != null && templateNode is AdvancedTemplate advancedTemplate)
                    {
                        template = new AdvancedTemplateResponse
                        {
                            Id = advancedTemplate.Key.ToString(),
                            Title = advancedTemplate.Name,
                            Content = advancedTemplate.TemplateContent?.ToString()
                        };
                    }
                }
            }

            return template;
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
                        Id = category.Key.ToString(),
                        Title = category.Name,
                        Items = category.Children<AdvancedTemplate>()?.WhereNotNull().Select(advTemplate => new AdvancedTemplateResponse
                        {
                            Id = advTemplate.Key.ToString(),
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
