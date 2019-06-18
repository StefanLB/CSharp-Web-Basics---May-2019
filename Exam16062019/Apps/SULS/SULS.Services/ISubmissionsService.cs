namespace SULS.Services
{
    using SULS.Models;
    using System.Linq;

    public interface ISubmissionsService
    {
        void CreateSubmission(string code, string problemId, string userId);

        IQueryable<Submission> GetAllSubmissionsByProblemId(string problemId);

        void DeleteSubmissionById(string submissionId);

        int GetNumberOfSubmissionsByProblemId(string problemId);
    }
}
