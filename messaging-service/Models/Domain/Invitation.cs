using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace messaging_service.Models.Domain
{
    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsAccepted { get; set; } = false;

    }
}
