using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Tutorial
{
    internal class Tutorial : MonoBehaviour
    {
        [SerializeField] private TutorialArrow[] _arrows;
        [SerializeField] private TutorialPanel[] _panels;

        private int _tutorialIndex = 0;
        private float _nextTutorialDelay = 10f;

        private void Start()
        {
            _arrows[0].gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (var arrow in _arrows)
            {
                arrow.Collected -= StartTutorialCoroutine;
            }
        }

        public void Init(Transform player)
        {
            foreach (var arrow in _arrows)
            {
                arrow.gameObject.SetActive(false);
                arrow.Init(player);
                arrow.Collected += StartTutorialCoroutine;
            }
        }

        private void StartTutorialCoroutine()
        {
            StartCoroutine(NextTutorialDelay());
        }

        private IEnumerator NextTutorialDelay()
        {
            if (_panels[_tutorialIndex] != null)
                _panels[_tutorialIndex].Open();

            yield return new WaitForSeconds(_nextTutorialDelay);

            ShowNextTutorial();
        }

        private void ShowNextTutorial()
        {
            if (_tutorialIndex < _arrows.Length - 1)
            {
                _tutorialIndex++;

                _arrows[_tutorialIndex].gameObject.SetActive(true);
            }
        }
    }
}