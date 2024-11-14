﻿
using OnlineStory.Contract.Share.Errors;

namespace OnlineStory.Contract.Abstractions;


public interface IResult<out TValue> : IResult
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    TValue Value { get; }
}

/// <summary>
/// Type-less interface for the <see cref="ErrorOr"/> object.
/// </summary>
/// <remarks>
/// This interface is intended for use when the underlying type of the <see cref="ErrorOr"/> object is unknown.
/// </remarks>
public interface IResult
{
    /// <summary>
    /// Gets the list of errors.
    /// </summary>
    List<Error>? Errors { get; }

    /// <summary>
    /// Gets a value indicating whether the state is error.
    /// </summary>
    bool IsError { get; }
}
