namespace SULS.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Attributes.Security;
    using SIS.MvcFramework.Result;
    using SULS.App.ViewModels.Problems;
    using SULS.App.ViewModels.Submissions;
    using SULS.Services;
    using System.Linq;

    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public ProblemsController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateProblemInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Problems/Create");
            }

            this.problemsService.CreateProblem(input.Name, input.Points);

            return this.Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(string id)
        {
            var problem = this.problemsService.GetProblemById(id);
            const int MaxPoints = 100;

            var submissions = this.submissionsService.GetAllSubmissionsByProblemId(id)
                .Select(x => new SubmissionViewModel
                {
                    Username = x.User.Username,
                    AchievedResult = (int)(((decimal)x.AchievedResult*100/(decimal)x.Problem.Points)),
                    MaxPoints = MaxPoints,
                    CreatedOn = x.CreatedOn.ToString("dd'/'MM'/'yyyy"),
                    SubmissionId = x.Id
                }).ToList();

            ProblemDetailsViewModel problemDetails = new ProblemDetailsViewModel
            {
                Name = problem.Name,
                Submissions = submissions
            };

            return this.View(problemDetails);
        }
    }
}
