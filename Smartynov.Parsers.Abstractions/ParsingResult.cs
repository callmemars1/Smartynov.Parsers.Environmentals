﻿namespace Smartynov.Parsers.Core;

public class ParsingResult<TObject>
{
    private TObject? _value;

    public ParsingResult(TObject result)
    {
        Value = result;
        IsFaulted = false;
    }

    public ParsingResult(ParsingException exception)
    {
        Value = default;
        IsFaulted = true;
        Exception = exception;
    }

    public bool IsFaulted { get; }

    public TObject? Value
    {
        get => IsFaulted ? throw Exception! : _value;
        private set => _value = value;
    }

    public ParsingException? Exception { get; }

    public void StartWithResult(Action<TObject> action)
    {
        CheckAndThrowIfFaulted();
        action.Invoke(Value!);
    }

    public async Task StartWithResultAsync(Func<TObject, Task> action)
    {
        CheckAndThrowIfFaulted();
        await action.Invoke(Value!);
    }

    private void CheckAndThrowIfFaulted()
    {
        if (IsFaulted)
            throw new InvalidOperationException("Can't invoke action, because arguments parsing failed", Exception);
    }
}