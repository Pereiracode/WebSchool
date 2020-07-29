using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Utils
{
    public class CriptoHash
    {
        public static string GerarHash(string str)
        {
            SHA256 sha256 = new SHA256Managed();

            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder resultado = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                resultado.Append(hash[i].ToString("x"));
            }

            return resultado.ToString();
        }
    }
}
