using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;
        private readonly SignInManager<StoreUser> signInManager;
        private readonly UserManager<StoreUser> userManager;
        private readonly IConfiguration config;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger,IMapper mapper, 
            SignInManager<StoreUser> signInManager,UserManager<StoreUser> userManager, IConfiguration config)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.config = config;
        }
        [HttpGet]
        public IActionResult Get(bool includeItems=true)
        {
            try {
                var username = User.Identity.Name;

                var result = repository.GetAllOrdersByUser(username,includeItems);
                return Ok(mapper.Map<IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get all orders:{ex}");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = repository.GetOrderById(User.Identity.Name, id);
                if (order != null)
                {
                    return Ok(mapper.Map<Order,OrderViewModel>(order));
                }
                
                
                
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get all orders{ex}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel model) 
        {
            try {

                if (ModelState.IsValid)
                {
                    var newOrder = mapper.Map<OrderViewModel, Order>(model);

                    var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;


                    repository.AddEntity(newOrder);
                    if (repository.SaveAll()) {
                        
                        return Created($"/api/orders/{newOrder.Id}", mapper.Map<Order,OrderViewModel>(newOrder));
                    }
                }
                else 
                {
                    return BadRequest(ModelState);
                }
               
            } catch (Exception ex) { logger.LogError($"Failed to save a new order:{ex}"); }

            return BadRequest("Failed to save new order");
        }



      /*  [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user,model.Password,false);

                    if (result.Succeeded) 
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
                        };


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));

                        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(config["Token:Issuer"], config["Token:Audience"], claims
                            ,signingCredentials:creds,expires:DateTime.UtcNow.AddMinutes(20));


                        return Created("", new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(token),
                            expiration=token.ValidTo
                        });



                    }

                }
            }

            return BadRequest();

        }*/


    }
}
