using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SisVenta.Areas.Users.Models;
using SisVenta.Data;
using SisVenta.Library;
using SisVenta.Models;

namespace SisVenta.Areas.Users.Controllers
{
    [Area("Users")]
    public class UsersController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private LUser _user;
        private static DataPaginador<InputModelRegister> models;

        public UsersController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _user = new LUser(userManager, signInManager, roleManager, context);
        }

        public IActionResult Users(int id, string filtrar)
        {
            //if (_signInManager.IsSignedIn(User))
            //{
                Object[] objects = new Object[3];
                var data = _user.getTUsuariosAsync(filtrar, 0);
                if (0 < data.Result.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegister>().paginador(data.Result, id, 10, "Users", "Users", "Users", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<InputModelRegister>();
                }
                models = new DataPaginador<InputModelRegister>
                {
                    List = (List<InputModelRegister>)objects[2],
                    Pagi_info = (string)objects[0],
                    Pagi_navegacion = (string)objects[1],
                    Input = new InputModelRegister(),
                };
                return View(models);
            //}
            //else
            //{
            //    return Redirect("/");
            //}
        }
    }
}
