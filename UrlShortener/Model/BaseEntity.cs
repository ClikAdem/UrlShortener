using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Model
{
    public abstract partial class BaseEntity
    {
        [NaturalKey]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true; //For soft deletion

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? DeletedBy { get; set; }
    }
}
