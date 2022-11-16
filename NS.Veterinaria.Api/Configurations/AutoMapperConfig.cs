using AutoMapper;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.ViewModels;

namespace NS.Veterinary.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AnimalViewModel, Animal>()
                .ConstructUsing(animalVm =>
                new Animal(
                    animalVm.Id.Value
                    , animalVm.CreationDate.Value
                    , animalVm.Name
                    , animalVm.Age
                    , animalVm.Race
                    , animalVm.Size))
                .ReverseMap();

            CreateMap<VeterinarianViewModel, Veterinarian>()
                .ConstructUsing(veterinarianVm =>
                new Veterinarian(
                    veterinarianVm.Id.Value
                    , veterinarianVm.CreationDate.Value
                    , veterinarianVm.Name
                    , veterinarianVm.Crmv
                    ))
                .ReverseMap();

            CreateMap<TreatmentViewModel, Treatment>()
                .ConstructUsing(treatmentVm =>
                new Treatment(
                    treatmentVm.Id.Value
                    , treatmentVm.CreationDate.Value
                    , treatmentVm.VeterinarianId
                    , treatmentVm.AnimalId
                    , treatmentVm.Recipe))
                .ReverseMap();

            CreateMap<Treatment, TreatmentDetailedViewModel>()
                .ReverseMap();
        }
    }
}
