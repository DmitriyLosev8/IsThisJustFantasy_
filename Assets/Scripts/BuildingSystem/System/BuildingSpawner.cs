using Assets.Scripts.BuildingSystem.Buildings;
using Assets.Scripts.Props.Chest;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.System
{
    internal class BuildingSpawner
    {
        private BuildingsPool _buildingsPool;
        private SelectedUnitsHandler _selectedUnitsHandler;

        public BuildingSpawner(
            Tower tower,
            Barracks barracks,
            ResoorceBuilding resoorceBuilding,
            SelectedUnitsHandler handler)
        {
            _selectedUnitsHandler = handler;
            _buildingsPool = new BuildingsPool(tower, barracks, resoorceBuilding);
        }

        public Building CurrentBuilding { get; private set; }

        public void Spawn(
            int spawnPointIndex,
            Transform spawnPointTransform,
            ChestSpawnerPointsContainer chestSpawnPoints)
        {
            CurrentBuilding = _buildingsPool.GetBuilding(spawnPointIndex, chestSpawnPoints);
            CurrentBuilding.Transform.parent = spawnPointTransform;

            if (CurrentBuilding is Barracks barracks)
            {
                barracks.Init(_selectedUnitsHandler);
            }
        }
    }
}