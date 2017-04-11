﻿using System.Web.Mvc;
using CashJobSite.Application.Services;
using CashJobSite.Models;
using CashJobSite.Web.Models;

namespace CashJobSite.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        public ActionResult Index(int id)
        {
            var job = _jobService.GetJobById(id);
            return View(job);
        }

        public ActionResult Apply(int id)
        {
            var job = _jobService.GetJobById(id);

            var viewModel = new ApplyForJobViewModel
            {
                JobId = job.Id,
                JobCash = job.Cash,
                JobDescription = job.Description,
                JobTitle = job.Title
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Apply(ApplyForJobViewModel model)
        {
            _jobService.AddJobApplication(model.JobId, model.CandidateName, model.CandidateEmail, model.CandidateInfo);

            return RedirectToAction("Applied");
        }

        public ActionResult Report(int id)
        {
            var ipAddress = Request.UserHostAddress;
            _jobService.ReportJob(id, ipAddress);

            var job = _jobService.GetJobById(id);
            return View(job);
        }

        [HttpGet]
        public ActionResult Post()
        {
            var postModel = new PostJobViewModel();
            return View(postModel);
        }

        [HttpPost]
        public ActionResult Post(PostJobViewModel model)
        {
            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                Cash = model.Cash
            };

            var savedJob = _jobService.AddJob(job);

            return RedirectToAction("Posted", new { id = savedJob.Id });
        }

        public ActionResult Posted(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        public ActionResult Applied()
        {
            return View();
        }
    }
}