using System.Runtime.Intrinsics.Arm;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.SeedWork;

namespace FC.CodeFlix.Catalog.Domain.Entity;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public Category(string name, string description, bool isActive = true) : base()
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
        IsActive = isActive;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        // if (string.IsNullOrWhiteSpace(Name))
        // throw new EntityValidationException($"{nameof(Name)} should not be empty or null");

        DomainValidation.MinLength(Name, 3, nameof(Name));
        // if (Name.Length < 3)
        // throw new EntityValidationException($"{nameof(Name)} should be at least 3 characters long");

        DomainValidation.MaxLength(Name, 255, nameof(Name));
        // if (Name.Length > 255)
        // throw new EntityValidationException($"{nameof(Name)} should be at most 255 characters long");

        DomainValidation.NotNull(Description, nameof(Description));
        // if (string.IsNullOrWhiteSpace(Description))
        // throw new EntityValidationException($"{nameof(Description)} should not be null");

        DomainValidation.MaxLength(Description, 10_000, nameof(Description));
        // if (Description.Length > 10_000)
        // throw new EntityValidationException($"{nameof(Description)} should be less or equal to 10.000 characters long");
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }
}