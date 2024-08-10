using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Tutorial
{
    internal class TutorialPanel : Screen
    {
        private static readonly Quaternion Rotation = new Quaternion(0.49638f, -0.14264f, 0.02853f, 0.85583f);

        private Coroutine _fade;
        private float _fadeTime = 15f;
        private WaitForSeconds _suspender;

        private void Awake()
        {
            _suspender = new WaitForSeconds(_fadeTime);
        }

        private void OnEnable()
        {
            transform.rotation = Rotation;
        }

        private void OnDisable()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public override void Open()
        {
            base.Open();

            StartFadeCoroutine();
        }

        private void StartFadeCoroutine()
        {
            if (_fade != null)
            {
                StopCoroutine(_fade);
            }

            _fade = StartCoroutine(Fade());
        }

        private IEnumerator Fade()
        {
            yield return _suspender;

            OnPanelClosed();
        }
    }
}