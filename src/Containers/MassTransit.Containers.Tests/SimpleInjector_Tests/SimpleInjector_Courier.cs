// Copyright 2007-2019 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
namespace MassTransit.Containers.Tests.SimpleInjector_Tests
{
    using Common_Tests;
    using NUnit.Framework;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using TestFramework.Courier;


    [TestFixture]
    public class SimpleInjectorCourier_ExecuteActivity :
        Courier_ExecuteActivity
    {
        readonly Container _container;

        public SimpleInjectorCourier_ExecuteActivity()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.AddMassTransit(cfg =>
            {
                cfg.AddExecuteActivity<SetVariableActivity, SetVariableArguments>();
                cfg.AddBus(() => BusControl);
            });
        }

        [Test]
        public void Should_be_a_valid_container()
        {
            _container.Verify();
        }
        protected override IRegistration Registration => _container.GetInstance<IRegistration>();
    }


    [TestFixture]
    public class SimpleInjectorCourier_ExecuteActivity_Endpoint :
        Courier_ExecuteActivity_Endpoint
    {
        readonly Container _container;

        public SimpleInjectorCourier_ExecuteActivity_Endpoint()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.AddMassTransit(cfg =>
            {
                cfg.AddExecuteActivity<SetVariableActivity, SetVariableArguments>()
                    .Endpoint(e => e.Name = "custom-setvariable-execute");

                cfg.AddBus(() => BusControl);
            });
        }

        [Test]
        public void Should_be_a_valid_container()
        {
            _container.Verify();
        }

        protected override IRegistration Registration => _container.GetInstance<IRegistration>();
    }


    [TestFixture]
    public class SimpleInjectorCourier_Activity :
        Courier_Activity
    {
        readonly Container _container;

        public SimpleInjectorCourier_Activity()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.AddMassTransit(cfg =>
            {
                cfg.AddActivity<TestActivity, TestArguments, TestLog>();
                cfg.AddBus(() => BusControl);
            });
        }

        [Test]
        public void Should_be_a_valid_container()
        {
            _container.Verify();
        }
        protected override IRegistration Registration => _container.GetInstance<IRegistration>();
    }


    [TestFixture]
    public class SimpleInjectorCourier_Activity_Endpoint :
        Courier_Activity_Endpoint
    {
        readonly Container _container;

        public SimpleInjectorCourier_Activity_Endpoint()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _container.AddMassTransit(cfg =>
            {
                cfg.AddActivity<TestActivity, TestArguments, TestLog>()
                    .Endpoints(e => e.Name = "custom-testactivity-execute", e => e.Name = "custom-testactivity-compensate");

                cfg.AddBus(() => BusControl);
            });
        }

        [Test]
        public void Should_be_a_valid_container()
        {
            _container.Verify();
        }
        protected override IRegistration Registration => _container.GetInstance<IRegistration>();
    }
}
