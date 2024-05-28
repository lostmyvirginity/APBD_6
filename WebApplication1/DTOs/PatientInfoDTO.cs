namespace WebApplication1.DTOs;

public class PatientInfoDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<DoctorDTO> Doctors { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}