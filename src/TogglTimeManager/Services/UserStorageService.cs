using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TogglTimeManager.Services
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user info for the application. Can return null if no information is found
        /// </summary>
        /// <returns>The instance of the <see cref="UserInfo"/> or null if none is found</returns>
        Task <UserInfo> GetUserInfo();

        /// <summary>
        /// Updates the cache and the file with the provided <see cref="UserInfo"/>
        /// </summary>
        /// <param name="userInfo">Instance to be saved</param>
        /// <returns>A flag indicating if the persistence was successful</returns>
        Task <bool> Persist(UserInfo userInfo);
    }

    public class UserRepository : IUserRepository
    {
        private const string StorageFileName = "userinfo.txt";
        private UserInfo _currentUserInfo;

        public async Task<UserInfo> GetUserInfo()
        {
            if (_currentUserInfo != null)
            {
                return _currentUserInfo;
            }

            try
            {
                _currentUserInfo = await GetFromFileStorage();
            }
            catch (Exception e)
            {
                //todo: log this
                return null;
            }

            return _currentUserInfo;
        }

        private async Task<UserInfo> GetFromFileStorage()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(StorageFileName, FileMode.Open, storage))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<UserInfo>(await reader.ReadToEndAsync());
                }
            }
        }

        public async Task<bool> Persist(UserInfo userInfo)
        {
            try
            {
                var storage = IsolatedStorageFile.GetUserStoreForDomain();
                using (var stream = new IsolatedStorageFileStream(StorageFileName, FileMode.Create, storage))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        var toWrite = JsonConvert.SerializeObject(userInfo);
                        await writer.WriteAsync(toWrite);
                    }
                }

                _currentUserInfo = userInfo;
                return true;
            }
            catch (Exception e)
            {
                //TODO: Log the exception
                return false;
            }
        }
    }
}
