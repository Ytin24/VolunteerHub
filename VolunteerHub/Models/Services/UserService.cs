using VolunteerHub.Db;

namespace VolunteerHub.Models.Services {
    public class UserService {
        private readonly VolunteerDbContext db = new VolunteerDbContext();

        public static User StaticMe;
        public User Me { get; set; }

        public System.Threading.Tasks.Task SetUpUser(User user) {
            Me = user;
            StaticMe = user;
            return System.Threading.Tasks.Task.CompletedTask;
        }
        public User GetUser() {
            return Me;
        }
        public static User SGetUser() {
            return StaticMe;
        }

    }
}
