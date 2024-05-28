using WebApplication1.DTOs;

namespace WebApplication1.Repositories;

public interface IHospitalRepository
{
    public string AddDoctorAsync(DoctorDTO doctor);
    Task AddPrescriptionAsync(PrescriptionRequestDTO request);
}