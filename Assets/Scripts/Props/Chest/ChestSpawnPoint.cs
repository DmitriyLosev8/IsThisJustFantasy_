using UnityEngine;

namespace Assets.Scripts.Props.Chest
{
    internal class ChestSpawnPoint : MonoBehaviour
    {
        [SerializeField] private int _coinsOfChest;

        public int CoinsOfChest => _coinsOfChest;
    }
}