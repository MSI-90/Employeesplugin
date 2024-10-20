using EmployeesMyPlugin.Entities.JsonRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesMyPlugin.Infrastructure.ExternalApiQuery
{
    public interface IExternalApiQuery
    {
        Task<IEnumerable<User>> GetUsers(params int[] userNumbers);
    }
}
