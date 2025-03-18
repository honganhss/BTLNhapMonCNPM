
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BTLNhapMonCNPM.BTLNhapMonCNPM.Tests
{
    internal class TestSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;
        public string Id => Guid.NewGuid().ToString();

        public IEnumerable<string> Keys => throw new NotImplementedException();

        public void Clear() => _sessionStorage.Clear();
        public void Remove(string key) => _sessionStorage.Remove(key);

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

        public void SetString(string key, string value) => Set(key, Encoding.UTF8.GetBytes(value));

        public string GetString(string key) => TryGetValue(key, out var data) ? Encoding.UTF8.GetString(data) : null;

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}