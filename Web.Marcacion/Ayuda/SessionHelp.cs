using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Web.Marcacion.Ayuda
{
    public class SessionHelp
    {

        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }
        public static int GetUser()
        {
            int UID = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    UID = Convert.ToInt32(ticket.UserData);
                }
            }
            return UID;
        }
        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var Galleta = FormsAuthentication.GetAuthCookie("Users", persist);


            Galleta.Name = FormsAuthentication.FormsCookieName;
            Galleta.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(Galleta.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            Galleta.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(Galleta);
        }

    }
}