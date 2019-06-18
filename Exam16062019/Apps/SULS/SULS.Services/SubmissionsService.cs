namespace SULS.Services
{
    using SULS.Data;
    using SULS.Models;
    using System;
    using System.Linq;

    public class SubmissionsService : ISubmissionsService
    {
        private readonly SULSContext context;

        public SubmissionsService(SULSContext sulsContext)
        {
            this.context = sulsContext;
        }

        public void CreateSubmission(string code, string problemId, string userId)
        {
            Random rnd = new Random();
            Problem problem = this.context.Problems.Where(x => x.Id == problemId).FirstOrDefault();
            User user = this.context.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (problem == null || user == null)
            {
                return;
            }

            Submission submission = new Submission
            {
                Code = code,
                AchievedResult = rnd.Next(0, (problem.Points + 1)),
                CreatedOn = DateTime.UtcNow,
                Problem = problem,
                User = user
            };

            context.Submissions.Add(submission);
            context.SaveChanges();
        }

        public void DeleteSubmissionById(string submissionId)
        {
            var submissionToDelete = this.context.Submissions.Where(x => x.Id == submissionId).FirstOrDefault();

            if (submissionToDelete != null)
            {
                this.context.Submissions.Remove(submissionToDelete);
                this.context.SaveChanges();
            }
        }

        public IQueryable<Submission> GetAllSubmissionsByProblemId(string problemId)
        {
            var submissions = this.context.Submissions.Where(x=>x.Problem.Id == problemId);

            return submissions;
        }

        public int GetNumberOfSubmissionsByProblemId(string problemId)
        {
            var numberOfSubmissions = this.context.Submissions.Where(x => x.Problem.Id == problemId).Count();

            return numberOfSubmissions;
        }
    }
}
