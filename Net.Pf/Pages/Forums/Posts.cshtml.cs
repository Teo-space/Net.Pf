using Infrastructure.Forums.Managers.ForumManager;
using Infrastructure.Forums.Managers.PostManager;
using Infrastructure.Forums.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Net.Pf.Pages.Forums
{
    public class PostsModel : PageModel
    {
        public readonly IPostManager postManager;

        public PostsModel(IPostManager postManager)
        {
            this.postManager = postManager;
        }


		public Guid TopicId;
        public IReadOnlyList<Post> Posts { get; set; } = new List<Post>();


        public async Task OnGet(Guid TopicId)
        {
			this.TopicId = TopicId;
			this.Posts = await this.postManager.GetPosts(TopicId);
        }




		public CreatePostCommand createPostCommand { get; private set; }
		public record CreatePostCommand(Guid TopicId, string Text)
		{
			public class Validator : AbstractValidator<CreatePostCommand>
			{
				public Validator()
				{
					RuleFor(x => x.TopicId).NotNull().NotEmpty();
					RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
				}
			}

		}


		public async Task<ActionResult> OnPost(CreatePostCommand createPostCommand)
		{
			//this.createPostCommand = createPostCommand;
			if (this.ModelState.IsValid)
			{
				await postManager.Create(createPostCommand.TopicId, createPostCommand.Text);
				//this.createPostCommand = default;
				return this.RedirectToPage(new { TopicId = createPostCommand.TopicId });
				//Setup(createPostCommand.TopicId);
			}
			return Page();
		}




	}

}


