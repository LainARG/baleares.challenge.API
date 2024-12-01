using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using baleares.challenge.API.infrastructure.repository.interfaces;
using baleares.challenge.API.infrastructure.services.utilities;
using baleares.challenge.API.Application.Validation;
using baleares.challenge.API.Application.DTO_s;
using baleares.challenge.API.model.contacts;
using Microsoft.IdentityModel.Tokens;

namespace Baleares.Challenge.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _repository;

    public ContactsController(IContactRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ContactDTO contct)
    {
        try
        {
            if (!ModelValidator.ValidateContact(contct)) return BadRequest(ModelValidator.ErrorMessage);
            var contact = new Contact(contct.UserId, contct.Name, contct.Company, contct.Email, contct.BirthDate, contct.Phone, contct.PhoneWork, contct.Address, contct.Province, contct.City);
            await _repository.AddAsync(contact);
            return Ok("Alta de contacto exitosa.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var contacts = await _repository.GetAllAsync();
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return NotFound("Contacto inexistente.");
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteContact(ContactDTO contct)
    {
        try
        {
            var ct = new Contact(contct.UserId, contct.Name, contct.Company, contct.Email, contct.BirthDate, contct.Phone, contct.PhoneWork, contct.Address, contct.Province, contct.City);
            var contact = await _repository.Search(ct);
            if (contact == null) return BadRequest("Contacto inexistente.");
            await _repository.DeleteAsync(contact);
            return Ok("Contacto eliminado.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactById(int id)
    {
        try
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return BadRequest("Contacto inexistente.");
            await _repository.DeleteAsync(contact);
            return Ok("Contacto eliminado.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }
    [HttpGet("SearchByPhoneOrEmail")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        try
        {
            var contacts = await _repository.SearchAsync(query);
            return contacts.Any() ? Ok(contacts) : Ok("No hay coincidencias.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }
    [HttpGet("GetByCityOrProvince")]
    public async Task<IActionResult> GetByCity([FromQuery] string query)
    {
        try
        {
            if (query.IsNullOrEmpty()) { return BadRequest("Query inválido."); }
            var contacts = await _repository.GetByCityOrProvinceAsync(query);
            return contacts.Any() ? Ok(contacts) : Ok("No hay coincidencias.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContactDTO contct)
    {
        try
        {
            var ct = await _repository.GetByIdAsync(id);
            if (ct == null) return NotFound("Contacto inexistente.");
            if (!ModelValidator.ValidateContact(contct)) return BadRequest(ModelValidator.ErrorMessage);
            ct.UserId = contct.UserId;
            ct.Name = contct.Name;
            ct.Company = contct.Company;
            ct.Email = contct.Email;
            ct.BirthDate = contct.BirthDate;
            ct.Phone = contct.Phone;
            ct.PhoneWork = contct.PhoneWork;
            ct.Address = contct.Address;
            ct.Province = contct.Province;
            ct.City = contct.City;
            await _repository.UpdateAsync(ct);
            return Ok("Contacto actualizado.");
        }
        catch (Exception ex)
        {
            return BadRequest(ExceptionHelper.GetExceptionMessage(ex));
        }
    }


}

