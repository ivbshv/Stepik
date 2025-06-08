using Stepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Services
{
    public interface ICommentsService
    {
        /// <summary>
        /// Получение всех комментариев к курсу
        /// </summary>
        /// <param name="id">id курса</param>
        /// <returns>Список комментариев</returns>
        List<Comment> Get(int id);

        /// <summary>
        /// Удаление комментария пользователя
        /// </summary>
        /// <param name="id">id комментария</param>
        /// <returns>Удалось ли удалить комментарий</returns>
        bool Delete(int id);
    }
}
