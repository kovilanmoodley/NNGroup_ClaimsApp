using ShareModels.Models;

namespace NNGroup_FrontEnd.Server.Helper
{
    public class DataEncryption
    {
        public static int Encrypt(int ID) 
        { 
            return (ID * -1); 
        }
        public static int Decrypt(int ID)
        {
            return (ID * -1);
        }


    }
}
