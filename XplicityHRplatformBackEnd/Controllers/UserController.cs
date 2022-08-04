using Microsoft.AspNetCore.Mvc;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()

        {
            return new List<User>
                {
                    new User
                    {
                        //Id= 1,
                        Email = "admin@admin.lt",
                        Password = "admin"
                    }
                };
        }
    }
}
