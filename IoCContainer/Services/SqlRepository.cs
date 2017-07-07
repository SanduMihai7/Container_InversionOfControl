using System;
using IoCContainer.Interfaces;

namespace IoCContainer
{
    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(ILogger logger)
        {

        }
    }
}
