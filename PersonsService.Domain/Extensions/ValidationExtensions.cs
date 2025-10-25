namespace PersonsService.Domain.Extensions;

/// <summary>
/// Extensões para validação de parâmetros.
/// </summary>
public static class ValidationExtensions
{

    #region Validações de Strings
    /// <summary>
    /// Lança uma ArgumentNullException se a string for nula ou vazia.
    /// </summary>
    /// <param name="value">valor da string</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentNullException">lança uma exceção se a string for nula ou vazia</exception>
    public static void ThrowIfNullOrWhiteSpace(this string? value, string paramName, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser nulo ou vazio.");
        }
    }

    /// <summary>
    /// Lança uma ArgumentNullException se o objeto for nulo.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto</typeparam>
    /// <param name="value">valor do objeto</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentNullException">lança uma exceção se o objeto for nulo</exception>
    public static void ThrowIfNull<T>(this T? value, string paramName, string? message = null) where T : class
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser nulo.");
        }
    }

    /// <summary>
    /// Lança uma ArgumentNullException se o valor nulo de um tipo valor for nulo.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto</typeparam>
    /// <param name="value">valor do objeto</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentNullException">lança uma exceção se o valor nulo de um tipo valor for nulo</exception>
    public static void ThrowIfNull<T>(this T? value, string paramName, string? message = null) where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser nulo.");
        }
    }
    #endregion

    #region Validações de Números
    /// <summary>
    /// Lança uma ArgumentOutOfRangeException se o valor for menor ou igual a zero.
    /// </summary>
    /// <param name="value">valor do objeto</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentOutOfRangeException">lança uma exceção se o valor for menor ou igual a zero</exception>
    public static void ThrowIfZeroOrNegative(this int value, string paramName, string? message = null)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, message ?? $"O parâmetro '{paramName}' deve ser maior que zero.");
        }
    }

    /// <summary>
    /// Lança uma ArgumentOutOfRangeException se o valor não for um ano válido (entre 1900 e o ano atual).
    /// </summary>
    /// <param name="value">valor do objeto</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentOutOfRangeException">lança uma exceção se o valor não for um ano válido (entre 1900 e o ano atual)</exception>
    public static void ThrowIfValidYear(this int value, string paramName, string? message = null)
    {
        var currentYear = DateTime.UtcNow.Year;
        if (value < 1900 || value > currentYear)
        {
            throw new ArgumentOutOfRangeException(paramName, message ?? $"O parâmetro '{paramName}' deve estar entre 1900 e {currentYear}.");
        }
    }

    /// <summary>
    /// Lança uma ArgumentOutOfRangeException se o valor for negativo.
    /// </summary>
    /// <param name="value">valor do objeto</param>
    /// <param name="paramName">parametro a ser validado</param>
    /// <param name="message">mensagem de erro personalizada</param>
    /// <exception cref="ArgumentOutOfRangeException">lança uma exceção se o valor for negativo</exception>
    public static void ThrowIfNegative(this decimal value, string paramName, string? message = null)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, message ?? $"O parâmetro '{paramName}' não pode ser negativo.");
        }
    }
    #endregion
}

