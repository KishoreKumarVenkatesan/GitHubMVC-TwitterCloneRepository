using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DAL;
using TwitterClone.Entities;

namespace TwitterClone.DAL
{
    public class AccountDAL
    {
        
        public bool SignupUser(SignupModel model)
        {
            bool Success = false;
            try
            {
                using (var Context = new PODbEntities())
                {
                    Person e = new Person { user_id = model.userId, password = model.password, fullName = model.fullName, email = model.email, active = model.active, joined = DateTime.Now };
                    Context.People.Add(e);
                    Context.SaveChanges();
                    Success = true;
                }
            }
            catch (Exception ex)
            {

            }
            return Success;
        }

        public bool VerifyLoginUser(LoginModel model)
        {
            PODbEntities db = new PODbEntities();
            Person p = new Person();
            bool successLogin = false;
            try
            {
                var result = (from e in db.People
                              where e.user_id == model.userId && e.password == model.password
                              select e).FirstOrDefault();

                if (result != null)
                {
                    successLogin = true;
                }
                else
                {
                    successLogin = false;
                }
            }
            catch (Exception ex)
            {

            }
            return successLogin;
        }

        public List<ManageProfile> GetUserInfo(string UserID)
        {
            PODbEntities db = new PODbEntities();
            Person p = new Person();
            List<ManageProfile> lst = new List<ManageProfile>();
            try
            {
                p = (from e in db.People
                     where e.user_id == UserID
                     select e).FirstOrDefault();

            }
            catch (Exception ex)
            {

            }
            lst.Select(t => new ManageProfile
            {

                email = t.email,
                fullName = t.fullName,
                password = t.password

            });
            return lst;

        }

        public bool UpdateDeleteUserInfo(ManageProfile model, string userID)
        {
            PODbEntities Context = new PODbEntities();

            bool result = false;
            try
            {
                if (!model.deleteAccount)
                {
                    Person p = Context.People.First(i => i.user_id == userID);

                    //using (var Context = new PODbEntities())
                    //{
                    //    p = (from e in db.Person
                    //         where e.user_id == userID
                    //         select e).FirstOrDefault();
                    if (!string.IsNullOrEmpty(model.fullName))
                        p.fullName = model.fullName;
                    if (!string.IsNullOrEmpty(model.password))
                        p.password = model.password;
                    if (!string.IsNullOrEmpty(model.email))
                        p.email = model.email;
                    Context.Entry(p).State = EntityState.Modified;
                    Context.SaveChanges();
                    result = true;
                }

                else
                {
                    Person p = Context.People.First(i => i.user_id == userID);
                    Context.People.Remove(p);
                    Context.SaveChanges();
                    result = false;
                }
            }

            catch (Exception ex)
            {

            }
            return result;
        }

        
    }
}