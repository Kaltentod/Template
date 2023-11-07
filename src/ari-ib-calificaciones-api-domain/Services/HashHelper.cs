using System.Security.Cryptography;
using System.Text;

namespace ari_ib_calificaciones_api_domain.Services;

public class HashHelper
{
    public static string EncryptString(string TipoNumFDesd)
    {
        if (string.IsNullOrEmpty(TipoNumFDesd)) throw new ArgumentNullException(nameof(TipoNumFDesd));
        var encrypted = " ";
        try
        {
            var d = Encoding.UTF8.GetBytes(TipoNumFDesd);

            using (SHA512 a = new SHA512Managed())
            {
                var h = a.ComputeHash(d);
                encrypted = BitConverter.ToString(h).Replace("-", "");
            }

            encrypted = encrypted.ToLowerInvariant();
        }
        catch
        {
        }

        return encrypted;
    }
}