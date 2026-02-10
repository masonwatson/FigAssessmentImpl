using FigAssessmentImpl.Application.Users;
using FigAssessmentImpl.Application.Users.CreateUsers;
using FigAssessmentImpl.Application.Users.GetUsers;
using FigAssessmentImpl.Application.Users.ValidateUsers;
using Microsoft.AspNetCore.Mvc;

namespace FigAssessmentImpl.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService productService)
        {
            _userService = productService;
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetUserResponse>>> GetUsersAsync(GetUserQueryOptions options, CancellationToken ct)
        {
            try
            {
                var result = await _userService.GetUsersAsync(options, ct);
                return Ok(result);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
            }
            catch (ArgumentException ex)
            {
                // Log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log with logger

                return StatusCode(500);
            }
        }

        // GET api/users
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserResponse>> GetUserByIdAsync([FromRoute] int id, CancellationToken ct)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id, ct);
                return Ok(result);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
            }
            catch (ArgumentException ex)
            {
                // Log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log with logger

                return StatusCode(500);
            }
        }

        // POST api/users/validate
        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateUserAsync([FromBody] ValidateUserRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _userService.ValidateUserAsync(request, ct);
                return Ok(result);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
            }
            catch (ArgumentException ex)
            {
                // Log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log with logger

                return StatusCode(500);
            }
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _userService.CreateUserAsync(request, ct);
                return Ok(result);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                return StatusCode(499, "Client canceled request");
            }
            catch (ArgumentException ex)
            {
                // Log with logger

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log with logger

                return StatusCode(500);
            }
        }
    }
}
