namespace Demo.ApplicationProcess.Api.Models
{
    public class FilterModel
    {

        public string Search { get; set; } = "";

        public string Sort { get; set; } = "Id";

        public string Order { get; set; } = "Asc";

        public int Page { get; set; } = 1;

    }
}
