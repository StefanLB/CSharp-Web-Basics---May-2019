namespace SULS.Services
{
    using SULS.Models;
    using System.Linq;

    public interface IProblemsService
    {
        void CreateProblem(string name, int points);

        IQueryable<Problem> GetAllProblems();

        Problem GetProblemById(string id);
    }
}
