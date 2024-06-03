namespace WebApplication4.DTO
{
    public class Class
    {
    }

    public class PrescriptionRequestModel
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int IdDoctor { get; set; }
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<MedicamentsViewModel> Medicaments { get; set; } = new List<MedicamentsViewModel>();
    }

    public class MedicamentsViewModel
    {
        public int IdMedicament { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; } = null!;
    }
}
