using System.Threading.Tasks;
using System.Web.Mvc;
using CashJobSite.Application.Features.FindAllJobs;
using CashJobSite.Application.Features.SearchJobs;
using CashJobSite.Web.Models;
using MediatR;

namespace CashJobSite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ViewResult> Index()
        {
            var allJobs = await _mediator.Send(new FindAllJobsQuery());

            var viewModel = new HomePageViewModel { Jobs = allJobs, SearchForm = new SearchFormModel() };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ViewResult> Index([Bind(Prefix = "SearchForm")]SearchFormModel search)
        {
            var searchResults = await _mediator.Send(new SearchJobsQuery(search.Title, search.Cash));

            var viewModel = new HomePageViewModel { Jobs = searchResults, SearchForm = search };

            return View(viewModel);
        }
    }
}