namespace PozitronDev.SharedKernel.Contracts;

// Overriding Equals() and GetHashCode() for entities is not the best approach.
// This would be OK only if we intend this equality to be applicable everywhere and in any circumstances.
// That's not the case with entities. We need value comparison only for specific use cases in our business logic.
// Otherwise, we don't want to consider two entities to be equal if they have different Id. This can have implications in EntityFramework, Dictionaries, etc.
// Therefore, we'll introduce this interface and implement it wherever is needed.
public interface IValueEquatable<in T> where T : class
{
    bool EqualsByValue(T other);
}
