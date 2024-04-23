using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.Extension;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Mappers;
using RedSocial.mvc.Repository;
using RedSocial.mvc.ViewModels.Dashboard;
using RedSocial.mvc.ViewModels.Post;

namespace RedSocial.mvc.Controllers
{
    public class PostController : Controller
    {
        private readonly ICommentPostProfileRepository _commentPostProfileRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPhotoService _photoService;
        private readonly IPostRepository _postRepository;
        private readonly IProfileUserRepository _profileUserRepository;
        public PostController(ICommentPostProfileRepository commentPostProfileRepository,
            ICommentRepository commentRepository,IPhotoService photoService,IPostRepository postRepository,
            IProfileUserRepository profileUserRepository)
        {
            _commentPostProfileRepository = commentPostProfileRepository;
            _commentRepository = commentRepository;
            _photoService = photoService;
            _postRepository = postRepository;
            _profileUserRepository = profileUserRepository;
        }

        public async Task<IActionResult> PostDetails(int postId, PostDetailViewModel? postDetailViewModel = null)
        {
            var post = await _postRepository.GetById(postId);
            var listPostToView = await _commentPostProfileRepository.GetByIdPost(postId);
            List<CommentViewModel> commentViewModels = new List<CommentViewModel>();
            foreach(var comment in listPostToView)
            {
                commentViewModels.Add(new CommentViewModel()
                {
                    FullNameProfile = comment.ProfileUser.Name + comment.ProfileUser.LastName,
                    Body = comment.Comment.Body,
                    ImageUrl = comment.Comment.ImageUrl,
                    DeltaTime = DashboardMapper.ObtainDeltaTimeFromActualDateTime(comment.Created),
                    ProfileImageUrl = comment.ProfileUser.ImageUrl
                }) ;
            }
            if(postDetailViewModel is null)
            {
                postDetailViewModel = new PostDetailViewModel();
            }
            postDetailViewModel.CommentViewModels = commentViewModels;
            postDetailViewModel.PostViewModel = new PostViewModel()
            {
                Id = postId,
                Title = post.Title,
                Body = post.Body,
                ImageUrl = post.ImageUrl,
                CreationTimeDelta = DashboardMapper.ObtainDeltaTimeFromActualDateTime(post.Created),
                ProfileUserVM = new ProfileUserViewModel
                {
                    FullName = post.ProfileUser.Name + " " + post.ProfileUser.LastName,
                    Id = post.ProfileUserId,
                    ImageUrl = post.ProfileUser.ImageUrl
                }
            };
            return View("PostDetails",postDetailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int postId, PostDetailViewModel postDetailViewModel)
        {
            if(!ModelState.IsValid)
            {
                return await PostDetails(postId, postDetailViewModel);
            }
            string? imageCommentUrl = null;
            if(postDetailViewModel.CreateCommentViewModel.Image is not null && postDetailViewModel.CreateCommentViewModel.Image.Length >0)
            {
                var photoAddResult = await _photoService.AddPhotoAsync(postDetailViewModel.CreateCommentViewModel.Image);
                imageCommentUrl = photoAddResult.Url.ToString();
            }

            var commentToAdd = new Comment()
            {
                Body = postDetailViewModel.CreateCommentViewModel.Body,
                ImageUrl = imageCommentUrl,

            };
            var addCommentResult = await _commentRepository.Add(commentToAdd);

            if (!addCommentResult)
            {
                TempData["Error"]= "Error al añadir tu comentario";
                return await PostDetails(postId,postDetailViewModel);
            }

            var actualUser = await _profileUserRepository.GetByIdentityUserId(User.GetIdActualIdentityUser());

            var addCommentPostProfileResult = await _commentPostProfileRepository.Add(new CommentPostProfile()
            {
                PostId = postId,
                ProfileUserId = actualUser.ProfileUserId,
                Created = DateTime.Now,
                CommentId = commentToAdd.CommentId,
            });

            if(!addCommentPostProfileResult)
            {
                TempData["Error"] = "Error al añadir tu comentario";
                return await PostDetails(postId, postDetailViewModel);
            }

            return await PostDetails(postId,null);
        }
    }
}
