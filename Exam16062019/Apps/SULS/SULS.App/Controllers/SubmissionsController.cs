namespace SULS.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using SULS.App.ViewModels.Problems;
    using SULS.App.ViewModels.Submissions;
    using SULS.Services;

    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;

        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(string id)
        {
            var problem = problemsService.GetProblemById(id);

            if (problem == null)
            {
                return this.Redirect("/");
            }

            var result = new ProblemViewModel
            {
                ProblemId = problem.Id,
                Name = problem.Name
            };

            return this.View(result);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(SubmissionCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Submissions/Create" + "?id=" + input.ProblemId);
            }

            this.submissionsService.CreateSubmission(input.Code, input.ProblemId, this.User.Id);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(string id)
        {
            this.submissionsService.DeleteSubmissionById(id);

            return this.Redirect("/");
        }
    }
}
