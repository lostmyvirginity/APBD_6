using APBD_Zadanie_6.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class HospitalRepository : IHospitalRepository
{
    private readonly Context _context;

    public HospitalRepository(Context context)
    {
        _context = context;
    }

    public string AddDoctorAsync(DoctorDTO doctor)
    {
        throw new NotImplementedException();
    }

    public async Task AddPrescriptionAsync(PrescriptionRequestDTO request)
    {
        throw new NotImplementedException();
    }
}
