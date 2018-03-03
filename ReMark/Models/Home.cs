using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReMark.Models
{
    public class Home
    {
        [AllowHtml]
        public string content { get; set; }

        [Required(ErrorMessage = "You Should have a Name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You Should Enter Password")]
        public string Password { get; set; }

    }
}