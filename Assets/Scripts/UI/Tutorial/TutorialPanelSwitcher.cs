using Agava.WebUtility;
using UnityEngine;

namespace Assets.Scripts.UI.Tutorial
{
    internal class TutorialPanelSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _desktopTutorial;
        [SerializeField] private GameObject _mobileTutorial;

        private void Start()
        {
            if (Device.IsMobile)
                _mobileTutorial.gameObject.SetActive(true);
            else
                _desktopTutorial.gameObject.SetActive(true);
        }
    }
}