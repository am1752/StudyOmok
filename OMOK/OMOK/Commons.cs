using System;
using System.Security.Cryptography;
using System.Text;

namespace OMOK
{
    public static class Room
    {
        public static int currentRoomNum;
    }

    public class Commons
    {
        public static string ROOMName = "";
        public static int Mousedown = 0;
        public static int roomPerson;
        public static string UserId;
        public static string PW;

        public static string GetMd5Hash(MD5 mD5Hash, string input)
        {
            byte[] data = mD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sbuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sbuilder.Append(data[i].ToString("x2"));
            }

            return sbuilder.ToString();
        }

        


        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
