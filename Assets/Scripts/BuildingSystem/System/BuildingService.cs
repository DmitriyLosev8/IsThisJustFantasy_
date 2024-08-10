using System;
using System.Collections.Generic;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.Props.Chest;
using Assets.Scripts.UI;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingService : MonoBehaviour
    {
        [SerializeField] private BuilderButton _builder;
        [SerializeField] private List<BuildPoint> _buildPoints;
        [SerializeField] private Tower _tower;
        [SerializeField] private Barracks _barracks;
        [SerializeField] private ResoorceBuilding _resoorceBuilding;
        [SerializeField] private ChestSpawnerPointsContainer _chestSpawnPoints;

        private BuildingSpawner _buildingSpawner;
        private Transform _playersTransform;
        private PlayerWallet _playerWallet;
        private int _currentCostToBuild;
        private int _currentBuildPointIndex;
        private bool _canBuild;
        private Building _currentBuilding;
        private bool _isEventerExist = false;

        public event Action<ColliderPanelEventer> EventerWithAdButtonWasMade;

        private void OnEnable()
        {
            SignToBuildingsPointEvents();
            _builder.BuildButtonClicked += Build;
        }

        private void OnDisable()
        {
            UnSignToBuildingsPointEvents();
            _builder.BuildButtonClicked -= Build;

            if (_isEventerExist == true)
            {
                _currentBuilding.BuildWithEventorWasMade -= OnBuildWithEventorWasMade;
            }
        }

        public void Init(SelectedUnitsHandler handler)
        {
            _buildingSpawner = new BuildingSpawner(_tower, _barracks, _resoorceBuilding, handler);
            _playerWallet = new PlayerWallet();
        }

        private void Build()
        {
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].SpotToPlaceBuilding != null && _buildPoints[i].IsOccupied == false
                    && _playersTransform == _buildPoints[i].transform)
                {
                        if (_buildPoints[i].CostToBuild <= _playerWallet.Coins)
                        {
                            _buildingSpawner.Spawn(
                                _buildPoints[i].Index,
                                _buildPoints[i].SpotToPlaceBuilding,
                                _chestSpawnPoints);

                            _buildPoints[i].TakeSpot();
                            _buildPoints[i].FreeSpotToBuild(_buildingSpawner.CurrentBuilding);
                            _canBuild = false;
                            _buildPoints[i].DeactivateIconOfBuildPoint();
                            _builder.ToggleButton(_currentBuildPointIndex, _currentCostToBuild, _canBuild);
                            _playerWallet.SpendCoins(_buildPoints[i].CostToBuild);

                            SendEventer();
                        }
                }
            }
        }

        private void SendEventer()
        {
            _currentBuilding = _buildingSpawner.CurrentBuilding;
            _currentBuilding.BuildWithEventorWasMade += OnBuildWithEventorWasMade;
            _currentBuilding.AnnounceOfCreation();
        }

        private void SignToBuildingsPointEvents()
        {
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                _buildPoints[i].PlayerWentIn += OnPlayerWentIn;
                _buildPoints[i].PlayerWentOut += OnPlayerWentOut;
            }
        }

        private void UnSignToBuildingsPointEvents()
        {
            for (int i = 0; i < _buildPoints.Count; i++)
            {
                _buildPoints[i].PlayerWentIn -= OnPlayerWentIn;
                _buildPoints[i].PlayerWentOut -= OnPlayerWentOut;
            }
        }

        private void OnPlayerWentIn(Transform spotOfPlayer)
        {
            _playersTransform = spotOfPlayer;
            _canBuild = true;

            for (int i = 0; i < _buildPoints.Count; i++)
            {
                if (_buildPoints[i].transform == spotOfPlayer)
                {
                    _currentCostToBuild = _buildPoints[i].CostToBuild;
                    _currentBuildPointIndex = _buildPoints[i].Index;

                    _builder.ToggleButton(_currentBuildPointIndex, _currentCostToBuild, _canBuild);
                }
            }
        }

        private void OnPlayerWentOut()
        {
            _canBuild = false;
            _builder.ToggleButton(_currentBuildPointIndex, _currentCostToBuild, _canBuild);
        }

        private void OnBuildWithEventorWasMade(ColliderPanelEventer currentEventer)
        {
            EventerWithAdButtonWasMade?.Invoke(currentEventer);
            _isEventerExist = true;
        }
    }
}