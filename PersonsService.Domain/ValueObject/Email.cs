using PersonsService.Domain.Validations;

namespace PersonsService.Domain.ValueObject;

public class Email
{
    public string Address { get; }

    public Email(string address)
    {
        address.ThrowIfNullOrWhiteSpace(nameof(address));
        if (!IsValidEmail(address))
        {
            throw new ArgumentException("Invalid email format.", nameof(address));
        }

        Address = address;
    }

    private bool IsValidEmail(string email)
    {
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
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Address == ((Email)obj).Address;
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }

    public override string ToString()
    {
        return Address;
    }
}