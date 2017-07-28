    using System;
using System.Collections.Generic;
using System.Text;

namespace Fd.CQRS
{
    public interface IStatementHandler<in TStatement>
    {
        void Handle(TStatement statement);
    }

    public interface IStatementHandler<in TStatement,out TResult> where TStatement : IStatement<TResult>
    {
        TResult Handle(TStatement statement);
    }
}
