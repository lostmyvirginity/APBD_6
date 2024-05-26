using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

namespace WebApplication1;

[ApiController]
[Route("api/")]
public class HospitalController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO doctor)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDTO request)
    {
        return Ok();
    }
}