using Library_management_system.DTOs;
using Library_management_system.Enums;
using Library_management_system.Extensions;
using Library_management_system.Models.database;
using Library_management_system.Models.Exceptions;
using Library_management_system.Models.Response;
using Library_management_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CustomExceptionsNS = Library_management_system.Models.Exceptions;
namespace Library_management_system.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController:ControllerBase
    {
        private readonly bookServices _bookService;

        public BooksController(bookServices services)
        {
            _bookService= services;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var result = await _bookService.GetAllBooks();
                return Ok(new Response
                {
                    Data = result,
                    ResponseCode = 200,
                    Success = true,
                });
            }
            catch (CustomExceptionsNS.CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }

        [Authorize(Roles ="publisher,admin")]
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBooks([FromBody] AddBookDTO dto) {
            try
            {
                var result = await _bookService.AddBooks(dto);
                return Ok(new Response
                {
                    Data = result,
                    ResponseCode = 200,
                    Success = true,
                });
            }
            catch (CustomExceptionsNS.CustomExceptions ex) {
                return StatusCode (ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }
        [Authorize(Roles = "publisher,admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO dto)
        {
            try
            {
                var result = await _bookService.UpdateBook(dto);
                return Ok(new Response
                {
                    Data = result,
                    ResponseCode = 200,
                    Success = true,
                });
            }
            catch (CustomExceptionsNS.CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }
        [Authorize(Roles = "publisher,admin")]
        [HttpPost("/Delete")]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            try
            {
                var result = await DeleteBook(id);
                return Ok(new Response
                {
                    Data = result,
                    ResponseCode = 200,
                    Success = true,
                });
            }
            catch (CustomExceptionsNS.CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }
        [HttpPost("LoanBook")]
        public async Task<IActionResult> LoanBook([FromBody] LoanBookDTO dto)
        
{

            try
            {
                if (this.GetUserType() != UserTypes.user) {
                    throw new CustomExceptionsNS.CustomExceptions("invalid user type", 400);
                }
                var userid = this.GetUserID();
                var result = await _bookService.LoanBook(dto, userid);
                return Ok();
                
            }


            catch (CustomExceptionsNS.CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }
        [HttpPost("ReturnBook")]
        public async Task<IActionResult> ReturnBook([FromQuery] int loanID)

        {
            try
            {
                if (this.GetUserType() != UserTypes.user)
                {
                    throw new CustomExceptionsNS.CustomExceptions("invalid user type", 400);
                }
                var userid = this.GetUserID();
                var result = await _bookService.ReturnBook(loanID, userid);
                return Ok();

            }

            catch (CustomExceptionsNS.CustomExceptions ex)
            {
                return StatusCode(ex.StatusCode, new Response
                {
                    Data = null,
                    ResponseCode = 400,
                    Success = false,
                    ResponseMessage = ex.Message
                });
            }
        }

    }
}
