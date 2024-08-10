using Assets.Scripts.Constants;
using Assets.Scripts.PlayerComponents;
using UnityEngine;

namespace Assets.Scripts.Props.Chest
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    internal class Chest : MonoBehaviour
    {
        [SerializeField] private int _coins;
        [SerializeField] private ParticleSystem _particleOfGiveCoins;
        [SerializeField] private ParticleSystem _particleOfPosition;

        private PlayerWallet _playerWallet;
        private Animator _animator;
        private AudioSource _audiosourse;
        private bool _isEmpty = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audiosourse = GetComponent<AudioSource>();
            _playerWallet = new PlayerWallet();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player) && _isEmpty == false)
            {
                _animator.SetBool(AnimatorHash.IsPlayerNear, true);
                Instantiate(_particleOfGiveCoins, transform.position, Quaternion.identity);
                _audiosourse.Play();
                GiveCoinsToWallet();
                _particleOfPosition.Stop();
            }
        }

        public void SetCountOfCoins(int coins)
        {
            _coins = coins;
        }

        private void GiveCoinsToWallet()
        {
            _isEmpty = true;
            _playerWallet.AddCoins(_coins);
        }
    }
}