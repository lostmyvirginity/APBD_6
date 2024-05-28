using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class HospitalController : ControllerBase
{
    private readonly IHospitalRepository _repository;

    public HospitalController(IHospitalRepository repository)
    {
        _repository = repository;
    }
    [HttpPost]
    public async Task<IActionResult> PatientInfo([FromBody] PatientRequestDTO patientReq)
    {
        var patient = await _repository.GetPatient(new PatientDTO()
        {
            IdPatient = patientReq.IdPatient,
            FirstName = patientReq.FirstName,
            LastName = patientReq.LastName,
            BirthDate = patientReq.BirthDate
        });


        if (patient == null)

        {

            return NotFound();

        }


        return Ok(patient);
    }

    [HttpPost("AddPrescription")]
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