using EduLinq.Tests.Helpers;

namespace EduLinq.Tests;

public class WhereTest
{
    [Fact]
    public void NullSourceThrowsNullArgumentException()
    {
        IEnumerable<int> source = null;
        Assert.Throws<ArgumentNullException>(() => source.Where(x => x > 5));
    }

    [Fact]
    public void NullPredicateThrowsNullArgumentException()
    {
        int[] source = [1, 3, 7, 9, 10];
        Func<int, bool> predicate = null!;
        Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
    }

    [Fact]
    public void WithIndexNullSourceThrowsNullArgumentException()
    {
        IEnumerable<int> source = null!;
        Assert.Throws<ArgumentNullException>(() => source.Where((x, index) => x > 5));
    }

    [Fact]
    public void WithIndexNullPredicateThrowsNullArgumentException()
    {
        int[] source = [1, 3, 7, 9, 10];
        Func<int, int, bool> predicate = null!;
        Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
    }

    [Fact]
    public void SimpleFiltering()
    {
        int[] source = [1, 3, 4, 2, 8, 1];
        var result = source.Where(x => x < 4);
        Assert.Equal([1, 3, 2, 1], result);
    }

    [Fact]
    public void SimpleFilteringWithQueryExpression()
    {
        int[] source = [1, 3, 4, 2, 8, 1];
        var result = from x in source
                     where x < 4
                     select x;
        Assert.Equal([1, 3, 2, 1], result);
    }

    [Fact]
    public void EmptySource()
    {
        int[] source = [];
        var result = source.Where(x => x < 4);
        Assert.Equal([], result);
    }

    [Fact]
    public void ExecutionIsDeferred()
    {
        ThrowingEnumerable.AssertDeferred(src => src.Where(x => x > 0));
    }

    [Fact]
    public void WithIndexSimpleFiltering()
    {
        int[] source = [1, 3, 4, 2, 8, 1];
        var result = source.Where((x, index) => x < index);
        Assert.Equal([2, 1], result);
    }

    [Fact]
    public void WithIndexEmptySource()
    {
        int[] source = [];
        var result = source.Where((x, index) => x < 4);
        Assert.Equal([], result);
    }

    [Fact]
    public void WithIndexExecutionIsDeferred()
    {
        ThrowingEnumerable.AssertDeferred(src => src.Where((x, index) => x > 0));
    }
}
