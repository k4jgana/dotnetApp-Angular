using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DutchTreat.Controllers
{
    public class AccountController :Controller
    {
        private readonly ILogger<AccountController> logger;
		private readonly SignInManager<StoreUser> signInManager;
		private readonly UserManager<StoreUser> userManager;
		private readonly IConfiguration config;

		public AccountController(ILogger<AccountController> logger,SignInManager<StoreUser> signInManager,
			UserManager<StoreUser> userManager, IConfiguration config)
        {
            this.logger = logger;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.config = config;
		}


        public IActionResult Login() 
        {
            if(this.User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Index","App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        return RedirectToAction("Shop", "App");
                    }
                }
            }

            ModelState.AddModelError("", "Failed to log in");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }




		[HttpPost]
		public async Task<IActionResult> CreateToken([FromBody] LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(loginViewModel.Username);
				if (user != null)
				{
					var result = await signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, lockoutOnFailure: false);
					if (result.Succeeded)
					{
						// create the token here
						// Claims-based identity is a common way for applications to acquire the identity information they need about users inside their organization, in other organizations, 
						// and on the Internet.[1] It also provides a consistent approach for applications running on-premises or in the cloud. 
						// Claims -based identity abstracts the individual elements of identity and access control into two parts: 
						// a notion of claims, and the concept of an issuer or an authority
						// to create a claim you need a time and a value!
						var claims = new[]
						{
              // Sub - name of the subject - which is user email here.
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              // jti - unique string that is representative of each token so using a guid
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              // unque name - username of the user mapped to the identity inside the user object 
              // that is available on every controller and view
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
			};

						// key is the secret used to encrypt the token. some parts of the token aren't encrypted but other parts are. 
						// credentials, who it is tied to and exploration etc are encrypted. 
						// information about the claims, about the individual etc aren't encrypted. 
						// use a natural string for a string and encode it to bytes. 
						// read from configuration json - keep changing/or fetch from another source. 
						// the trick here is that the key needs to be accessible for the application
						// also needs to be replaceable by the people setting up your system. 
						var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));

						// new credentials required. create it using the key you just created in combination with a
						// security algorithm.
						var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

						var token = new JwtSecurityToken(config["Tokens:Issuer"], // the creator of the token
						  config["Tokens:Audience"], // who can use the token
						  claims,
						  expires: DateTime.UtcNow.AddMinutes(20),
						  signingCredentials: credentials);

						var results = new
						{
							token = new JwtSecurityTokenHandler().WriteToken(token),
							expiration = token.ValidTo
						};

						// empty quotes to say no source for this resource, just give a new object
						return Created("", results);
					}
				}
			}
			return BadRequest();
		}
	}


}

