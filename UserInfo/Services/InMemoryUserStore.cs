using System.Collections.Concurrent;
using UserInfo.Models;

namespace UserInfo.Services
{
    public class InMemoryUserStore
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private int _nextId = 1;

        public InMemoryUserStore()
        {
            // seed with sample users
            Add(new User { FirstName = "Alice", LastName = "Smith", Email = "alice@example.com" });
            Add(new User { FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com" });
        }

        public IEnumerable<User> GetAll() => _users.Values.OrderBy(u => u.Id);

        public User? Get(int id) => _users.TryGetValue(id, out var u) ? u : null;

        public User Add(User user)
        {
            var id = Interlocked.Increment(ref _nextId);
            user.Id = id;
            _users[id] = user;
            return user;
        }

        public bool Update(int id, User user)
        {
            if (!_users.ContainsKey(id)) return false;
            user.Id = id;
            _users[id] = user;
            return true;
        }

        public bool Delete(int id) => _users.TryRemove(id, out _);
    }
}
