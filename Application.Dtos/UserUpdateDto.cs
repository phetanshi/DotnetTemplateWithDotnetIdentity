﻿namespace Application.Dtos
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string Email { get; set; } = null!;
        public int? ManagerId { get; set; }
    }
}