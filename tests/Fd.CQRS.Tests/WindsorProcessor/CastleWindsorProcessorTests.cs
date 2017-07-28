using System;
using System.ComponentModel;
using Castle.Windsor;
using NUnit.Framework;
using System.Runtime;
using Fd.CQRS.CastleWindsor;
using Component = Castle.MicroKernel.Registration.Component;

namespace Fd.CQRS.Tests.WindsorProcessor
{
    [TestFixture]
    public class CastleWindsorProcessorTests
    {

        private static IWindsorContainer GetRegisteredContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IStatementHandler<CqrsQuery, object>>().ImplementedBy<CqrsQueryHandler>()
                .LifeStyle.Singleton);

            container.Register(Component.For<IStatementHandler<CqrsCommand>>().ImplementedBy<CqrsCommandHandler>()
                .LifeStyle.Singleton);

            return container;
        }

        private static IWindsorContainer GetContainerUnregistered()
        {
            var container = new WindsorContainer();
            return container;
        }

        [Test]
        public void When_container_is_null_Should_throw_error()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var processor = new CastleWindsorProcessor(null);
            });
        }

        [Test]
        public void When_container_was_incorrectly_installed_process_command_Should_throw_error()
        {
            var command = new CqrsCommand();
            var container = GetContainerUnregistered();
            var processor = new CastleWindsorProcessor(container);

            Assert.Throws<Exception>(() =>
            {
                processor.Process(command);
            });
        }

        [Test]
        public void When_container_was_incorrectly_installed_process_query_Should_throw_error()
        {
            var query = new CqrsQuery();
            var container = GetContainerUnregistered();
            var processor = new CastleWindsorProcessor(container);

            Assert.Throws<Exception>(() =>
            {
                var result = processor.Process(query);
            });
        }


        [Test]
        public void When_command_is_installed_Should_handle_command()
        {
            var container = GetRegisteredContainer();
            var processor = new CastleWindsorProcessor(container);
            var command = new CqrsCommand();

            processor.Process(command);

            Assert.Pass();
        }

        [Test]
        public void When_query_is_installed_Should_handle_command()
        {
            var container = GetRegisteredContainer();
            var processor = new CastleWindsorProcessor(container);
            var query = new CqrsQuery();

            var result = processor.Process(query);

           Assert.IsNull(result);
        }

    }
}
