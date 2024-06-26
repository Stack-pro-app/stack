﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace messaging_service.models.domain
{
    public class Channel
    {
        public int Id { get; set; }
        public string ChannelString {  get; set; }=null!;
        [MaxLength(30)]
        public string Name { get; set; }=null!;
        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime? Created_at { get; set; }
        public bool Is_private { get; set; } = true;
        public bool? Is_OneToOne { get; set; } = false;
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public ICollection<Member> Members { get; } = new List<Member>();
        public ICollection<Chat> Messages { get; } = new HashSet<Chat>();
    }
}
