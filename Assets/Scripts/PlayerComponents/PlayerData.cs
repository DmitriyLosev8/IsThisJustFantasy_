using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    internal class PlayerData : ScriptableObject
    {
        [field: SerializeField] [field: Range(1, 10)]
        public float Speed { get; private set; }

        [field: SerializeField] [field: Range(1, 10)]
        public float AttackMoveSpeed { get; private set; }

        [field: SerializeField] [field: Range(1, 5)]
        public float RecoverTime { get; private set; }

        [field: SerializeField] [field: Range(1, 1000)]
        public float Health { get; private set; }

        [field: SerializeField] [field: Range(0, 5)]
        public float WeaponAttackSpeedLevelUpAmount { get; private set; }

        [field: SerializeField] [field: Range(0, 5)]
        public float WeaponDamageLevelUpAmount { get; private set; }
    }
}
