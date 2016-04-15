using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkDB_DudasVI.General
{
    public static class Session
    {
        private static STAFF user;

        public static STAFF User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
