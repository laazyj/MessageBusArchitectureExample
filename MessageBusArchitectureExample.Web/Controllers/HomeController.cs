using System;
using System.Web.Mvc;
using MessageBusArchitectureExample.Service1.Messages.Commands;
using MessageBusArchitectureExample.Web.Models;
using NServiceBus;

namespace MessageBusArchitectureExample.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public ISendOnlyBus Bus { get; set; }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Example of a fire & forget style command.
        /// Once it is sent we can assume that it's work will be done eventually.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult FireAndForgetCommand()
        {
            var id = Guid.NewGuid();
            Bus.Send<FireAndForgetCommand>(cmd =>
            {
                cmd.EntityId = id;
                cmd.SomeSortOfTimeStampUtc = DateTime.UtcNow;
                cmd.SomeProperty = "Do something to this entity";
                cmd.AnotherProperty = 10;
            });

            return View(new FireAndForgetCommandViewModel
            {
                EntityId = id
            });
        }
    }
}