using PersonsService.Domain.Core;
using PersonsService.Domain.Enum;
using PersonsService.Domain.Validations;

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
    public int? Age { get; protected set; }
    public Gender Gender { get; protected set; }
    public MaritalStatus MaritalStatus { get; protected set; }
    public string? Nationality { get; protected set; }
    public string? Naturality { get; protected set; }
    public string? Email { get; protected set;}
    public string? Notes { get; protected set; }
    public int? EnterpriseId { get; protected set; }

    protected Person() 
    { 
        UniqueId = Guid.NewGuid();
    }

    public Person(string name, string? picture, string? alias, string? jobTitle, DateTime? dateOfBirth, int? age,
        string? nationality, string? naturality, string? email, string? notes, int? enterpriseId,
        Gender gender, MaritalStatus maritalStatus = MaritalStatus.None, Status status = Status.Active) : this()
    {
        name.ThrowIfNullOrWhiteSpace(nameof(name));        

        Name = name;
        Picture = picture;
        Alias = alias;
        JobTitle = jobTitle;
        DateOfBirth = dateOfBirth;
        Age = age;
        Gender = gender;
        MaritalStatus = maritalStatus;
        Nationality = nationality;
        Naturality = naturality;
        Email = email;
        Notes = notes;
        EnterpriseId = enterpriseId;
        Status = status;
    }

    public static Person Create(string name, string? picture, string? alias, string? jobTitle, DateTime? dateOfBirth, 
        int? age, string? nationality, string? naturality, string? email, string? notes, int? enterpriseId, Gender gender, MaritalStatus maritalStatus, Status status)
    {
        return new(name, picture, alias, jobTitle, dateOfBirth, age, nationality, naturality, email, notes, enterpriseId, gender, maritalStatus, status);
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
        Email = $"DELETED.{DateTime.Now.ToString("yyyyMMddHHmmss")}.{Email}";

        ChangeStatus(Status.Delete);
    }

}
