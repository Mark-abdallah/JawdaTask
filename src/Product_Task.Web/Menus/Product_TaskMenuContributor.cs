using System.Threading.Tasks;
using Product_Task.Localization;
using Product_Task.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Product_Task.Web.Menus
{
    public class Product_TaskMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<Product_TaskResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    Product_TaskMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );
            if (!administration.IsDisabled)
            {
                context.Menu.AddItem(
               new ApplicationMenuItem(
                   "Products",
                   l["Products"]
                   ).AddItem(
                   new ApplicationMenuItem(
                       "Product_Task.Products",
                       l["Products"],
                       url: "/Product"
                       )
                   )
                   );

            }
           
            
            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        }
    }
}
