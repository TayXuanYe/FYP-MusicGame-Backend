public class Result<T>
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    private readonly T _value;

    public T Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("The value of a failed result cannot be accessed.");
            }
            return _value;
        }
    }

    protected internal Result(T value)
    {
        IsSuccess = true;
        _value = value;
        ErrorMessage = string.Empty;
    }

    protected internal Result(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        _value = default!;
    }
    
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(string errorMessage) => new Result<T>(errorMessage);
}