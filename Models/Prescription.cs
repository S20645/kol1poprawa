using System;
using System.Collections.Generic;


public class Prescription
{
	public string DoctorFirstName { get; set; }
	public string DoctorLastName { get; set; }
	public string PatientFirstName { get; set; }
	public string PatientLastName { get; set; }
	public DateTime Date { get; set; }
	public DateTime DueDate { get; set; }
	public int IdMedicament { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string Type { get; set; }
    //public List<Medicament> Medicament { get; set; }
}
