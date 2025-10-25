using PersonsService.Domain.Core;
using PersonsService.Domain.Enum;
using PersonsService.Domain.Validations;
using PersonsService.Domain.ValueObject;

namespace PersonsService.Domain.Entities;

public class Person : Statusable<Person>
{
private readonly List<PersonDocument> _documents = [];

    public Guid UniqueId { get; protected set; }
    public string Name { get; protected set; }
    public string? Picture { get; protected set; }
    public string? Alias { get; protected set; }
    public IReadOnlyCollection<PersonDocument> Documents => _documents;
    public string? JobTitle { get; protected set; }
    public DateTime? DateOfBirth { get; protected set; }
    public int? CalculatedAge 
    {
        get 
        {
            if (DateOfBirth.HasValue)
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Value.Year;
                
                if (DateOfBirth.Value.Date > today.AddYears(-age)) age--;
                return age;
            }
            return null;
        }
    }
    public Gender Gender { get; protected set; }
    public MaritalStatus MaritalStatus { get; protected set; }
    public string? Nationality { get; protected set; }
    public string? Naturality { get; protected set; }
    public Email Email { get; protected set;}
    public string? Notes { get; protected set; }
    public int? EnterpriseId { get; protected set; }

    protected Person() 
    { 
        UniqueId = Guid.NewGuid();
    }

    public Person(
        Guid uniqueId, string name, Gender gender, Status status, MaritalStatus maritalStatus, 
        string? picture, string? alias, string? jobTitle, DateTime? dateOfBirth, 
        string? nationality, string? naturality, string email, string? notes, int? enterpriseId, 
        IReadOnlyCollection<PersonDocument> documents) : this()
    {
        UniqueId = uniqueId; // Builder define o ID ou deixa o padrão
        Name = name;
        Gender = gender;
        MaritalStatus = maritalStatus;
        Status = status;
        
        // Atribuição dos demais campos
        Picture = picture;
        Alias = alias;
        JobTitle = jobTitle;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        Naturality = naturality;
        Email = new Email(email);
        Notes = notes;
        EnterpriseId = enterpriseId;
        
        // Adiciona documentos (se necessário)
        if (documents != null) _documents.AddRange(documents);
    }

    public void RenameIt(string name, string? alias)
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name));

        Name = name;
        Alias = alias;
    }

    public void ChangeGender(Gender gender)
    {
        Gender = gender;
    }

    public void ChangeMaritalStatus(MaritalStatus maritalStatus)
    {
        MaritalStatus = maritalStatus;
    }

    public void ChangeJobTitle(string jobTitle)
    {
        JobTitle = jobTitle;
    }

    public void ChangeDateOfBirth(DateTime? dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void ChangePicture(string picture)
    {
        Picture = picture;
    }

    public void ChangeNote(string notes)
    {
        Notes = notes;
    }

    public override void ChangeStatus(Status status)
    {
        Status = status;
    }

    public void ChangeEnterprise(int? enterpriseId)
    {
        EnterpriseId = enterpriseId;
    }

    public void AddDocument(PersonDocument personDocument)
    {
        var exists = _documents.Exists(personDocument.Equals);
        if (!exists) 
        {
            _documents.Add(personDocument);
        }
    }

    public void RemoveDocument(PersonDocument personDocument)
    {
        var exists = _documents.Exists(personDocument.Equals);
        if (exists) 
        {
            _documents.Remove(personDocument);
        }
    }

    public void Delete()
    {
        Name = $"DELETED.{DateTime.Now.ToString("yyyyMMddHHmmss")}.{Name}";
        Email = new Email($"DELETED.{DateTime.Now.ToString("yyyyMMddHHmmss")}.{Email.Address}");

        ChangeStatus(Status.Delete);
    }

}
