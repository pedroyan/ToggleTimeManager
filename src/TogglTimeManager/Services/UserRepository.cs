using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Exception = System.Exception;

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
        Task Persist(UserInfo userInfo);
    }

    public class UserRepository : IUserRepository
    {
        private const string StorageFileName = "userinfo.txt";
        private UserInfo _currentUserInfo;

        /// <inheritdoc />
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

        private static async Task<UserInfo> GetFromFileStorage()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
            using (var stream = new IsolatedStorageFileStream(StorageFileName, FileMode.Open, storage))
            {
                using (var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<UserInfo>(await reader.ReadToEndAsync());
                }
            }
        }

        /// <inheritdoc />
        public async Task Persist(UserInfo userInfo)
        {
            try
            {
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
                using (var stream = new IsolatedStorageFileStream(StorageFileName, FileMode.Create, storage))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        string toWrite = JsonConvert.SerializeObject(userInfo);
                        await writer.WriteAsync(toWrite);
                    }
                }
                _currentUserInfo = userInfo;
            }
            catch (Exception e)
            {
                //TODO: Add logging here.
                throw;
            }
        }
    }
}
