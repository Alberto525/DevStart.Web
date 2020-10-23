using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{

    [NotMapped]
    public class ImportarEx
    {
        public HttpPostedFileBase file { get; set; }
    }
}