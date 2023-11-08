using System.Security.Cryptography;
using System.Text;

namespace ari_ib_calificaciones_api_application.DomainServices;

public class Hash
{
    public static string CreateHashString(string TipoNumFDesd)
    {
        if (string.IsNullOrEmpty(TipoNumFDesd)) throw new ArgumentNullException(nameof(TipoNumFDesd));
        var hash = " ";
        try
        {
            var d = Encoding.UTF8.GetBytes(TipoNumFDesd);

            using (SHA512 a = new SHA512Managed())
            {
                var h = a.ComputeHash(d);
                hash = BitConverter.ToString(h).Replace("-", "");
            }

            hash = hash.ToLowerInvariant();
        }
        catch
        {
        }

        return hash;
    }
}