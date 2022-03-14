using NS.Veterinary.Api.DTOs;

namespace NS.Veterinary.Api.ViewModels
{
    public abstract class EntityViewModel : Dto
    {
        public Guid? Id { get; set; }
        public DateTime? CreationDate { get; set; }

        public void ToGenerate()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
