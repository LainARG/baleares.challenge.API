namespace baleares.challenge.API.Application.DTO_s;

    public partial class ContactDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Company { get; set; }

        public string? Email { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Phone { get; set; }

        public string? PhoneWork { get; set; }

        public string? Address { get; set; }

        public string? Province { get; set; }

        public string? City { get; set; }
    }

