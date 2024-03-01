using messaging_service.models.dto;
using messaging_service.models.dto.Response;
using messaging_service.models.domain;
using messaging_service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWT;
using JWT.Builder;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;
using JWT.Algorithms;
using JWT.Serializers;
using AutoMapper;
using messaging_service.models.domain;


namespace messaging_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(UserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromBody]UserDto userDto)
        {

            /*IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new RS256Algorithm(certificate);
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

            var json = decoder(token);
            int generalId = json.Id;*/

            try
            {
                var user = _mapper.Map<User>(userDto);
                ResponseDto response = new ResponseDto();
                bool result = await _userRepository.CreateUserAsync(user);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Message = "Successfully Created !";
                    return Ok(response);
                }
                else
                {
                    throw new Exception("Failled to add !");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

        }


    }
}
