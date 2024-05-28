namespace WebApplication1.DTOs;

public class PrescriptionRequestDTO
{
    public int IdPrescription { get; set; }

    public PatientRequestDTO Patient { get; set; }
    public List<MeidcamentRequestDTO> Medicaments { get; set; }
    public DoctorRequestDTO Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}