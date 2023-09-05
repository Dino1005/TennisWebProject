using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Backend.WebApi.Services
{
    public class FirebaseInitializer
    {
        private static FirebaseApp firebase;

        public static FirebaseApp Initialize()
        {
            if (firebase == null)
            {
                string pathToFirebaseCredentials = "C:\\Users\\radon\\Desktop\\TennisWebProject\\Backend\\Backend.WebApi\\firebaseConfig.json";

                firebase = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(pathToFirebaseCredentials),
                });
            }

            return firebase;
        }
    }
}