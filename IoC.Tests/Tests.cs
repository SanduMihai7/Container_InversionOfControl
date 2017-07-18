using System;
using IoC.Interfaces;
using IoC.Services;
using NUnit.Framework;

namespace IoC.Tests
{
    public class Tests
    {
		[Test]
		public void Can_Resolve_Types()
		{
			var ioc = new Container();
			ioc.For<ILogger>().Use<SqlServerLogger>();

			var logger = ioc.Resolve<ILogger>();

			Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
		}

		[Test]
		public void Can_Resolve_Types_Without_Default_Constructors()
		{
			var ioc = new Container();
			ioc.For<ILogger>().Use<SqlServerLogger>();
			ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

			var repository = ioc.Resolve<IRepository<Employee>>();

			Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
		}

		[Test]
		public void Can_Resolve_Concrete_Types()
		{
			var ioc = new Container();
			ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));

            var service = ioc.Resolve<InvoiceService<Employee>>();
            Assert.IsNotNull(service);

		}

		public class Employee
		{
			public string Name
			{
				get;
				set;
			}
		}
    }
}
