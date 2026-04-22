namespace AccountingScholarships.Application.Common;

/// <summary>
/// Результат сервисной операции. Передаёт данные либо ошибку — без зависимости от HTTP-типов.
/// </summary>
public class AuthResult<T>
{
    public T? Data { get; private init; }
    public string? ErrorMessage { get; private init; }
    public bool IsSuccess => ErrorMessage is null;
    public bool IsNotFound { get; private init; }

    public static AuthResult<T> Success(T data) =>
        new() { Data = data };

    public static AuthResult<T> Unauthorized(string message) =>
        new() { ErrorMessage = message };

    public static AuthResult<T> NotFound(string message) =>
        new() { ErrorMessage = message, IsNotFound = true };
}
