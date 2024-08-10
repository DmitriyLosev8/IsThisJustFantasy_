using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameLogic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class LevelsPanel : Screen
    {
        [SerializeField] private List<LevelButton> _levelButtons;
        [SerializeField] private SceneLoader _sceneLoader;

        private int _lastAvailableLevel = 1;

        private void OnDisable()
        {
            UnSignToButtons();
        }

        public void Init()
        {
            Open();
            SetLastAvailableLevel();
            ActiveAvailableLevels();
            SignToButtons();
        }

        private void SetLastAvailableLevel()
        {
            if (PlayerPrefs.GetInt(SceneNames.LastAvailableLevel) > _lastAvailableLevel)
            {
                _lastAvailableLevel = PlayerPrefs.GetInt(SceneNames.LastAvailableLevel);
            }
        }

        private void ActiveAvailableLevels()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Active();
            }
        }

        private void SignToButtons()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Clicked += LoadLevel;
            }
        }

        private void UnSignToButtons()
        {
            for (int i = 0; i < _lastAvailableLevel; i++)
            {
                _levelButtons[i].Clicked -= LoadLevel;
            }
        }

        private void LoadLevel(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName);
        }
    }
}