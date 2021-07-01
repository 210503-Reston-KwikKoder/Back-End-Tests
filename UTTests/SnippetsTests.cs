using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserTestsBL;
using Xunit;

namespace UTTests
{
    public class SnippetsTests
    {
        private readonly IOptions<ApiSettings> s = Options.Create(new ApiSettings());

            public SnippetsTests()
            {

            }
            
            [Fact]
            public async Task RandomQuoteShouldNotBeNull()
            {
                    ISnippets _snipetService = new Snippets();
                    TestMaterial test1 = await _snipetService.GetRandomQuote();
                    Assert.NotNull(test1);
            }
            [Fact]
            public async Task RandomQuoteShouldReturnRandom()
            {
                    ISnippets _snipetService = new Snippets();
                    TestMaterial test1 = await _snipetService.GetRandomQuote();
                    TestMaterial test2 = await _snipetService.GetRandomQuote();
                    Assert.NotEqual(test1, test2);            
            }
    }
}