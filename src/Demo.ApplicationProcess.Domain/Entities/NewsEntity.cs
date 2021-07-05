using System;

namespace Demo.ApplicationProcess.Domain.Entities
{
    /// <summary>
    /// News table
    /// </summary>
    public class NewsEntity
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PostDate { get; set; } 

    }
}
