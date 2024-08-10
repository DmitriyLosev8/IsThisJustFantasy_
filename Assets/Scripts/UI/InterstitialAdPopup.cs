using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class InterstitialAdPopup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupPanel;

        private float _seconds = 3;
        private WaitForSeconds _timeToShow;

        private void Start()
        {
            _timeToShow = new WaitForSeconds(_seconds);
        }

        public IEnumerator Show()
        {
            _popupPanel.SetActive(true);

            yield return _timeToShow;

            _popupPanel.SetActive(false);
        }
    }
}