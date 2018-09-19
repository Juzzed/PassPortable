namespace PassPortable.Models
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
