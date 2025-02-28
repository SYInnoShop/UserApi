namespace UserService.Application.Common;
public record ErrorOr<T>
{
    public T Value { get; }
    public List<string> Errors { get; }
    public bool IsError => Errors.Any();

    private ErrorOr(T value)
    {
        Value = value;
        Errors = new List<string>();
    }

    private ErrorOr(List<string> errors)
    {
        Errors = errors;
    }

    public static ErrorOr<T> Success(T value) => new(value);
    public static ErrorOr<T> Failure(List<string> errors) => new(errors);
}