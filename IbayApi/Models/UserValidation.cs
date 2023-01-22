using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class UserValisation
    {
        //public Boolean EmailIsUse(String Email, DatabaseContext context)
        //{
        //    var _context = (DatabaseContext)validationContext.GetService(typeof(DatabaseContext));
        //    var entity = _context.Users.SingleOrDefault(e => e.Email == value.ToString());

        //    if (entity != null)
        //    {
        //        return true;
        //    }
        //    return false ;
        //}

        public string GetErrorMessage(string email)
        {
            return $"Email {email} is already in use.";
        }
    }

}
