﻿
#pragma warning disable SA1649 // File name should match first type name
public readonly record struct Success;
#pragma warning restore SA1649 // File name should match first type name
public readonly record struct Created;
public readonly record struct Deleted;
public readonly record struct Updated;
namespace OnlineStory.Contract.Share
{
    public class ResultType
    {
        public static Success Success => default;

        public static Created Created => default;

        public static Deleted Deleted => default;

        public static Updated Updated => default;
    }
}
