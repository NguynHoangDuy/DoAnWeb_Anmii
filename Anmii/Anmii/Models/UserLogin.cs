using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anmii.Models
{
    public class UserLogin
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();
        public string EMAIL { get; set; }
        public string MATKHAU { get; set; }

        public NHAN_VIEN GetById(string email)
        {
            return db.NHAN_VIEN.SingleOrDefault(x => x.EMAIL == email);
        }
        public int Login(string userName, string passWord)
        {
            var result = db.NHAN_VIEN.SingleOrDefault(x => x.EMAIL == userName);// usẻName to Email
            if (result == null)
            {

                return 0;
            }
            else if (result.MATKHAU == passWord)
                return 1;
            else return -2;
        }
    }
}