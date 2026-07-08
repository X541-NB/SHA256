using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace SHA256Project
{
    class SHA256Algorithm
    {
        public static string ComputeSha256Hash(string filePath)
        {
            // Create an object from SHA265
            using (SHA256 sha256 = SHA256.Create())
            {
                // Open file as stream
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    // Cal the Hash(Arrays of bytes)
                    byte[] hashBytes = sha256.ComputeHash(fileStream);

                    // Convert the bytes of arrays to a string of normal hexdecimal
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        // 2 digits of hex( It means that if one side has number, use the left zero
                        // Example in decimal we have 5 in the hex decimal we also have 5
                        // but the above form use 05 instead of 5
                        // to full the chain
                        stringBuilder.Append(b.ToString("x2")); 
                    }
                    return stringBuilder.ToString();
                }
            }
        }  
    }
}
