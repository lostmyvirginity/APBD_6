using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configuration;

public class PrescriptionConfig :  IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.IdPrescription).HasName("IdPrescription");

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.DueDate).IsRequired();

        builder.HasOne(e => e.DoctorNav)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdDoctor)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Doctor_FK");

        builder.HasOne(e => e.PatientNav)
            .WithMany(e => e.Prescriptions)
            .HasForeignKey(e => e.IdPatient)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Patient_FK");
        
        var list = new List<Prescription>();
        list.Add( new Prescription()
        {
            IdPrescription = 1,
            Date = new DateTime(2024,05,27),
            DueDate = new DateTime(2024, 05, 30),
            IdPatient = 1,
            IdDoctor = 1
        }); list.Add( new Prescription()
        {
            IdPrescription = 2,
            Date = new DateTime(2020,01,27),
            DueDate = new DateTime(2025, 06, 30),
            IdPatient = 2,
            IdDoctor = 2
        }); list.Add( new Prescription()
        {
            IdPrescription = 3,
            Date = new DateTime(2024,03,27),
            DueDate = new DateTime(2026, 01, 30),
            IdPatient = 3,
            IdDoctor = 3
        });
        builder.HasData(list);
    }
}