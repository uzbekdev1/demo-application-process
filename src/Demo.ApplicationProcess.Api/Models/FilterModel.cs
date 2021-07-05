using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Demo.ApplicationProcess.Api.Models
{
    public class FilterModel
    {
         
        public string Search { get; set; }

        [FromQuery]
        [DefaultValue("Id")]
        public string Sort { get; set; } 

        [FromQuery]
        [DefaultValue("Asc")]
        public string Order { get; set; }

        [FromQuery]
        [DefaultValue(1)]
        public int Page { get; set; }

    }
}
