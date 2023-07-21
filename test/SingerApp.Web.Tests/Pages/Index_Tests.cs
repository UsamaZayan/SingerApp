using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace SingerApp.Pages;

public class Index_Tests : SingerAppWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
