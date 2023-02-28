using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inz.Data
{
    [Table("SIATKOWKA")]
    public class Siatkowka
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "Kontakt")]
        public string Contact { get; set; }
        [MaxLength(400)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
