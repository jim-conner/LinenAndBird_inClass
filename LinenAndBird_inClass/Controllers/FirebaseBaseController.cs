using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinenAndBird_inClass.Controllers
{
    public class FirebaseBaseController : ControllerBase
    {
        // you can use either below
        public string GetFirebaseUid()
        {
            return User.FindFirst(claim => claim.Type == "user id").Value;
        }

        public string FirebaseUid => User.FindFirst(claim => claim.Type == "user id").Value;
        
        // ^^ makes this a read-only property

    }
}
