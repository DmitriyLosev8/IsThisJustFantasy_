using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    internal abstract class PlayerComponent : MonoBehaviour
    {
        public abstract void Init(PlayerData data, PlayerSFX sfx);
    }
}
