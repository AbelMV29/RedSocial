using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.ViewModels.Account;

namespace RedSocial.mvc.Mappers
{
    public class AccountMapper
    {
        public IdentityUser RegisterIdentityMapper(RegisterViewModel model)
        {
            var identityUserModel = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                NormalizedUserName = model.UserName.ToUpper(),
                NormalizedEmail = model.Email.ToUpper()
            };
            return identityUserModel;
        }

        public ProfileUser RegisterProfileMapper(RegisterViewModel model,IdentityUser identityUserModel)
        {
            var profileUserCreate = new ProfileUser()
            {
                Name = model.Name,
                LastName = model.LastName,
                IdentityUserId = identityUserModel.Id,
                IdentityUser = identityUserModel,
                Created = DateTime.Now
            };

            return profileUserCreate;
        }   
    }
}