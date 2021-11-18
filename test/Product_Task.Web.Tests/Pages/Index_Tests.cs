using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Product_Task.Pages
{
    public class Index_Tests : Product_TaskWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
