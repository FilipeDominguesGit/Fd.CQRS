using System;
using System.Collections.Generic;
using System.Text;

namespace Fd.CQRS
{
    public interface IStatementProcessor
    {
        TResult Process<TResult>(IStatement<TResult> query);

        void Process(IStatement command);

    }
}
