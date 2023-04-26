using System.Security.Cryptography;

byte[] key = ...; // klíč
byte[] iv = ...; // vektor
byte[] encrypted = ...; // vstupní data
byte[] received; // výstupní data - text
​
using (Aes aesAlg = Aes.Create())
{
    aesAlg.Key = key;
    aesAlg.IV = iv;
    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
    using (MemoryStream msDecrypt = new MemoryStream(encrypted))
    {
        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        {
            using (BinaryReader srDecrypt = new BinaryReader(csDecrypt))
            {
                received = srDecrypt.ReadBytes((int)msDecrypt.Length);
            }
        }
    }
}
​
foreach (var x in data)
{
    Console.Write(String.Format("{0:x2} ", x));
}