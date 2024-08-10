using System;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.PlayerComponents;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spotToPlaceBuilding;
        [SerializeField] private int _index;
        [SerializeField] private GameObject _iconOfBuildPoint;
        [SerializeField] private int _costToBuild;

        private bool _isOccupied;
        private int speedOfRotateVisualObject = 200;
        private Building _currentBuilding;
        private int _numberToSetRaiseValue = 5;

        public event Action<Transform> PlayerWentIn;
        public event Action PlayerWentOut;

        public Transform SpotToPlaceBuilding => _spotToPlaceBuilding;

        public int Index => _index;

        public bool IsOccupied => _isOccupied;

        public int CostToBuild => _costToBuild;

        private void Update()
        {
            RotateIconOfBuildingPoint();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null && other.gameObject.TryGetComponent(out Player player))
            {
                if (_isOccupied == false)
                {
                    PlayerWentIn?.Invoke(transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other != null && other.gameObject.TryGetComponent(out Player player))
            {
                PlayerWentOut?.Invoke();
            }
        }

        private void OnDisable()
        {
            if (_currentBuilding != null)
            {
                _currentBuilding.Destroyed -= FreeSpotToBuild;
            }
        }

        public void TakeSpot()
        {
            _isOccupied = true;
        }

        public void FreeSpotToBuild(Building biulding)
        {
            _currentBuilding = biulding;

            if (_isOccupied == true && _currentBuilding.Transform.parent.position == _spotToPlaceBuilding.position)
            {
                _currentBuilding.Destroyed += FreeSpotToBuild;
            }
        }

        public void ActiveIconOfBuildPoint()
        {
            _iconOfBuildPoint.SetActive(true);
        }

        public void DeactivateIconOfBuildPoint()
        {
            if (_iconOfBuildPoint.gameObject.activeSelf == true)
            {
                _iconOfBuildPoint.SetActive(false);
            }
        }

        private void RotateIconOfBuildingPoint()
        {
            _iconOfBuildPoint.transform.Rotate(0, speedOfRotateVisualObject * Time.deltaTime, 0);
        }

        private void RaiseCostForNextBuilding()
        {
            int valueToRaise = _costToBuild / _numberToSetRaiseValue;
            _costToBuild += valueToRaise;
        }

        private void FreeSpotToBuild()
        {
            if (_isOccupied == true && _currentBuilding.transform.position == _spotToPlaceBuilding.position)
            {
                _isOccupied = false;
                ActiveIconOfBuildPoint();
                RaiseCostForNextBuilding();
            }
        }
    }
}