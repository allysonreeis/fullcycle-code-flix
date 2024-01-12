using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.Domain.SeedWork;

public class DomainValidation
{
    public static void NotNull(object? value, string filedName)
    {
        if (value == null)
            throw new EntityValidationException($"{filedName} should not be null");
    }

    public static void NotNullOrEmpty(string? value, string filedName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EntityValidationException($"{filedName} should not be empty or null");
    }

    public static void MinLength(string? value, int minLength, string filedName)
    {
        if (value?.Length < minLength)
            throw new EntityValidationException($"{filedName} should be at least {minLength} characters long");
    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
            throw new EntityValidationException($"{fieldName} should be less or equal to {maxLength} characters long");
    }
}