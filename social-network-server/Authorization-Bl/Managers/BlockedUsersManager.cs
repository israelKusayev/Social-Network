using Authorization_Common.Interfaces.Managers;
using Authorization_Common.Interfaces.Repositories;
using Authorization_Common.Models;

namespace Authorization_Bl.Managers
{
    public class BlockedUsersManager : IBlockedUsersManager
    {
        private IDynamoDbRepository<BlockedUser> _blockedUsersRepository;

        public BlockedUsersManager(IDynamoDbRepository<BlockedUser> blockedUsersRepository)
        {
            _blockedUsersRepository = blockedUsersRepository;
        }

        public bool BlockUser(string userId)
        {
            var user =_blockedUsersRepository.Add(new BlockedUser(userId));
            return user != null;
        }

        public bool UnBlockUser(string userId)
        {
            return _blockedUsersRepository.Delete(userId);
        }
    }
}
