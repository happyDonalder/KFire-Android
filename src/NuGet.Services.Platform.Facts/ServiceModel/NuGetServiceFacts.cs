﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Moq;
using NuGet.Services.TestInfrastructure;
using Xunit;

namespace NuGet.Services.ServiceModel
{
    public class NuGetServiceFacts
    {
        private static readonly ServiceName TestName = new ServiceName(
                    new ServiceHostInstanceName(
                        new ServiceHostName(
                            new DatacenterName(
                                new EnvironmentName(
                                    "nuget",
                                    "local"),
                                0),
                            "testhost"),
                        0),
                    "test");

        public class TheStartMethod
        {
            public async Task ItInjectsPropertiesWithPublicSetters()
            {
                // Arrange
                var host = new TestServiceHost();
                var container = CreateTestContainer();
                var service = new TestServiceWithComponents(TestName, host);

                // Act
                await service.Start(container);

                // Assert
                Assert.Same(service.Something, container.Resolve<SomeComponent>());
                Assert.Null(service.SomethingElse);
            }

            public async Task ItInvokesOnStart()
            {
                // Arrange
                var host = new TestServiceHost();
                var container = CreateTestContainer();
                var service = new TestService(TestName, host);

                // Act
                await service.Start(container);

                // Assert
                Assert.True(service.WasStarted);
            }

            public async Task ItConfiguresShutdownTokenToCallOnShutdown()
            {
                // Arrange
                var host = new TestServiceHost();
                var container = CreateTestContainer();
                var service = new TestService(TestName, host);
                await service.Start(container);

                // Act
                host.Shutdown();

                // Assert
                Assert.True(service.WasShutdown);
            }
        }

        public class TheRunMethod
        {
            public async Task ItInvokesOnRun()
            {
                // Arrange
                var host = new TestServiceHost();
                var container = CreateTestContainer();
                var service = new TestService(TestName, host);
                await service.Start(container);

                // Act
                await service.Run();

                // Assert
                Assert.True(service.WasRun);
            }
        }

        // Helper methods
        private static IContainer CreateTestContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SomeComponent>().SingleInstance();
            builder.RegisterType<SomeOtherComponent>().SingleInstance();
            var container = builder.Build();
            return container;
        }

        // Helper nested classes
        public class TestServiceWithComponents : TestService
        {
            public SomeComponent Something { get; set; }
            public SomeOtherComponent SomethingElse { get; set; }

            public TestServiceWithComponents(ServiceName name, ServiceHost host) : base(name, host) { }
        }

        public class SomeComponent
        {
        }

        public class SomeOtherComponent
        {
        }
    }
}
