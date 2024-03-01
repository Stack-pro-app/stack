﻿using messaging_service.models.domain;

namespace messaging_service.models.dto.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Last_login { get; set; }
        public int? WorkspaceId { get; set; }
        public int AuthId { get; set; }
    }
}
