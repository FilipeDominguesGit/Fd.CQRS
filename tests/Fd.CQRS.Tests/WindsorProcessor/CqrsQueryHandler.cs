namespace Fd.CQRS.Tests.WindsorProcessor
{
    public class CqrsQueryHandler : IStatementHandler<CqrsQuery, object>
    {
        public object Handle(CqrsQuery statement)
        {
            return null;
        }
    }
}
