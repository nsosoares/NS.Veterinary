namespace NS.Veterinary.Api.ViewModels
{
    public class TreatmentDetailedViewModel : EntityViewModel
    {
        public AnimalViewModel Animal { get; set; }
        public VeterinarianViewModel Veterinarian { get; set; }
        public string Recipe { get; set; }
    }
}
