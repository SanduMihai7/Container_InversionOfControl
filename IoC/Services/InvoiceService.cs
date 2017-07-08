using System;
using IoC.Interfaces;

namespace IoC.Services
{
    public class InvoiceService<T>
    {
        public InvoiceService(IRepository<T> repository, ILogger logger)
        {
        }
    }
}
