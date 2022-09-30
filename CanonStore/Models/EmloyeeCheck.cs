using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanonStore.Models
{
    public class EmloyeeCheck
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("[0-9]{3}", ErrorMessage = "Must be 3 digits")]
        public string Password { get; set; }
        public string LoginErrorMessage { get; internal set; }
    }
}