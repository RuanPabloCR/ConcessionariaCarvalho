namespace Application.Services.PasswordEncryption
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private readonly int _workFactor;
        public PasswordEncryptionService(int workFactor = 12)
        {
            _workFactor = workFactor;
        }
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be whitespace", nameof(password));
            }
            return BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
