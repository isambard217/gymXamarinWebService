using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeActiveWebservice.Controllers {
    
    [Route("api/[controller]")]
    public class StartupController : Controller {
        
        // GET api/values
        [HttpGet("index")]
        public string Get() {
            
            return "Web API is running";
        }



    }
}
