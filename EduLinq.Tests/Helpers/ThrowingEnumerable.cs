namespace EduLinq.Tests.Helpers;

public sealed class ThrowingEnumerable : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        throw new InvalidOperationException();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Check that the given function uses deferred execution.
    /// A "spiked" source is given to the function: the function
    /// call itself shouldn't throw an exception. However, using
    /// the result (by calling GetEnumerator() then MoveNext() on it) *should*
    /// throw InvalidOperationException.
    /// </summary>
    public static void AssertDeferred<T>(
        Func<IEnumerable<int>, IEnumerable<T>> deferredFunction)
    {
        ThrowingEnumerable source = new ThrowingEnumerable();
        IEnumerable<T> result = deferredFunction(source);
        using (IEnumerator<T> iterator = result.GetEnumerator())
        {
            Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
        }
    }
}