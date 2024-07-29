using System;
using System.Text;

public static class EncryptionUtility
{
    private static readonly string key = "your-secret-key";

    public static string Encrypt(string data)
    {
        StringBuilder sb = new StringBuilder(data);
        for (int i = 0; i < sb.Length; i++)
        {
            sb[i] = (char)(sb[i] ^ key[i % key.Length]);
        }
        return sb.ToString();
    }

    public static string Decrypt(string data)
    {
        return Encrypt(data); // XOR encryption is symmetrical
    }
}