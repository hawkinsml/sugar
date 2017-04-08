using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sugar.Components.Commands;
using Sugar.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Sugar.Components.Commands
{
    class Passwordfile
    {
        public static String SharedSecret { get; set; }

        public static bool SaveFile(PasswordsModal data)
        {
            bool retVal = false;

            string json = JsonConvert.SerializeObject(data, new IsoDateTimeConverter());
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format("{0}\\{1}", path, "sugar.data");

            string encryptedData = CryptoHelper.EncryptStringAES(json, SharedSecret);

            encryptedData.SaveAsTextFile(fullFileName);
            return retVal;
        }

        public static PasswordsModal ReadFile()
        {
            PasswordsModal retVal = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullFileName = string.Format("{0}\\{1}", path, "sugar.data");

            try
            {
                String encryptedData;
                String json;
                using (StreamReader sr = new StreamReader(fullFileName))
                {
                    encryptedData = sr.ReadToEnd();
                    json = CryptoHelper.DecryptStringAES(encryptedData, SharedSecret);
                }

                retVal = JsonConvert.DeserializeObject<PasswordsModal>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return retVal;
        }
    }
}
