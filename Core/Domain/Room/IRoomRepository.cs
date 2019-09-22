using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Room
{
    interface IRoomRepository
    {
        Task<bool> CodeExist(string code);

        void Add();  

    }
}
