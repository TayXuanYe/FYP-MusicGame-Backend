using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP_MusicGame_Backend.Models
{
    public class BugReport
    {
        [Key]
        public int ReportId { get; set; }
        
        [Required]
        public int ReporterId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }
        
        [Required]
        public required string Description { get; set; }
        
        public string? StepsToReproduce { get; set; }
        
        [Required]
        public DateTime ReportTime { get; set; } = DateTime.Now;
        
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        
        [ForeignKey("ReporterId")]
        public required User Reporter { get; set; }
    }
}