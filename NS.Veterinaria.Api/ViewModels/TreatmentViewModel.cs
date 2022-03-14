namespace NS.Veterinary.Api.ViewModels
{
    public class TreatmentViewModel : EntityViewModel
    {
        public Guid VeterinarianId { get; set; }
        public Guid AnimalId { get; set; }
        public string Recipe { get; set; }
    }
}
