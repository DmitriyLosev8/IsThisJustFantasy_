using Agava.WebUtility;
using Agava.YandexGames;
using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.UI;
using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerInput
{
    internal class MobileInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attack;
        [SerializeField] private Button _changeWeapon;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private CanvasGroup _canvasGroup;

        private SelectedUnitsHandler _selectedUnitsHandler;
        private PlayerMovement _playerMover;
        private PlayerAttacker _playerAttacker;
        private Vector2 _moveDirection;
        private SpriteChanger _spriteChanger;

        private WorldPointFinder _worldPointFinder;
        private PointerSelectableChecker _poinerChecker;

        private float _doubleTapThreshold = 0.4f;
        private float _lastTapTime;

        private void Start()
        {
            _spriteChanger = _changeWeapon.GetComponent<SpriteChanger>();
            SetVisible();
        }

        private void Update()
        {
            HandleTouchInput();
            HandleJoystickInput();
        }

        private void OnDisable()
        {
            _attack.onClick.RemoveListener(OnAttackInput);
            _changeWeapon.onClick.RemoveListener(OnChangeWeaponInput);
        }

        public void Init(Player player, SelectedUnitsHandler handler)
        {
            _playerMover = player.GetComponent<PlayerMovement>();
            _playerAttacker = player.GetComponent<PlayerAttacker>();
            _selectedUnitsHandler = handler;

            _worldPointFinder = new WorldPointFinder(_ground);
            _poinerChecker = new PointerSelectableChecker();

            _attack.onClick.AddListener(OnAttackInput);
            _changeWeapon.onClick.AddListener(OnChangeWeaponInput);
        }

        public void SetVisible()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void SetInvisible()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void HandleJoystickInput()
        {
            Vector3 direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;

            _moveDirection = new Vector2(direction.x, direction.z);

            OnMoveInput(_moveDirection);
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (Time.time - _lastTapTime <= _doubleTapThreshold)
                    {
                        _lastTapTime = 0;

                        Vector3 position = _worldPointFinder.GetPosition(touch.position);

                        if (_poinerChecker.IsPointerOverSelectableObject(touch.position) == false)
                            OnMoveUnits(position);
                    }
                    else
                    {
                        _lastTapTime = Time.time;
                    }
                }
            }
        }

        private void OnMoveInput(Vector2 direction)
        {
            _playerMover.Move(direction);
        }

        private void OnAttackInput()
        {
            _playerAttacker.Attack();
        }

        private void OnChangeWeaponInput()
        {
            _playerAttacker.ChangeWeapon();
            _spriteChanger.ChangeSprite();
        }

        private void OnMoveUnits(Vector3 position)
        {
            _selectedUnitsHandler.MoveUnits(position);
        }
    }
}