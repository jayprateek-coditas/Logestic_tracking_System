using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Project_ON_MVC.Services
{
   static public class Security
    {
        static public string Encrypt(string str)
        {
            string password = "";

            foreach (char ch in str)
            {
                var val = (int)ch;
                password += val.ToString() + "$";

            }
            Console.WriteLine(password);
            return password;
        }

        static public string Decrypt(string str)
        {
            string password = "";
            for (int i = 0; i < str.Length; i++)
            {
                string store = "";
                while (str[i] >= '0' && str[i] <= '9')
                {
                    store += str[i];
                    i++;
                }
                if (store != "")
                {
                    password += (char)(Convert.ToInt16(store));
                }
            }
            return password;
        }
    }
}