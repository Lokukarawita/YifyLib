using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YifyLib.Data.Base;

namespace YifyLib.Data
{
    public class ReviewUser : AbstractUser
    {
        public ReviewUser()
        { }

        public string UserLocation { get; set; }

        public override string ToString()
        {
            return Username + ", " + UserLocation;
        }
    }
}
