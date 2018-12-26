using System.Web.Mvc;

namespace BlogHulya.Areas.ManagerPanel
{
    public class ManagerPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ManagerPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ManagerPanel_default",
                "ManagerPanel/{controller}/{action}/{id}",
                defaults: new { controller = "Manager", action = "Index", id = UrlParameter.Optional }
                //new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}