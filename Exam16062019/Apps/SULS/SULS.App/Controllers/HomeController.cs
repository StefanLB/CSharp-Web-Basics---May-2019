namespace SULS.App.Controllers
{
    using SIS.MvcFramework;
    using SIS.MvcFramework.Attributes;
    using SIS.MvcFramework.Result;
    using SULS.App.ViewModels.Home;
    using SULS.Services;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly ISubmissionsService submissionsService;

        public HomeController(IProblemsService problemsService, ISubmissionsService submissionsService)
        {
            this.problemsService = problemsService;
            this.submissionsService = submissionsService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Index();
        }

        public IActionResult Index()
        {
            HomeProblemAllViewModel homeProblemAllViewModel = new HomeProblemAllViewModel();

            if (this.IsLoggedIn())
            {
                var problems = this.problemsService.GetAllProblems()
                    .Select(x => new HomeProblemViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Count = submissionsService.GetNumberOfSubmissionsByProblemId(x.Id)
                    }).ToList();

                homeProblemAllViewModel.Problems = problems;

                return this.View(homeProblemAllViewModel);
            }

            return this.View(homeProblemAllViewModel);
        }
    }
}