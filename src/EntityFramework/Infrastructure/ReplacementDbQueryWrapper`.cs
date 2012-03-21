namespace System.Data.Entity.Infrastructure
{
    using System.Data.Objects;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
    ///     Instances of this class are used internally to create constant expressions for <see cref = "ObjectQuery{T}" />
    ///     that are inserted into the expression tree to  replace references to <see cref = "DbQuery{TResult}" />
    ///     and <see cref = "DbQuery" />.
    /// </summary>
    /// <typeparam name = "TElement">The type of the element.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db")]
    public sealed class ReplacementDbQueryWrapper<TElement>
    {
        #region Fields and constructors

        private readonly ObjectQuery<TElement> _query;

        /// <summary>
        ///     Private constructor called by the Create factory method.
        /// </summary>
        /// <param name = "query">The query.</param>
        private ReplacementDbQueryWrapper(ObjectQuery<TElement> query)
        {
            _query = query;
        }

        /// <summary>
        ///     Factory method called by CreateDelegate to create an instance of this class.
        /// </summary>
        /// <param name = "query">The query, which must be a generic object of the expected type.</param>
        /// <returns>A new instance.</returns>
        internal static ReplacementDbQueryWrapper<TElement> Create(ObjectQuery query)
        {
            Contract.Assert(query is ObjectQuery<TElement>);

            return new ReplacementDbQueryWrapper<TElement>((ObjectQuery<TElement>)query);
        }

        #endregion

        #region Query property

        /// <summary>
        ///     The public property expected in the LINQ expression tree.
        /// </summary>
        /// <value>The query.</value>
        public ObjectQuery<TElement> Query
        {
            get { return _query; }
        }

        #endregion
    }
}