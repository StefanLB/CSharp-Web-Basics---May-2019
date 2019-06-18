namespace SULS.App.ViewModels.Submissions
{
    using SIS.MvcFramework.Attributes.Validation;

    public class SubmissionCreateInputModel
    {
        [RequiredSis]
        [StringLengthSis(30, 800, "Code length should be between 30 and 800 characters")]
        public string Code { get; set; }

        [RequiredSis]
        public string ProblemId { get; set; }
    }
}
