using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace Assets.Scripts.UI.Leaderboard
{
    internal class LeaderboardView : MonoBehaviour
    {
        private readonly List<LeaderboardEntryView> _leaderboardEntryViews = new ();

        [SerializeField] private LeaderboardEntryView _playerViewTemplate;

        public void Create(LeaderboardEntryResponse entry)
        {
            LeaderboardEntryView view = Instantiate(_playerViewTemplate, transform);
            view.SetData(entry);
            _leaderboardEntryViews.Add(view);
        }

        public void Clear()
        {
            foreach (var entry in _leaderboardEntryViews)
            {
                Destroy(entry.gameObject);
            }

            _leaderboardEntryViews.Clear();
        }
    }
}