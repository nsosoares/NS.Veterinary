using NS.Veterinaria.Api.Enums;

namespace NS.Veterinary.Api.ViewModels
{
    public class AnimalViewModel : EntityBaseViewModel
    {
        public int Age { get; set; }
        public string Race { get; set; }
        public ESize Size { get; set; }
    }
}
