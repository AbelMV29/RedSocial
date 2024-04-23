using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.ViewModels.Dashboard;

namespace RedSocial.mvc.Mappers
{
    public class DashboardMapper
    {
        public void DashboardIndex(List<PostViewModel> postsVM,List<Post> postsList)
        {
            foreach(var post in postsList)
            {
                postsVM.Add(new PostViewModel
                {
                    Id = post.PostId,
                    Title = post.Title,
                    Body = post.Body,
                    ImageUrl = post.ImageUrl,
                    CreationTimeDelta = ObtainDeltaTimeFromActualDateTime(post.Created), 
                    ProfileUserVM = new ProfileUserViewModel
                    {
                        FullName = post.ProfileUser.Name + " " + post.ProfileUser.LastName,
                        Id = post.ProfileUserId,
                        ImageUrl = post.ProfileUser.ImageUrl
                    } 
                });
            }
        }

        public Post CreatePost(CreatePostViewModel postCreate, ProfileUser actualUser,string stringImageUrl)
        {
            var newPost = new Post()
            {
                Title = postCreate.Title,
                Body = postCreate.Body,
                ImageUrl = stringImageUrl,
                ProfileUserId = actualUser.ProfileUserId,
                ProfileUser = actualUser,
                Created = DateTime.Now
            };
            return newPost;
        }

        public static string ObtainDeltaTimeFromActualDateTime(DateTime postCreated)
        {
            TimeSpan lapsedTime = DateTime.Now - postCreated;

            if (lapsedTime.TotalDays >= 1)
            {
                return $"{lapsedTime.Days} día(s) atrás";
            }
            else if (lapsedTime.TotalHours >= 1)
            {
                return $"{lapsedTime.Hours} hora(s) atrás";
            }
            else if (lapsedTime.TotalMinutes >= 1)
            {
                return $"{lapsedTime.Minutes} minuto(s) atrás";
            }
            else
            {
                return "Hace unos momentos";
            }
        }
    }
}