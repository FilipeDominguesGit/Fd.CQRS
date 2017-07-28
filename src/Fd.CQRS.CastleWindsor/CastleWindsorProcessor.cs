using System;
using System.Reflection;
using Castle.Windsor;
using System.Runtime;

namespace Fd.CQRS.CastleWindsor
{
    public class CastleWindsorProcessor : IStatementProcessor
    {
        private readonly IWindsorContainer _container;

        public CastleWindsorProcessor(IWindsorContainer container)
        {
            _container = container ?? throw  new ArgumentNullException(nameof(container), "Provided Castle Windsor container cannot be null.");
        }

        public TResult Process<TResult>(IStatement<TResult> query)
        {

            Type queryType = query.GetType();
            Type handlerType = typeof(IStatementHandler<,>).MakeGenericType(queryType, typeof(TResult));
            try
            {
                var handler = _container.Resolve(handlerType);
                MethodInfo methodInfo = handlerType.GetRuntimeMethod("Handle", new Type[] { queryType });
                return (TResult)methodInfo.Invoke(handler, new object[] { query });
            }
            catch (Exception exception)
            {
                throw new Exception($"The processor couldn't find a suitable handler for the provided {nameof(query)}", exception);
            }
        }

        public void Process(IStatement command)
        {
            Type commandType = command.GetType();
            Type handlerType = typeof(IStatementHandler<>).MakeGenericType(commandType);
            try
            {
                var handler = _container.Resolve(handlerType);
                MethodInfo methodInfo = handlerType.GetRuntimeMethod("Handle", new Type[] { commandType });
                methodInfo.Invoke(handler, new object[] { command });
            }
            catch (Exception exception)
            {
                throw new Exception($"The processor couldn't find a suitable handler for the provided {nameof(command)}", exception);
            }
        }
    }
}
