using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Models;
using RedSocial.mvc.ViewModels.Account;
using RedSocial.mvc.Extension;

namespace RedSocial.mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProfileUserRepository _profileUserRepository;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IProfileUserRepository profileUserRepository, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _profileUserRepository = profileUserRepository;
            _roleManager = roleManager;

        }
        [HttpGet]
        public async Task<IActionResult> AddRolesToDataBase()
        {
            var adminRole = new IdentityRole(Roles.Admin)
            {
                NormalizedName = Roles.Admin.ToUpper()
            };
            var userRole = new IdentityRole(Roles.User)
            {
                NormalizedName = Roles.User.ToUpper()
            };
            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(userRole);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = "pruebaUrl")
        {
            var registerVM = new RegisterViewModel() { returnUrl = returnUrl };
            return View(registerVM);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ConfirmPassword != model.Password)
            {
                TempData["Error"] = "Las contraseñas no coinciden";
                return View(model);
            }
            var existsEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existsEmail is not null)
            {
                TempData["Error"] = "El email ya esta en uso";
                return View(model);
            }
            var existUserName = await _userManager.FindByNameAsync(model.UserName);
            if (existUserName is not null)
            {
                TempData["Error"] = "El nombre de usuario ya esta en uso";
                return View(model);
            }

            var identityUserModel = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                NormalizedUserName = model.UserName.ToUpper(),
                NormalizedEmail = model.Email.ToUpper()
            };

            var identityCreateResult = await _userManager.CreateAsync(identityUserModel,model.Password);
            if(!identityCreateResult.Succeeded)
            {
                foreach(var error in identityCreateResult.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                TempData["Error"] = "Error al crear tu cuenta";
                return View(model);
            }

            var roleAddResult = await _userManager.AddToRoleAsync(identityUserModel,Roles.User);

            if(!roleAddResult.Succeeded)
            {
                foreach (var error in roleAddResult.Errors)
                {
                    Console.WriteLine(error.Description);
                }
                var deleteIdentityResult = await _userManager.DeleteAsync(identityUserModel);
                if(!deleteIdentityResult.Succeeded)
                {
                    foreach (var error in deleteIdentityResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                TempData["Error"] = "Error al crear tu cuenta";
                return View(model);
            }

            var profileUserCreate = new ProfileUser()
            {
                Name = model.Name,
                LastName = model.LastName,
                IdentityUserId = identityUserModel.Id,
                IdentityUser = identityUserModel,
                Created = DateTime.Now
            };

            var profileUserAddResult = await _profileUserRepository.Add(profileUserCreate);
            if(!profileUserAddResult)
            {
                var deleteIdentityResult = await _userManager.DeleteAsync(identityUserModel);
                if (!deleteIdentityResult.Succeeded)
                {
                    foreach (var error in deleteIdentityResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                TempData["Error"] = "Error al crear tu cuenta";
                return View(model);
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var emailUserExist = await _userManager.FindByEmailAsync(model.Email);
            var emailPasswordCorrect = await _userManager.CheckPasswordAsync(emailUserExist, model.Password); 

            if(emailUserExist is null || !emailPasswordCorrect)
            {
                TempData["Error"] = "El correo y/o la contraseña son incorrectos";
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(emailUserExist, model.Password, model.RememberMe, true);
            if(!signInResult.Succeeded)
            {
                if(!signInResult.IsLockedOut)
                {
                    TempData["Error"] = "Error al ingresar a tu cuenta";
                    return View(model);
                }
                return View("Locked");
            }
            var profileUser = await _profileUserRepository.GetByIdentityUserId(emailUserExist.Id);
            if(!profileUser.IsComplete)
            {
                return RedirectToAction("NewUser", "Profile");
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();  
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var userEmailExist = await _userManager.FindByEmailAsync(model.Email);
            var passwordCheckResult = await _userManager.CheckPasswordAsync(userEmailExist, model.Password);
            if(userEmailExist is null || userEmailExist.Email != User.GetEmailActualIdentityUser() || !passwordCheckResult)
            {
                TempData["Error"] = "Email y/o contraseña incorrecta";
                return View(model);
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(userEmailExist, model.Password, model.NewPassword);
            if(!changePasswordResult.Succeeded)
            {
                TempData["Error"] = "Error al cambiar tu contraseña";
                return View(model);
            }
            model.Changed = true;
            TempData["Success"] = "Contraseña cambiada con exito!";
            return View(model);

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
