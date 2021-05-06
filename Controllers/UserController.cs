using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using netcoreapinet;

namespace netcoreapi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(AppDb db)
        {
            Db = db;
        }

        
        public AppDb Db { get; }

        [HttpGet("{username}/{password}")]
        public async Task<string> UserAuthen(string username,string password)
        {

                try
                {

                UserMitsu u = new UserMitsu();

                u.id = "1";
                u.user_mail = "kritsanai@hrmonious.co.th";
                u.first_name = "Kritsanai    ";
                u.last_name = "Yindeesook";
                u.profile_photo_url = "110059";
                u.token = "110059";
                u.dealer = "110059";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(u);
                return json;
                }
                catch (Exception ex)
                {
                    return "";
                    //throw;
                }
            




        }
    }
    public class UserMitsu
    {
        public string id { get; set; }
        public string user_mail { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string profile_photo_url { get; set; }
        public string token { get; set; }
        public string dealer { get; set; }


    }
}