namespace Mizore.util
{
    //TODO-LOW: compare .NET's Activator.CreateInstance to expression, or even ILEmit. (especially in .NET 4.5)
    ///// <summary>
    ///// Creates a new instance of document type <typeparamref name="T"/> using <see cref="Expression"/>, faster than SolrDocumentActivator but <typeparamref name="T"/> requires a public parameterless constructor.
    ///// In my case it took ~5-10 ms less for 65k Objects.
    ///// </summary>
    ///// <typeparam name="T">document type</typeparam>
    ///// <seealso cref="http://www.smelser.net/blog/post/2010/03/05/When-Activator-is-just-to-slow.aspx"/>
    ///// <seealso cref="http://stackoverflow.com/questions/367577/why-does-the-c-sharp-compiler-emit-activator-createinstance-when-calling-new-in"/>
    //Additional urls: http://rogeralsing.com/2008/02/28/linq-expressions-creating-objects/ http://ayende.com/blog/3167/creating-objects-perf-implications
    //public class ClassActivator<T>
    //{
    //    //private static readonly Expression<Func<T>> ConstructorExpression = () => new T();
    //    //private static readonly Func<T> Constructor = ConstructorExpression.Compile();

    //    //public T Create()
    //    //{
    //    //    return Constructor();
    //    //}
    //}
}