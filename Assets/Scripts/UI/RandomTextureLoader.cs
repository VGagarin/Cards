using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable CS0649
namespace UI
{
    public class RandomTextureLoader : MonoBehaviour
    {
        private const string UrlFormat = "https://picsum.photos/{0}/{1}";
        private const int Weight = 200;
        private const int Height = 200;

        [SerializeField] private RawImage _image;

        private void Start()
        {
            LoadImages(Weight, Height);
        }

        private void LoadImages(int x, int y)
        {
            string url = string.Format(UrlFormat, x, y);
            
            StartCoroutine(SetImageCoroutine(url));
        }

        private IEnumerator SetImageCoroutine(string url) {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            
            if (request.isNetworkError || request.isHttpError)
            {
                throw new Exception(request.error);
            }

            _image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;

            request.Dispose();
        }
    }
}