using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Yandex
{
    public class SDKInitializer : MonoBehaviour
    {
        private const string MenuSceneName = "Menu";

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize(OnInitialized);
#endif
            yield return null;
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(MenuSceneName);
        }
    }
}