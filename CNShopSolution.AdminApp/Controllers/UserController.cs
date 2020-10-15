using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CNShopSolution.AdminApp.Services;
using CNShopSolution.ViewModel.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace CNShopSolution.AdminApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserApiClient _userApi;
        public UserController(IUserApiClient userApi)
        {
            _userApi = userApi;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var token = await _userApi.Authenticate(request);

            return View(token);
        }



    }
}
