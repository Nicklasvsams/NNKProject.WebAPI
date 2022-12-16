using Microsoft.EntityFrameworkCore;
using NNKProject.DAL.Database;
using NNKProject.DAL.Database.Entities;

namespace NNKProject.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity> SelectAccountByNameAsync(string accountName);
        Task<AccountEntity> InsertAccountAsync(AccountEntity account);
        Task<AccountEntity> UpdateAccountByNameAsync(AccountEntity account, string accountName);
        Task<AccountEntity> DeleteAccountSaveDataByNameAsync(string accountName);

    }

    public class AccountRepository : IAccountRepository
    {
        private readonly GameDBContext _dBContext;

        public AccountRepository(GameDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<AccountEntity> DeleteAccountSaveDataByNameAsync(string accountName)
        {
            AccountEntity accountEntity = await _dBContext.Accounts.FirstOrDefaultAsync(x => x.Name == accountName);

            if (accountEntity == null) return null;

            accountEntity.SaveData = "DELETEDVALUE"; // TODO: Figure out what a deleted savedata value would be

            await _dBContext.SaveChangesAsync();

            return accountEntity;
        }

        public async Task<AccountEntity> InsertAccountAsync(AccountEntity account)
        {
            await _dBContext.Accounts.AddAsync(account);
            await _dBContext.SaveChangesAsync();

            return account;
        }

        public async Task<AccountEntity> SelectAccountByNameAsync(string accountName)
        {
            return await _dBContext.Accounts.FirstOrDefaultAsync(x => x.Name == accountName);
        }

        public async Task<AccountEntity> UpdateAccountByNameAsync(AccountEntity account, string accountName)
        {
            AccountEntity accountEntity = await _dBContext.Accounts.FirstOrDefaultAsync(x => x.Name == accountName);

            if (accountEntity == null) return null;

            accountEntity.Name = account.Name;
            accountEntity.Password = account.Password;
            accountEntity.SaveData = account.SaveData;

            await _dBContext.SaveChangesAsync();

            return accountEntity;
        }
    }
}
