using kol1poprawa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace kol1poprawa.Services
{
    public class PrescriptonService : IPrescriptionService
    {
        private readonly IConfiguration _configuration;

        public PrescriptonService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Prescription> GetPrescriptions()
        {
            var result = new List<Prescription>();
            var medicament = new List<Medicament>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"Select Doctor.FirstName, Doctor.LastName, Patient.FirstName, Patient.LastName, Date, DueDate, Medicament.IdMedicament, Medicament.Name, Medicament.Description, Medicament.Type" +
                    $" FROM Prescription JOIN Doctor ON Prescription.IdDoctor = Doctor.IdDoctor" +
                    $" JOIN Patient ON Prescription.IdPatient = Patient.IdPatient" +
                    $" JOIN Prescription_Medicament ON Prescription.IdPrescription = Prescription_Medicament.IdPrescription" +
                    $" JOIN Medicament ON Prescription_Medicament.IdMedicament = Medicament.IdMedicament";
                connection.Open();

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var prescription = new Prescription
                    {
                        DoctorFirstName = dataReader["Doctor.FirstName"].ToString(),
                        DoctorLastName = dataReader["Doctor.LastName"].ToString(),
                        PatientFirstName = dataReader["Patient.FirstName"].ToString(),
                        PatientLastName = dataReader["Patient.Lastname"].ToString(),
                        Date = DateTime.Parse(dataReader["Date"].ToString()),
                        DueDate = DateTime.Parse(dataReader["DueDate"].ToString()),
                        IdMedicament = int.Parse(dataReader["IdMedicament"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Description = dataReader["Description"].ToString(),
                        Type = dataReader["Type"].ToString()
                    };
                    result.Add(prescription);
                }
            }

            return result;
        }

        public Boolean IsDoctorIDExists(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"SELECT IdDoctor FROM Doctor WHERE IdDoctor = {id}";
                connection.Open();

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    int result = int.Parse(dataReader["IdDoctor"].ToString());
                    if (result > 0)
                        return true;
                }
            }
            return false;
        }

        public int AddDoctor(Doctor doctor)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"SELECT MAX(IdDoctor) asd FROM Doctor";
                connection.Open();

                var dataReader = command.ExecuteScalar();

                doctor.IdDoctor = int.Parse(dataReader.ToString()) + 1;
                
            }


            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Doctor (IdDoctor, FirstName, LastName, Email) VALUES (@1, @2, @3, @4)";
                connection.Open();
                command.Parameters.AddWithValue("@1", doctor.IdDoctor);
                command.Parameters.AddWithValue("@2", doctor.FirstName);
                command.Parameters.AddWithValue("@3", doctor.LastName);
                command.Parameters.AddWithValue("@4", doctor.Email);

                return command.ExecuteNonQuery();
            }
        }
    }
}
