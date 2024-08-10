using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.PlayerInput
{
    internal class DesktopInput : MonoBehaviour
    {
        [SerializeField] private LayerMask _ground;

        private SelectedUnitsHandler _selectedUnitsHandler;
        private InputActions _inputActions;
        private PlayerMovement _playerMover;
        private PlayerAttacker _playerAttacker;
        private WorldPointFinder _worldPointFinder;
        private PointerSelectableChecker _poinerChecker;

        private Vector2 _moveDirection;

        private void Update()
        {
            _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>();

            OnMoveInput(_moveDirection);
        }

        private void OnDisable()
        {
            _inputActions.Player.Attack.performed -= OnAttackInput;
            _inputActions.Player.ChangeWeapon.performed -= OnChangeWeaponInput;
            _inputActions.Player.MoveUnits.performed -= OnMoveUnits;

            _inputActions.Disable();
        }

        public void Init(Player player, SelectedUnitsHandler handler)
        {
            _inputActions = new InputActions();
            _inputActions.Enable();

            _playerMover = player.GetComponent<PlayerMovement>();
            _playerAttacker = player.GetComponent<PlayerAttacker>();
            _selectedUnitsHandler = handler;

            Cursor.visible = true;

            _worldPointFinder = new WorldPointFinder(_ground);
            _poinerChecker = new PointerSelectableChecker();

            _inputActions.Player.Attack.performed += OnAttackInput;
            _inputActions.Player.ChangeWeapon.performed += OnChangeWeaponInput;
            _inputActions.Player.MoveUnits.performed += OnMoveUnits;
        }

        private void OnMoveInput(Vector2 direction)
        {
            _playerMover.Move(direction);
        }

        private void OnAttackInput(InputAction.CallbackContext context)
        {
            if (_poinerChecker.IsPointerOverSelectableObject(Input.mousePosition) == false)
            {
                _playerAttacker.Attack();
            }
        }

        private void OnChangeWeaponInput(InputAction.CallbackContext context)
        {
            _playerAttacker.ChangeWeapon();
        }

        private void OnMoveUnits(InputAction.CallbackContext context)
        {
            _selectedUnitsHandler.MoveUnits(_worldPointFinder.GetPosition(Input.mousePosition));
        }
    }
}