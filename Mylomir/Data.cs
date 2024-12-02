using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mylomir
{
    class Data
    {
        //static public string con = $@"host={Properties.Settings.Default["host"]};
        //                               uid={Properties.Settings.Default["user"]};
        //                               pwd={Properties.Settings.Default["password"]};
        //                               database={Properties.Settings.Default["db"]};";
        //static public string UserFIO;        
        static public string role;
        static public string page;
        static public string con = "host = localhost; uid = root; pwd = root; database = db22;"; 
        //static public string con = "host = 10.207.106.12; uid = user22; pwd = kk94; database = db22;"; 
    }
}
