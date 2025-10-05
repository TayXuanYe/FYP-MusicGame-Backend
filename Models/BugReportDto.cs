using System;
using System.ComponentModel.DataAnnotations;

namespace FYP_MusicGame_Backend.Models
{
    public class BugReportDto
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }
        
        [Required]
        public required string Description { get; set; }

        [Required]
        public required string StepsToReproduce { get; set; }
    }

    public class BugReportResponseDto
    {
        public int ReportId { get; set; }
        public int ReporterId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string StepsToReproduce { get; set; }
        public DateTime ReportTime { get; set; }
        public required string Status { get; set; }
    }
}