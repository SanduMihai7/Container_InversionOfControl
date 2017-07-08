using System;
using IoC.Interfaces;

namespace IoC.Services
{
    public class SqlRepository<T> : IRepository<T>
    {
        public SqlRepository(ILogger logger)
        {

        }
    }
}
