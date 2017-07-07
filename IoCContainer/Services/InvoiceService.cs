using System;
using IoCContainer.Interfaces;

namespace IoCContainer.Services
{
    public class InvoiceService
    {
        public InvoiceService(IRepository<Employee> repository, ILogger logger)
        {
        }
    }
}
