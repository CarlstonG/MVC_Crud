using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{

    public class Expense
    {
        public int Id { get; set; }
        public required string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        // Link the expense to the user who created it
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }
    }
}