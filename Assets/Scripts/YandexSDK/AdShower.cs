using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.YandexSDK
{
    internal abstract class AdShower : MonoBehaviour
    {
        private List<Button> _buttonsToDeactivate;
        private Pauser _pauser;

        public bool IsPaused { get; private set; }

        public abstract void Show();

        public void Init(Pauser pauser)
        {
            _pauser = pauser;
        }

        protected void OnOpenCallBack()
        {
            _pauser.Pause();
            IsPaused = true;

            foreach (Button button in _buttonsToDeactivate)
                button.interactable = false;
        }

        protected void OnCloseCallBack()
        {
            Resume();

            EnableButtons();
        }

        protected void OnCloseCallBack(bool wasShown)
        {
            Resume();

            if (wasShown == false)
                return;

            EnableButtons();
        }

        private void Resume()
        {
            _pauser.Resume();
            IsPaused = false;
        }

        private void EnableButtons()
        {
            foreach (Button button in _buttonsToDeactivate)
                button.interactable = true;
        }
    }
}