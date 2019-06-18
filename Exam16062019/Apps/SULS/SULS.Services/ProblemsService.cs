namespace SULS.Services
{
    using SULS.Data;
    using SULS.Models;
    using System.Linq;

    public class ProblemsService : IProblemsService
    {
        private readonly SULSContext context;

        public ProblemsService(SULSContext sulsContext)
        {
            this.context = sulsContext;
        }

        public void CreateProblem(string name, int points)
        {
            Problem problem = new Problem
            {
                Name = name,
                Points = points
            };

            this.context.Problems.Add(problem);
            this.context.SaveChanges();
        }

        public IQueryable<Problem> GetAllProblems()
        {
            var problems = this.context.Problems;

            return problems;
        }

        public Problem GetProblemById(string id)
        {
            var problem = this.context.Problems.Where(x => x.Id == id).FirstOrDefault();

            return problem;
        }
    }
}
