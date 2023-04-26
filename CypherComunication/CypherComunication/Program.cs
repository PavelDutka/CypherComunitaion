using System;
using System.IO;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Zadejte text k zašifrování: ");
        string text = Console.ReadLine();

        byte[] key, iv;
        using (Aes myAes = Aes.Create())
        {
            key = myAes.Key;
            iv = myAes.IV;
        }

        File.WriteAllText("key.txt", Convert.ToBase64String(key));
        File.WriteAllText("iv.txt", Convert.ToBase64String(iv));

        Console.WriteLine("Klíč a inicializační vektor byly uloženy do souborů key.txt a iv.txt");

        byte[] encrypted = EncryptStringToBytes_Aes(text, key, iv);

        Console.Write("Zašifrovaný text: ");
        foreach (var x in encrypted)
        {
            Console.Write(String.Format("{0:x2} ", x));
        }
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        return encrypted;
    }
}