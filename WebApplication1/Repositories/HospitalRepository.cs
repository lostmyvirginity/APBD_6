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

    public async Task<PatientInfoDTO> GetPatient(PatientDTO patientDto)
    {
        var patient = await _context.Patients

           .Where(p => p.IdPatient == patientDto.IdPatient)

           .Select(p => new PatientInfoDTO

            {

                IdPatient = p.IdPatient,

                FirstName = p.FirstName,

                LastName = p.LastName,

                BirthDate = p.BirthDate,

                Doctors = p.Prescriptions

                   .Select(pr => new DoctorDTO

                    {

                        IdDoctor = pr.DoctorNav.IdDoctor,

                        FirstName = pr.DoctorNav.FirstName,

                        LastName = pr.DoctorNav.LastName,

                        Email = pr.DoctorNav.Email

                    })

                   .Distinct()

                   .ToList(),

                Prescriptions = p.Prescriptions

                   .OrderBy(pr => pr.DueDate)

                   .Select(pr => new PrescriptionDTO

                    {

                        IdPrescription = pr.IdPrescription,

                        Date = pr.Date,

                        DueDate = pr.DueDate,

                        Doctor = new DoctorDTO

                        {

                            IdDoctor = pr.DoctorNav.IdDoctor,

                            FirstName = pr.DoctorNav.FirstName,

                            LastName = pr.DoctorNav.LastName,

                            Email = pr.DoctorNav.Email

                        },

                        Medicaments = pr.PrescriptionMedicaments

                           .Select(pm => new MedicamentDTO

                            {

                                IdMedicament = pm.IdMedicamentNav.IdMedicament,

                                Name = pm.IdMedicamentNav.Name,

                                Description = pm.IdMedicamentNav.Description,

                                Type = pm.IdMedicamentNav.Type,


                            })

                           .ToList()

                    })

                   .ToList()

            })

           .FirstOrDefaultAsync();


        return patient;

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