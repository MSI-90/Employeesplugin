using EmployeesMyPlugin.Infrastructure.ExternalApiQuery;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EmployeesMyPlugin
{
    [Author(Name = "Sergey Miller")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IExternalApiQuery _apiQuery;
        public Plugin()
        {
            _apiQuery = new ExternalApiService();
        }
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Loading MyPlugin");

            //Отправляем запрос на получение 3 тестовых пользователей от стороннего web api
            var usersFromApi = _apiQuery.GetUsers(5, 7, 12).Result;

            var usersAsEmployees = new List<EmployeesDTO>();
            foreach (var user in usersFromApi)
            {
                var employee = new EmployeesDTO { Name = user.LastName };
                employee.AddPhone( user.Phone );
                usersAsEmployees.Add(employee);
            }

            return usersAsEmployees.Cast<DataTransferObject>();
        }
    }
}
