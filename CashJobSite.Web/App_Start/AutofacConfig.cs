﻿using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Services;
using CashJobSite.Data;
using CashJobSite.Data.Repositories;
using CashJobSite.Models;

namespace CashJobSite.Web
{
    public class AutofacConfig
    {
        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CashJobSiteDbContext>().As<ICashJobSiteDbContext>().InstancePerRequest();

            builder.RegisterType<JobRepository>().As<IRepository<Job>>();
            builder.RegisterType<JobReportRepository>().As<IRepository<JobReport>>();
            builder.RegisterType<JobApplicationRepository>().As<IRepository<JobApplication>>();

            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterType<EmailService>().As<IEmailService>();
            builder.RegisterType<Logger>().As<ILogger>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}