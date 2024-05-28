using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1;

[ApiController]
[Route("api/")]
public class HospitalController : ControllerBase
{
    private readonly IHospitalRepository _repository;

    public HospitalController(IHospitalRepository repository)
    {
        _repository = repository;
    }
    [HttpPost]
    public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctor)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDTO request)
    {
        try

        {

            await _repository.AddPrescriptionAsync(request);

            return Ok("Recepta została dodana pomyślnie");

        }

        catch (Exception ex)

        {

            return BadRequest(ex.Message);

        }
    }
    
}