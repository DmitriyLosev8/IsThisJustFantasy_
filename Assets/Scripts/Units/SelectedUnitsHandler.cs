using System;
using System.Collections.Generic;
using Assets.Scripts.PlayerUnits;
using UnityEngine;

namespace Assets.Scripts.Units
{
    internal class SelectedUnitsHandler : IDisposable
    {
        private List<Selectable> _units;
        private List<Selectable> _selectedUnits;

        private ArmyFormation _armyFormation;
        private bool _isInitialized;

        public void Init(Unit[] units)
        {
            _units = new List<Selectable>();
            _selectedUnits = new List<Selectable>();
            _armyFormation = new ArmyFormation();

            foreach (var unit in units)
            {
                unit.Selected += OnSelect;
                unit.Deselected += OnDeselct;

                _units.Add(unit);
            }

            _isInitialized = true;
        }

        public void OnSelect(Selectable unit)
        {
            _selectedUnits.Add(unit);
        }

        public void OnDeselct(Selectable unit)
        {
            _selectedUnits.Remove(unit);
        }

        public void MoveUnits(Vector3 position)
        {
            if (_isInitialized == false)
                return;

            if (_selectedUnits.Count > 0)
            {
                Vector3[] formation = _armyFormation.GetFormationDestination(position, _selectedUnits.Count);

                for (int i = 0; i < _selectedUnits.Count; i++)
                {
                    if (_selectedUnits[i] is Knight knight)
                    {
                        knight.GetComponent<KnightMovement>().Move(formation[i]);
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (Selectable unit in _units)
            {
                unit.Selected -= OnSelect;
                unit.Deselected -= OnDeselct;
            }
        }
    }
}