namespace baleares.challenge.API.model.contacts;

public partial class Contact
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

    public Contact(int userId, string name, string company, string email, DateOnly? birthDate,string phone, string phoneWork, string address,string province, string city)
    {
        UserId = userId;
        Name = name;
        Company = company;
        Email = email;
        BirthDate = birthDate;
        Phone = phone;
        PhoneWork = phoneWork;
        Address = address;
        Province = province;
        City = city;
    }
}
