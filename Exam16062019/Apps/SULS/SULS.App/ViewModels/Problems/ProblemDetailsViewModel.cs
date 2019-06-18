namespace SULS.App.ViewModels.Problems
{
    using SULS.App.ViewModels.Submissions;
    using System.Collections.Generic;

    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public ICollection<SubmissionViewModel> Submissions { get; set; }
    }
}
