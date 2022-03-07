using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TesteTecnicoDigiStart.Domain;
using TesteTecnicoDigiStart.Interface;

namespace TesteTecnicoDigiStart.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class MainController : AppBaseController
    {
        public MainController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginDTO model)
        {
            var userRepository = GetService<IUsersRepository>();
            string token = Configuration.GetSection("Infos").GetSection("Token").Value;

            try
            {
                if (userRepository.CheckUserOnDB(model))
                {
                    var response = new StandardReturnDTO()
                    {
                        status_code = "200",
                        message = "Success",
                        data = "Token " + token
                    };

                    return response;
                }
                else
                {
                    throw new Exception("Error: The Email or Password is Incorrect.");
                }
            }
            catch(Exception e)
            {
                var response = new StandardReturnDTO()
                {
                    status_code = "404",
                    message = "Not Found",
                    data = e.Message
                };
                return response;
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<dynamic>> CreateNewUser([FromBody] UserDTO model)
        {
            var userRepository = GetService<IUsersRepository>();
            var response = new StandardReturnDTO()
            {
                status_code = "201"
            };

            try
            {
                if (!userRepository.CheckUserOnDB(model.email))
                {
                    userRepository.CreateNewUser(model);
                    response.message = "Success: Your User Was Created Successfully";
                }
                else
                {
                    throw new Exception("Error: There is already a user with this Email registered.");
                }
            }
            catch (Exception e)
            {
                response.status_code = "409";
                response.message = e.Message;
            }

            HttpContext.Response.StatusCode = int.Parse(response.status_code);
            return response;
        }

        [HttpGet]
        [Route("get-infos")]
        public async Task<ActionResult<dynamic>> GetInfos([FromHeader][Required] string token)
        {
            var requestHelper = GetService<IRequestHelper>();
            string url = Configuration.GetSection("Urls").GetSection("GetInformations").Value;

            var responseFromEndpoint = requestHelper.GetT(url, token);
            var responseData = JsonConvert.DeserializeObject<GetInformationResponseDTO>(responseFromEndpoint);

            var response = new StandardReturnDTO()
            {
                status_code = "200",
                message = "Success",
                data = responseData                
            };

            return Ok(response);
        }
    }
}
