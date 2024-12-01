using System.Text.RegularExpressions;
using baleares.challenge.API.Application.DTO_s;


namespace baleares.challenge.API.Application.Validation;

public class ModelValidator
{
    private const string UsernameRegex = @"^(?!\s)(?!.*\s$)[A-Za-z0-9_]{3,60}$";
    private const string EmailRegex = @"^(?!.{151,}$)([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$";
    private const string NameRegex = @"^[A-Za-z0-9. ñÑ]{3,150}$";
    private const string CompanyRegex = @"^[A-Za-z0-9. ñÑ]{3,60}$";
    private const string PhoneRegex = @"^\d{7,16}$";
    private const string AddressRegex = @"^[A-Za-z0-9., ]{3,150}$";
    private const string PersonNameRegex = @"^[A-Za-zñÑ]{3,60}$";

    public static string ErrorMessage { get; private set; }

    public static bool ValidateUser(UserDTO user)
    {
        if (string.IsNullOrEmpty(user.UserName) || !Regex.IsMatch(user.UserName, UsernameRegex))
        {
            ErrorMessage = "Username solo acepta alfanuméricos y guiones bajos, entre 3 y 60 caracteres.";
            return false;
        }
        if (string.IsNullOrEmpty(user.Email) || !Regex.IsMatch(user.Email, EmailRegex))
        {
            ErrorMessage = "Email debe contener al menos '@', un dominio y un alfanumérico, menos de 150 caracteres.";
            return false;
        }
        if (string.IsNullOrEmpty(user.FirstName) || !Regex.IsMatch(user.FirstName, PersonNameRegex))
        {
            ErrorMessage = "FirstName solo permite alfabéticos, entre 3 y 60 caracteres.";
            return false;
        }
        if (string.IsNullOrEmpty(user.LastName) || !Regex.IsMatch(user.LastName, PersonNameRegex))
        {
            ErrorMessage = "LastName solo permite alfabéticos, entre 3 y 60 caracteres.";
            return false;
        }
        ErrorMessage = null;
        return true;
    }

    public static bool ValidateContact(ContactDTO contact)
    {
        if (string.IsNullOrEmpty(contact.Name) || !Regex.IsMatch(contact.Name, NameRegex))
        {
            ErrorMessage = "Name solo permite alfanuméricos y puntos, 3 a 60 caracteres.";
            return false;
        }

        if (string.IsNullOrEmpty(contact.Company) || !Regex.IsMatch(contact.Company, CompanyRegex))
        {
            ErrorMessage = "Company solo Permite alfanuméricos y puntos, entre 3 y 60 caracteres.";
            return false;
        }

        if (string.IsNullOrEmpty(contact.Email) || !Regex.IsMatch(contact.Email, EmailRegex))
        {
            ErrorMessage = "Email solo Permite alfanuméricos y puntos, entre 3 y 60 caracteres";
            return false;
        }

        if (contact.BirthDate == null || DateTime.Now.Year - contact.BirthDate.Value.Year < 18)
        {
            ErrorMessage = "La edad debe ser mayor o igual a 18 años.";
            return false;
        }

        if (string.IsNullOrEmpty(contact.Phone) || !Regex.IsMatch(contact.Phone, PhoneRegex))
        {
            ErrorMessage = "Phone solo permite numéricos, entre 7 y 16 dígitos.";
            return false;
        }

        if (string.IsNullOrEmpty(contact.PhoneWork) || !Regex.IsMatch(contact.PhoneWork, PhoneRegex))
        {
            ErrorMessage = "PhoneWork solo permite numéricos, entre 7 y 16 dígitos.";
            return false;
        }

        if (string.IsNullOrEmpty(contact.Address) || !Regex.IsMatch(contact.Address, AddressRegex))
        {
            ErrorMessage = "Address solo permite alfanuméricos, comas y puntos, entre 3 y 150 caracteres.";
            return false;
        }

        ErrorMessage = null;
        return true;
    }
}