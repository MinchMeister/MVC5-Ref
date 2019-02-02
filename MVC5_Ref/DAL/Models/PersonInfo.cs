using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVC5_Ref.DAL.Models
{
    [Table("MVC5_Ref.PersonInfo")]
    public class PersonInfo
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your First Name")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[\w\-. ']+$", ErrorMessage = "Invalid Character(S)")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

    }
}
