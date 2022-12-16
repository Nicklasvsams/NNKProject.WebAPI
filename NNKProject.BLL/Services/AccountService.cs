using NNKProject.BLL.DTO;
using NNKProject.DAL.Database.Entities;
using NNKProject.DAL.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NNKProject.BLL.Services
{
    public interface IAccountService
    {
        Task<AccountResponse> AuthorizeAccountAsync(string accountName, string password);
        Task<AccountResponse> GetAccountByNameAsync(string accountName);
        Task<AccountResponse> CreateAccountAsync(AccountRequest account);
        Task<AccountResponse> UpdateAccountByNameAsync(AccountRequest account, string accountName);
        Task<AccountResponse> DeleteAccountSaveDataByNameAsync(string accountName);
    }

    public class AccountService : IAccountService
    {
        IAccountRepository _accRepository;

        public AccountService(IAccountRepository accRepository)
        {
            _accRepository = accRepository;
        }

        public async Task<AccountResponse> AuthorizeAccountAsync(string accountName, string password)
        {
            AccountEntity account = await _accRepository.SelectAccountByNameAsync(accountName);

            if (account == null) return null;

            AccountResponse accRes = MapEntityToResponse(account);

            if (account.Name == accountName && account.Password == password) accRes.IsAuthorized = true; 
            else accRes.IsAuthorized = false;

            return accRes;
        }

        public async Task<AccountResponse> CreateAccountAsync(AccountRequest account)
        {
            return MapEntityToResponse(await _accRepository.InsertAccountAsync(MapRequestToEntity(account)));
        }

        public async Task<AccountResponse> DeleteAccountSaveDataByNameAsync(string accountName)
        {
            return MapEntityToResponse(await _accRepository.DeleteAccountSaveDataByNameAsync(accountName));
        }

        public async Task<AccountResponse> GetAccountByNameAsync(string accountName)
        {
            return MapEntityToResponse(await _accRepository.SelectAccountByNameAsync(accountName));
        }

        public async Task<AccountResponse> UpdateAccountByNameAsync(AccountRequest account, string accountName)
        {
            return MapEntityToResponse(await _accRepository.UpdateAccountByNameAsync(MapRequestToEntity(account), accountName));
        }

        private AccountResponse MapEntityToResponse(AccountEntity account)
        {
            if (account == null) return null;

            AccountResponse accRes = new AccountResponse() 
            {
                Id = account.Id,
                Name = account.Name,
                SaveData = account.SaveData
            };

            return accRes;
        }

        private AccountEntity MapRequestToEntity(AccountRequest request)
        {
            AccountEntity account = new AccountEntity()
            {
                Name = request.Name,
                Password = request.Password,
                SaveData = request.SaveData
            };

            return account;
        }
    }
}
