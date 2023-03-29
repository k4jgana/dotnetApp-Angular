using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController:Controller
    {
		private readonly IMailService mailService;
		private readonly IDutchRepository repository;

		public AppController(IMailService mailService,IDutchRepository repository) 
        {
			this.mailService = mailService;
			this.repository = repository;
		}  
        public IActionResult Index()
        {
            //var results=context.Products.ToList();
            return View("Index");
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View("Contact");
        }

        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {

			            
		    mailService.SendMessage("shawn@yahoo.com", model.Subject, $"From {model.Name} {model.Email},Message:{model.Message}");
            ViewBag.UserMessage = "message sent";
            ModelState.Clear();
            
            return View("Contact");

		}


        public IActionResult Shop() 
        {
            var results=repository.GetProducts();
            return View(results);
            
        }
    }
}
