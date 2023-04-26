using System.Security.Cryptography;

byte[] key = ...; // klíč
byte[] iv = ...; // vektor
byte[] data = ...; // vstupní data - text
byte[] encrypted; // výstupní data
​
using (Aes aesAlg = Aes.Create())
{
    aesAlg.Key = key;
    aesAlg.IV = iv;
    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
    using (MemoryStream msEncrypt = new MemoryStream())
    {
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using (BinaryWriter swEncrypt = new BinaryWriter(csEncrypt))
            {
                swEncrypt.Write(original);
            }
            encrypted = msEncrypt.ToArray();
        }
    }
}
​
foreach (var x in encrypted)
{
    Console.Write(String.Format("{0:x2} ", x));
}