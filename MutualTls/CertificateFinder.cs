using System.Security.Cryptography.X509Certificates;

namespace MutualTls
{
    public static class CertificateFinder
    {
        public static X509Certificate2 FindBySubject(string subjectName, StoreName storeName = StoreName.My, StoreLocation storeLocation = StoreLocation.CurrentUser)
        {
            using (var certStore = new X509Store(storeName, storeLocation))
            {
                certStore.Open(OpenFlags.ReadOnly);
                var certCollection = certStore.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, subjectName, true);
                X509Certificate2 certificate = null;
                if (certCollection.Count > 0)
                {
                    certificate = certCollection[0];
                }
                return certificate;
            }
        }
    }
}