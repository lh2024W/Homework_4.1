using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_4._1
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public UserSettings UserSettings { get; set; }
    }
}
