using System.Runtime.CompilerServices;

namespace Todo.Domain;

public class GuardAgainst
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid ArgumentBeingEmpty(Guid argumentValue,
                                          string? message = null,
                                          [CallerArgumentExpression("argumentValue")]
                                          string? argumentName = null)
    {
        return argumentValue == Guid.Empty
            ? throw new ArgumentException(message, argumentName)
            : argumentValue;
    }
}
