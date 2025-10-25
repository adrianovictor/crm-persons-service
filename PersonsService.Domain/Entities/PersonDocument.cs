using PersonsService.Domain.Core;

namespace PersonsService.Domain.Entities;

public class PersonDocument : Entity<PersonDocument>
{
    public Person Person { get; protected set; }
    public int DocumentType { get; protected set; }
    public string DocumentNumber { get; protected set; }

    protected PersonDocument() {}
    
    public PersonDocument(int documentType, string documentNumber): this()
    {
        DocumentType = documentType;
        DocumentNumber = documentNumber;
    }

    public static PersonDocument Create(int documentType, string documentNumber)
    {
        return new(documentType, documentNumber);
    }

    public void ChangeDocument(int documentType, string documentNumber)
    {
        DocumentType = documentType;
        DocumentNumber = documentNumber;
    }

    public void ChangePerson(Person person)
    {
        ArgumentNullException.ThrowIfNull(person);
        Person = person;
    }
}
