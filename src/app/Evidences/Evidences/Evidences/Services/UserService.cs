using System.Threading.Tasks;
using Evidences.Models;
using Evidences.Repositories;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Evidences.Services
{
    public class UserService : IUserService
    {
        private const string USER_KEY = "user";

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(string userName)
        {
            var user = await _userRepository.Add(userName);

            Application.Current.Properties[USER_KEY] = JsonConvert.SerializeObject(user);
            await Application.Current.SavePropertiesAsync();
        }

        public User Get()
        {
            if (Application.Current.Properties.ContainsKey(USER_KEY))
            {
                return JsonConvert.DeserializeObject<User>((string)Application.Current.Properties[USER_KEY]);
            }

            return null;
        }
    }
}