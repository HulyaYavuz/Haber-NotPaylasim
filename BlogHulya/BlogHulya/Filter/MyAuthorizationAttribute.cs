using BlogHulya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHulya.Filter
{
    public class MyAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public string ActionMemberType { get; private set; }
        public MyAuthorizationAttribute()
        {

        }
        public MyAuthorizationAttribute(string _memberType)
        {
            this.ActionMemberType = _memberType;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
           var member = (Member)HttpContext.Current.Session["LogonUser"];
            if(member == null)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
            else
            {
                var memberType =(string)member.MemberType;
                if(memberType != ActionMemberType)
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
            }
        }
    }
}