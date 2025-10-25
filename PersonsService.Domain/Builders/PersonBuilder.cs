using PersonsService.Domain.Entities;
using PersonsService.Domain.Enum;
using PersonsService.Domain.Validations;

namespace PersonsService.Domain.Builders;

public class PersonBuilder
{
    // Campos Essenciais (que o Builder DEVE ter)
    private string _name = string.Empty;
    private Gender _gender = Gender.NotSpecified;

    // Campos Opcionais (com valores padrões)
    private string? _picture;
    private string? _alias;
    private string? _jobTitle;
    private DateTime? _dateOfBirth;
    private string? _nationality;
    private string? _naturality;
    private string? _email;
    private string? _notes;
    private int? _enterpriseId = null;
    private MaritalStatus _maritalStatus = MaritalStatus.None;
    private Status _status = Status.Active;
    private Guid _uniqueId = Guid.Empty;
    private readonly List<PersonDocument> _documents = [];

    // CONSTRUTOR: Para iniciar o Builder
    public PersonBuilder() { }
    
    // Método estático para uso rápido: new PersonBuilder().WithXXX().Build()
    public static PersonBuilder New() => new PersonBuilder();

    // --- Métodos de Configuração (Fluent Interface) ---
    
    public PersonBuilder WithName(string name) 
    {
        // Validação do campo obrigatório no próprio Builder
        name.ThrowIfNullOrWhiteSpace(nameof(name)); 
        _name = name;
        return this;
    }

    public PersonBuilder WithGender(Gender gender) 
    {
        _gender = gender;
        return this;
    }

    public PersonBuilder WithOptionalData(
        string? picture = null, string? alias = null, string? jobTitle = null, 
        DateTime? dateOfBirth = null, string? nationality = null, string? naturality = null, 
        string? email = null, string? notes = null, int? enterpriseId = null, 
        MaritalStatus maritalStatus = MaritalStatus.None)
    {
        _picture = picture;
        _alias = alias;
        _jobTitle = jobTitle;
        _dateOfBirth = dateOfBirth;
        _nationality = nationality;
        _naturality = naturality;
        _email = email;
        _notes = notes;
        _enterpriseId = enterpriseId;
        _maritalStatus = maritalStatus;
        return this;
    }
    
    public PersonBuilder WithStatus(Status status)
    {
        _status = status;
        return this;
    }
    
    public PersonBuilder WithExistingDocuments(IReadOnlyCollection<PersonDocument> documents)
    {
        if (documents != null) _documents.AddRange(documents);
        return this;
    }
    
    public PersonBuilder WithExistingId(Guid id)
    {
        _uniqueId = id;
        return this;
    }

    // --- Método de Construção ---
    public Person Build()
    {
        // Validação final de integridade antes de chamar o construtor protegido
        if (_name == string.Empty) 
        {
            throw new InvalidOperationException("O nome da Pessoa deve ser definido antes de construir.");
        }
        
        // Validação do estado derivado: Se DateOfBirth está presente, a idade deve ser consistente,
        // mas como removemos o Age do estado, confiamos que o Builder só passa o DoB, e o Person calcula.
        // Não há mais checagem de Age vs DoB necessária aqui.

        var idToUse = _uniqueId == Guid.Empty ? Guid.NewGuid() : _uniqueId;

        return new Person(
            idToUse,
            _name,
            _gender,
            _status,
            _maritalStatus,
            _picture,
            _alias,
            _jobTitle,
            _dateOfBirth,
            _nationality,
            _naturality,
            _email,
            _notes,
            _enterpriseId,
            _documents
        );
    }
}