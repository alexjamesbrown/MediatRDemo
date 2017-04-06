﻿using System;
using System.Collections.Generic;
using System.Linq;
using CashJobSite.Application.Logging;
using CashJobSite.Data.Repositories;
using CashJobSite.Models;

namespace CashJobSite.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<JobReport> _jobReportRepository;
        private readonly IRepository<JobApplication> _jobApplicationRepository;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        public JobService(IRepository<Job> jobRepository, IRepository<JobReport> jobReportRepository, IRepository<JobApplication> jobApplicationRepository, ILogger logger, IEmailService emailService)
        {
            _jobRepository = jobRepository;
            _jobReportRepository = jobReportRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public Job AddJob(Job job)
        {
            try
            {
                _logger.Info("Saving job");
                _jobRepository.Save(job);
                _logger.Info("Job saved");
                return job;
            }
            catch (Exception ex)
            {
                _logger.Error("Error saving job - " + ex.Message);
                throw;
            }
        }

        public Job GetJobById(int id)
        {
            _logger.Info("Getting job with id " + id);
            return _jobRepository.GetById(id);
        }

        public IEnumerable<Job> FindAllJobs()
        {
            _logger.Info("Finding all jobs");

            var result = _jobRepository.List()
                .OrderByDescending(x => x.Created)
                .ToList();

            _logger.Info($"Found {result.Count()} jobs");

            return result;
        }

        public IEnumerable<Job> SearchJobs(string title, int cash)
        {
            _logger.Info("Searching jobs");

            IEnumerable<Job> result;

            if (string.IsNullOrEmpty(title))
            {
                result = _jobRepository.List(job => job.Cash >= cash)
                    .ToList();
            }
            else
            {
                result = _jobRepository.List(job => job.Title.StartsWith(title) && job.Cash >= cash)
                    .ToList();
            }

            _logger.Info($"Found {result.Count()} jobs");

            return result;
        }

        public void ReportJob(int id, string ipAddress)
        {
            var job = _jobRepository.GetById(id);

            var emailSubject = "Job '" + job.Title + "' has been reported.";
            var emailBody = "Somebody has reported job #" + job.Id;

            _emailService.SendEmail("admin@CashJobSiteCashJobSite.com", emailSubject, emailBody);
            _logger.Debug("Email Sent");

            var jobReport = new JobReport { Job = job,  ReporterIpAddress = ipAddress};

            _jobReportRepository.Save(jobReport);

            _logger.Debug("Job report saved");
        }

        public void AddJobApplication(int jobId, string candidateName, string candidateEmail, string candidateInfo)
        {
            var job = _jobRepository.GetById(jobId);

            var jobApplication = new JobApplication
            {
                CandidateName = candidateName,
                CandidateEmail = candidateEmail,
                CandidateInfo = candidateInfo,
                Job = job
            };

            _jobApplicationRepository.Save(jobApplication);

            var emailSubject = "Job application received.";
            var emailBody = "You have a new application for your job #" + job.Id + "\n" +
                            "Name: " + candidateName + "\n" +
                            "Email: " + candidateEmail + "\n" +
                            "Info: " + candidateInfo + "\n";
            
            _emailService.SendEmail(job.BossEmail, emailSubject, emailBody);
            _logger.Debug("Email Sent");
        }
    }
}
