using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TinyMceTalk.Models;
using TinyMceTalk.Models.Generated;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using uSync.Core;

namespace TinyMceTalk.Controllers
{
    public class MentionsController : UmbracoApiController
    {
        private readonly IMemberService _memberService;
        private readonly IMemberManager _memberManager;

        public MentionsController(IMemberService memberService, IMemberManager memberManager)
        {
            _memberService = memberService;
            _memberManager = memberManager;
        }

        [HttpGet]
        public async Task<Mention?> GetMember(string id)
        {
            var mention = new Mention();

            var member = await _memberManager.FindByIdAsync(id);

            if (member != null)
            {
                var publishedMember = _memberManager.AsPublishedMember(member);
                mention = new Mention
                {
                    Id = id,
                    Name = member.Name,
                    Description = publishedMember?.Value<string>("description"),
                    Image = publishedMember?.Value<MediaWithCrops>("image")?.GetCropUrl(100, 100)
                };
            }

            return mention;
        }

        [HttpGet]
        public IEnumerable<Mention>? GetMembers(string query)
        {
            var mentions = new List<Mention>();
            if (!query.IsNullOrWhiteSpace())
            {
                query = query.ToLowerInvariant();
                var allMembers = _memberService.GetAllMembers();
                if (allMembers.Any(x => x.Name != null && x.Name.ToLowerInvariant().Contains(query)))
                {
                    var filteredMembers = allMembers.Where(x => x.Name != null && x.Name.ToLowerInvariant().Contains(query)).Select(member => new Mention
                    {
                        Id = member.Id.ToString(),
                        Name = member.Name,
                        Description = member.GetValue<string>("description"),
                    });
                    mentions.AddRange(filteredMembers);
                }
            }

            return mentions;
        }
    }
}
