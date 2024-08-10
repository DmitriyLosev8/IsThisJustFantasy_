using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Props.Chest
{
    internal class ChestSpawnerPointsContainer : MonoBehaviour
    {
        [SerializeField] private List<ChestSpawnPoint> _spawnPoints;

        public List<ChestSpawnPoint> SpawnPoints => _spawnPoints;
    }
}