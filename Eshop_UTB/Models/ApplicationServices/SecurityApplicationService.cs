﻿using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.ApplicationServices
{
    public class SecurityApplicationService : ISecurityApplicationService
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;

        public SecurityApplicationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task<User> FindUserByUserEmail(string email)
        {
            return userManager.FindByEmailAsync(email);
        }

        public Task<User> FindUserByUserName(string username)
        {
            return userManager.FindByNameAsync(username);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return userManager.GetUserAsync(principal);
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return userManager.GetRolesAsync(user);
        }

        public async Task<bool> Login(LoginViewModel vm)
        {
            var result = await signInManager.PasswordSignInAsync(vm.Username,vm.Password,vm.RememberMe,true);

            return result.Succeeded;
        }

        public Task Logout()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<string[]> Register(RegisterViewModel vm, Roles role)
        {
            User user = new User()
            {
                UserName = vm.Username,
                Name = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber
            };
            string[] errors = null;
            var result = await userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                var resultRole = await userManager.AddToRoleAsync(user, role.ToString());

                if (resultRole.Succeeded == false)
                {
                    for (int i = 0; i < result.Errors.Count(); ++i)
                    {
                        result.Errors.Append(result.Errors.ElementAt(i));
                     }
                }
            }
            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for (int i = 0; i <result.Errors.Count();++i)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }

            }

            return errors;
        }
    }
}
