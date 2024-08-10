using System.Collections.Generic;
using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Constants;
using Assets.Scripts.Props.Chest;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingsPool
    {
        private List<Tower> _towerPool;
        private int _countOfTowers = 10;
        private Barracks _barracks;
        private ResoorceBuilding _resoorceBuilding;

        public BuildingsPool(Tower tower, Barracks barracks, ResoorceBuilding resoorceBuilding)
        {
            CreateTowerPool(tower);
            CreateBarracks(barracks);
            CreateResoorceBuilding(resoorceBuilding);
        }

        public Building GetBuilding(int buildingPointIndex, ChestSpawnerPointsContainer chestSpawnPoints)
        {
            switch (buildingPointIndex)
            {
                case BuildingsHash.TowerIndex:

                    foreach (Tower tower in _towerPool)
                    {
                        if (tower.gameObject.activeSelf == false)
                        {
                            tower.gameObject.SetActive(true);

                            return tower;
                        }
                    }

                    break;

                case BuildingsHash.BarracksIndex:

                    if (_barracks.gameObject.activeSelf == false)
                    {
                        _barracks.gameObject.SetActive(true);

                        return _barracks;
                    }

                    break;

                case BuildingsHash.ResoorceBuildingIndex:

                    if (_resoorceBuilding.gameObject.activeSelf == false)
                    {
                        _resoorceBuilding.gameObject.SetActive(true);
                        _resoorceBuilding.SetChestsSpawnPoints(chestSpawnPoints);

                        return _resoorceBuilding;
                    }

                    break;
            }

            return null;
        }

        private void CreateTowerPool(Tower tower)
        {
            _towerPool = new List<Tower>();

            for (int i = 0; i < _countOfTowers; i++)
            {
               Tower currentTower = GameObject.Instantiate(tower);
               currentTower.gameObject.SetActive(false);
               _towerPool.Add(currentTower);
            }
        }

        private void CreateBarracks(Barracks barracks)
        {
            _barracks = GameObject.Instantiate(barracks);
            _barracks.gameObject.SetActive(false);
        }

        private void CreateResoorceBuilding(ResoorceBuilding resoorceBuilding)
        {
            _resoorceBuilding = GameObject.Instantiate(resoorceBuilding);
            _resoorceBuilding.gameObject.SetActive(false);
        }
    }
}