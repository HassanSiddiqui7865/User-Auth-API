using backend.DTOS;
using backend.Model;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly bookServices bookServices;
        public BookController(TestDBContext context)
        {
            this.bookServices = new bookServices(context);
        }
       
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateBook([FromBody] AddBook addBook)
        {
            try
            {
                var savedBook = await bookServices.CreateBook(addBook);
                return Ok(savedBook);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var BookList = await bookServices.GetAll();
                return Ok(BookList);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                var findBook = await bookServices.GetById(id);
                if(findBook != null)
                {
                    return Ok(findBook);
                }
                return NotFound("Book Not found");
               
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            try
            {
                var findBook = await bookServices.GetById(id);
                if (findBook != null)
                {
                    await bookServices.DeleteBook(findBook);
                    return Ok();
                }
                return NotFound("Book Not found");

            }catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateBook([FromBody] AddBook book,Guid id)
        {
            try
            {
                var findBook = await bookServices.GetById(id);
                if (findBook != null)
                {
                    await bookServices.UpdateBook(findBook, book);
                    return Ok("Updated");
                }
                return NotFound("Book not Found");
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
