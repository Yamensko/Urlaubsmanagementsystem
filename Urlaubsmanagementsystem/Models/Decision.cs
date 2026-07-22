namespace Urlaubsmanagementsystem.Models // Dieser Namespace MUSS vorhanden sein
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Decision")]
    public class Decision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte DecisionId { get; set; }

        public string Status { get; set; }  // change it later to id in the database

        public enum DecisionStatus : byte
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3
        }

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    }
}