using System;
using System.IO;
using System.Security.Cryptography;

public partial class Program
{
    static void Main(string[] args)
    {
        Console.Write("Do you want to encrypt or decrypt? (E/D): ");
        string choice = Console.ReadLine();

        if (choice.ToLower() == "e")
        {
            Console.Write("Enter the text to encrypt: ");
            string text = Console.ReadLine();

            byte[] key, iv;
            using (Aes myAes = Aes.Create())
            {
                key = myAes.Key;
                iv = myAes.IV;
            }

            File.WriteAllText("key.txt", Convert.ToBase64String(key));
            File.WriteAllText("iv.txt", Convert.ToBase64String(iv));

            Console.WriteLine("Key and initialization vector have been saved to key.txt and iv.txt");

            byte[] encrypted = EncryptStringToBytes_Aes(text, key, iv);

            Console.WriteLine("Encrypted text: " + Convert.ToBase64String(encrypted));
            Console.WriteLine("Key: " + Convert.ToBase64String(key));
            Console.WriteLine("IV: " + Convert.ToBase64String(iv));
        }
        else if (choice.ToLower() == "d")
        {
            Console.Write("Enter the text to decrypt (in base64 format): ");
            string base64Text = Console.ReadLine();

            Console.Write("Enter the key: ");
            string keyString = Console.ReadLine();

            Console.Write("Enter the initialization vector: ");
            string ivString = Console.ReadLine();

            byte[] encrypted = Convert.FromBase64String(base64Text);
            byte[] key = Convert.FromBase64String(keyString);
            byte[] iv = Convert.FromBase64String(ivString);

            string decrypted = DecryptStringFromBytes_Aes(encrypted, key, iv);

            Console.WriteLine("Decrypted text: " + decrypted);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter 'E' or 'D'.");
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

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string plaintext = null;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}

