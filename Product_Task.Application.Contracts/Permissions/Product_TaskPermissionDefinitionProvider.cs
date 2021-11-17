using Product_Task.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Product_Task.Permissions
{
    public class Product_TaskPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var product_TaskGroup = context.AddGroup(Product_TaskPermissions.GroupName, L("Permission:Product_Task"));

            var productsPermission = product_TaskGroup.AddPermission(Product_TaskPermissions.Products.Default, L("Permission:Products"));
            productsPermission.AddChild(Product_TaskPermissions.Products.Create, L("Permission:Products.Create"));
            productsPermission.AddChild(Product_TaskPermissions.Products.Edit, L("Permission:Products.Edit"));
            productsPermission.AddChild(Product_TaskPermissions.Products.Delete, L("Permission:Products.Delete"));

            var authorsPermission = product_TaskGroup.AddPermission(Product_TaskPermissions.Categories.Default, L("Permission:Categories"));
            authorsPermission.AddChild(Product_TaskPermissions.Categories.Create, L("Permission:Categories.Create"));
            authorsPermission.AddChild(Product_TaskPermissions.Categories.Edit, L("Permission:Categories.Edit"));
            authorsPermission.AddChild(Product_TaskPermissions.Categories.Delete, L("Permission:Categories.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Product_TaskResource>(name);
        }
    }
}
