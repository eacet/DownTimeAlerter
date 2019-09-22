using System;
using System.Threading.Tasks;

namespace Core.Domain.Room {
    class RoomDomainService
    {
        private IRoomRepository _roomRepository;

        public async Task Add(string code) {

            var codeExist = await _roomRepository.CodeExist(code);

            if (codeExist)
                throw new Exception("Code already exist");

            _roomRepository.Add();
        }
    }
}
 