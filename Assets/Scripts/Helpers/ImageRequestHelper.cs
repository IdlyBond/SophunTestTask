using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class ImageRequestHelper
    {
        private static string imageCachePath = Application.persistentDataPath + "/images/";
        
        public static async Task<Texture> RequestTexture(string url)
        {
            if (!Directory.Exists(imageCachePath))
            {
                Directory.CreateDirectory(imageCachePath);
            }
            
            string filePath = imageCachePath + url.GetHashCode() + ".png";
            
            if (File.Exists(filePath))
            {
                byte[] fileData = await File.ReadAllBytesAsync(filePath);
                Texture2D texture = new (2, 2);
                texture.LoadImage(fileData);
                return texture;
            }
            
            using UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(url);
            UnityWebRequestAsyncOperation operation = imageRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (imageRequest.result != UnityWebRequest.Result.ConnectionError &&
                imageRequest.result != UnityWebRequest.Result.ProtocolError)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
                CacheImage(texture, filePath);
                return texture;

            }
            Debug.LogError($"Error downloading image: {imageRequest.error}");
            return null;
            
        }

        private static async void CacheImage(Texture2D texture, string filePath)
        {
            byte[] imageData = texture.EncodeToPNG();
            await File.WriteAllBytesAsync(filePath, imageData);
        }
    }
}