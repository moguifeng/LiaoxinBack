namespace Liaoxin.IBusiness
{
    public interface IValidateCodeService
    {
        byte[] CreateCode();

        bool IsSameCode(string code);
    }
}