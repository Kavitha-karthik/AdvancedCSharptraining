using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASynchronousProgram
{
    public class Assigenment
    {
        static void Main(string[] args)
        {
            SignDocumnetUsingDSAsync("Message for Asyn");
            HashSantizedDocumentContentAsync("Hashed");
            EncryptDigestUsingPrivateKeyAync("Encrypted");
            Console.ReadLine();
        }
            static void SignDocumnetUsingDS(string documentContent)
        {
           string santizedDocContent= SantizeDocumentContent(documentContent);
           string digest= HashSantizedDocumentContent(santizedDocContent);
           string signedDocumentContent = EncryptDigestUsingPrivateKey(digest);
            Console.WriteLine(signedDocumentContent);
        }

        static  async void SignDocumnetUsingDSAsync(string documentContent)
        {
            String content = await SantizeDocumentContentAync(documentContent);
            Console.WriteLine("Content are ready");
           

        }

       
        private static Task<string> SantizeDocumentContentAync(string documentContent)
        {
            return Task.Run<string>(() =>
            {
            return SantizeDocumentContent(documentContent);
            });
        }
        static string SantizeDocumentContent(string documentContent)
        {
            Task.Delay(3000).Wait();
            Console.WriteLine("waiting to santized the content");
            return documentContent + " Santized ";
        }
        static async void HashSantizedDocumentContentAsync(string HashDocumentContent)
        {
            String content = await HashSantizedDocumentContentAync(HashDocumentContent);
            Console.WriteLine("Hashed are ready");


        }


        private static Task<string> HashSantizedDocumentContentAync(string documentContent)
        {
            return Task.Run<string>(() =>
            {
                return HashSantizedDocumentContent(documentContent);
            });
        }
        static string HashSantizedDocumentContent(string HashDocumentContent)
        {
            Task.Delay(3000).Wait();
            return HashDocumentContent + " Hashed ";
        }

        static async void EncryptDigestUsingPrivateKeyAync(string EncryDocumentContent)
        {
            String content = await EncryptDigestUsingPrivateKeyContent(EncryDocumentContent);
            Console.WriteLine("Encrypted are ready");


        }

        private static Task<string> EncryptDigestUsingPrivateKeyContent(string EncryptedDocumentContent)
        {
            return Task.Run<string>(() =>
            {
                return EncryptDigestUsingPrivateKey(EncryptedDocumentContent);
            });
        }
        
        static string EncryptDigestUsingPrivateKey(string digest)
        {
            Task.Delay(3000).Wait();
            return digest  + " Encrypted ";
        }
    }
}
