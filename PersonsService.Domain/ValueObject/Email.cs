using System.Text.RegularExpressions;
using PersonsService.Domain.Validations;

namespace PersonsService.Domain.ValueObject;

public class Email
{
    private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    public string Address { get; }

    public Email(string address)
    {
        address.ThrowIfNullOrWhiteSpace(nameof(address));

        if (!EmailRegex.IsMatch(address))
        {
            throw new ArgumentException("Invalid email format.", nameof(address));
        }

        Address = address;
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