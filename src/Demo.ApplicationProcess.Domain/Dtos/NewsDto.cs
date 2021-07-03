using System;

namespace Demo.ApplicationProcess.Domain.Dtos
{
    /// <summary>
    /// News object
    /// </summary>
    public class NewsDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
         
        public DateTime PostDate { get; set; } 

    }
}