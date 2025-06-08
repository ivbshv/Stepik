using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Services
{
    public interface ICertificatesService
    {
        /// <summary>
        /// Получение сертификатов пользователя
        /// </summary>
        /// <param name="fullName">Полное имя пользователя</param>
        /// <returns>DataSet</returns>
        DataSet Get(string fullName);
    }
}
