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

    public async Task AddPrescriptionAsync(PrescriptionRequestDTO req)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.IdPatient == req.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = req.Patient.IdPatient,
                FirstName = req.Patient.FirstName,
                LastName = req.Patient.LastName,
                BirthDate = req.Patient.BirthDate
            };
            _context.Patients.Add(patient);
        }

        var medicamentIds = req.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        var missingMedicaments = medicamentIds.Except(existingMedicaments).ToList();
        if (missingMedicaments.Any())
        {
            throw new KeyNotFoundException("Some medicaments are not found in the database: " +
                                           string.Join(", ", missingMedicaments));
        }

        if (req.Medicaments.Count > 10)
        {
            throw new Exception("Too many Medicaments in prescription");
        }

        if (req.Date > req.DueDate)
        {
            throw new Exception("Date and due date is invalid");
        }

        var prescription = new Prescription()
        {
            Date = req.Date,
            DueDate = req.DueDate,
            IdPatient = req.Patient.IdPatient,
            IdDoctor = req.Doctor.IdDoctor
        };
        _context.Prescriptions.Add(prescription);

        await _context.SaveChangesAsync();
    }
}