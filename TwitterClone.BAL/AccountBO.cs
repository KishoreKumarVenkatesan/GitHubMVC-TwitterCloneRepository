using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DAL;
using TwitterClone.Entities;

namespace TwitterClone.BAL
{
  public class AccountBO
    {
        public bool SignUp(SignupModel model)
        {
            AccountDAL acc = new AccountDAL();
            bool success = false;
            if (model!=null)
            {
                success= acc.SignupUser(model);
            }
            return success;
        }

        public bool VerifyLoginUser(LoginModel model)
        {
            AccountDAL acc = new AccountDAL();
            bool loginSuccess = false;
            if (model.userId!=null)
            {
                loginSuccess = acc.VerifyLoginUser(model);
            }
            return loginSuccess;
        }

        public List<ManageProfile> GetUserInfo(string UserID)
        {
            AccountDAL acc = new AccountDAL();
            List<ManageProfile> lst = new List<ManageProfile>();
            if(!String.IsNullOrEmpty(UserID))
            {
                lst = acc.GetUserInfo(UserID);
              
            }
            return lst;
        }
        public bool UpdateDeleteUserInfo(ManageProfile model, string userID)
        {
            AccountDAL acc = new AccountDAL();
            bool result = false;
            if (model!=null && !string.IsNullOrEmpty(userID))
            {
                result = acc.UpdateDeleteUserInfo(model, userID);
            }
            return result;
        }
    }
}
