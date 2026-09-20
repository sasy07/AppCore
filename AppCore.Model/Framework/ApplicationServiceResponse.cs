namespace AppCore.Model.Framework;

public class ApplicationServiceResponse
{
    private List<string> _errors = new List<string>();
    private List<string> _messages = new List<string>();


    public void AddError(string error)
    {
        _errors.Add(error);
    }

    public void AddError(List<string> errors)
    {
        _errors.AddRange(errors);
    }

    public void AddMessage(string message)
    {
        _messages.Add(message);
    }

    public void AddMessage(List<string> messages)
    {
        _messages.AddRange(messages);
    }

    public List<string> Errors => _errors;
    public List<string> Messages => _messages;

    public bool IsSuccess => !IsFailure;
    public bool IsFailure => _errors.Any();
}


public class ApplicationServiceResponse<T> : ApplicationServiceResponse
    where T : class
{
    public T Result { get; set; }
}