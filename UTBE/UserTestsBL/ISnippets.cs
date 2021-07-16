using System.Threading.Tasks;
using UserTestsModels;
using Octokit;


namespace UserTestsBL
{
    public interface ISnippets
    {
        Task<TestMaterial> GetRandomQuote();
        Task<TestMaterial> GetCodeSnippet(int id);
        Task<string> GetAuth0String();
    }
}