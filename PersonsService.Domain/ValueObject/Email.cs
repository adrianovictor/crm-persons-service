using System.Text.RegularExpressions;
using PersonsService.Domain.Validations;

namespace PersonsService.Domain.ValueObject;

public class Email : IEquatable<Email>
{
    public string Address { get; }

    public Email(string address)
    {
        Address = address.Trim().ToLower();
        if (!IsValid(Address))
        {
            throw new ArgumentException("Email inválido.", nameof(address));
        }
    }

    public static bool IsValid(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Email);
    }

    public bool Equals(Email other)
    {
        return other != null && Address == other.Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }

    public override string ToString()
    {
        return Address;
    }

    public static implicit operator string(Email email) => email.Address;
    public static explicit operator Email(string address) => new Email(address);
}