﻿namespace System.Data.Entity.Internal.Linq
{
    using System.Collections;
    using System.Data.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     A non-generic interface implemented by <see cref = "InternalQuery{TElement}" /> that allows operations on
    ///     any query object without knowing the type to which it applies.
    /// </summary>
    internal interface IInternalQuery
    {
        void ResetQuery();
        InternalContext InternalContext { get; }
        ObjectQuery ObjectQuery { get; }

        Type ElementType { get; }
        Expression Expression { get; }
        IQueryProvider ObjectQueryProvider { get; }
        IEnumerator GetEnumerator();
    }
}