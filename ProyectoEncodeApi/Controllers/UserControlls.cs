using DataAccess.Generic;
using Entities.Domain;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoEncodeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IGenericRepository<Usuario> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IGenericRepository<Usuario> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _genericRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _genericRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            else { return Ok(user); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario user)
        {
            try
            {
                if (await _genericRepository.CreateAsync(user))
                {
                    _unitOfWork.Commit();
                    return Ok();
                }
                else { return NotFound(); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Usuario user)
        {
            if (user == null)
                return BadRequest();

            try
            {
                if (await _genericRepository.UpdateAsync(id, user))
                {
                    await _genericRepository.UpdateAsync(id, user);
                    _unitOfWork.Commit();
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, "Error del servidor");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genericRepository.DeleteByIdAsync(id);
            _unitOfWork.Commit();
            
            return NoContent();
        }
    }
}
    

