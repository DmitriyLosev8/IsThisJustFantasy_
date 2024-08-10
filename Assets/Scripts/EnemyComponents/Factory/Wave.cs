using UnityEngine;

namespace Assets.Scripts.EnemyComponents.Factory
{
    [System.Serializable]
    internal class Wave
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _meleeAmount;
        [SerializeField] private int _rangeAmount;
        [SerializeField] private int _spawnAmount;

        private int _spawnChangeValue = 1;

        public float SpawnDelay => _spawnDelay;
        public int MeleeAmount => _meleeAmount;
        public int RangeAmount => _rangeAmount;
        public int SpawnAmount => _spawnAmount;

        public void IncreaseSpawnAmount()
        {
            _spawnAmount += _spawnChangeValue;
        }

        public void DecreaceSpawnAmount()
        {
            _spawnAmount -= _spawnChangeValue;
        }
    }
}