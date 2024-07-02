using System.Security.Cryptography;
using System.Text;

namespace DataProtectionOnServer.Security
{
    public class DataProtector
    {

        /*
* Kritik veriyi şifreleyecek (Encryption)
* Bu kritik veriyi bir dosyaya kaydedecek
* Ardından ihtiyaç duyduğunda bu kaydı okuyup
* Decrypt edecek
* 
* Encode: Bir veriyi A noktasından B'ye transfer etmek işin kullanılan bir standart.
* 
* 
*/

        private readonly string path;
        private readonly byte[] entropy;

        public DataProtector(string path)
        {
            this.path = path;
            entropy = RandomNumberGenerator.GetBytes(16);
        }

        public int EncryptData(string value)
        {
            var encodedValue = Encoding.UTF8.GetBytes(value);

            using var fStream = new FileStream(path, FileMode.OpenOrCreate);
           
            var length = encryptDataToFile(encodedValue,entropy, DataProtectionScope.CurrentUser, fStream);

            return length;

        }

        private int encryptDataToFile(byte[] encodedBytes, byte[] entropy, DataProtectionScope scope,FileStream fileStream)
        {
            var encrypted = ProtectedData.Protect(encodedBytes, entropy, scope);
            if (fileStream.CanWrite && encrypted != null)
            {
                fileStream.Write(encrypted, 0, encrypted.Length);
            }
            return encrypted.Length;
        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decrypted = decryptFromFile(length, entropy,  DataProtectionScope.CurrentUser, fileStream);
            return Encoding.UTF8.GetString(decrypted);

        }

        private byte[] decryptFromFile(int length, byte[] entropy, DataProtectionScope currentUser, FileStream fileStream)
        {
            var input = new byte[length];
            var output = new byte[length];
            if (fileStream.CanRead)
            {
                fileStream.Read(input, 0, input.Length);
                output = ProtectedData.Unprotect(input, entropy, currentUser);

            }
            return output;

        }
    }
}
