using WebApplication1.DTOs;

namespace WebApplication1.Repositories;

public interface IHospitalRepository
{
    public Task<PatientInfoDTO> GetPatient(PatientDTO patientInfoDto);
    Task AddPrescriptionAsync(PrescriptionRequestDTO request);
}