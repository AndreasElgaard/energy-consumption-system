using ModelContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.AccountCommandHandler.Services
{
    public interface IAccountingService
    {
        Task<HttpStatusCode> UpdateAccount(AccountingCommand command);
    }
}
