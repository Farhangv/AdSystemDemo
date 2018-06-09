using System.ComponentModel.DataAnnotations;

namespace AdSystem.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}