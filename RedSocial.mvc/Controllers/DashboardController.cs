using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Mvc;
using RedSocial.mvc.Extension;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Mappers;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.ViewModels.Dashboard;

namespace RedSocial.mvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProfileUserRepository _profileUserRepository;
        private readonly IPhotoService _photoService;
        private readonly IPostRepository _postRepository;

        private readonly DashboardMapper _dashboardMapper;

        public DashboardController(IProfileUserRepository profileUserRepository, IPhotoService photoService
            ,IPostRepository postRepository)
        {
            _profileUserRepository = profileUserRepository;
            _photoService = photoService;
            _postRepository = postRepository;
            _dashboardMapper = new DashboardMapper();

        }

        [HttpGet]
        public async Task <IActionResult> Index(PostDashboardViewModel? dashboardVM = null)
{
            var postList = _postRepository.GetAllPostOrderByDateTime();
            if (dashboardVM is null)
            {
                dashboardVM = new PostDashboardViewModel();
            }
            dashboardVM.Posts = new List<PostViewModel>();
            await postList;
            _dashboardMapper.DashboardIndex(dashboardVM.Posts,postList.Result.ToList());
            return View("Index",dashboardVM);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDashboardViewModel model) 
        { 
            if(!ModelState.IsValid)
            {
                return await Index(model);
            }
            var actualUser = await _profileUserRepository.GetByIdentityUserId(User.GetIdActualIdentityUser());
            string? stringImageUrl = null;
            if(model.CreatePost.Image is not null && model.CreatePost.Image.Length>0)
            {
                var photoAddResult = await _photoService.AddPhotoAsync(model.CreatePost.Image);
                stringImageUrl = photoAddResult.Url.ToString();
            }
            var newPost = _dashboardMapper.CreatePost(model.CreatePost,actualUser,stringImageUrl);
            var addDbPostResult = await _postRepository.Add(newPost);
            if (!addDbPostResult)
            {
                return await Index(model);
            }
            return await Index();
        }
        
        

    }
}
