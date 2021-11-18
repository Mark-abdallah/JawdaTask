using Volo.Abp.Settings;

namespace Product_Task.Settings
{
    public class Product_TaskSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(Product_TaskSettings.MySetting1));
        }
    }
}
