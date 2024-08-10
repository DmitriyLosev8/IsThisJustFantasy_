using System.Collections.Generic;
using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GameLogic.Utilities
{
    internal class PointerSelectableChecker
    {
        private LayerMask _ui = 5;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _results;

        public PointerSelectableChecker()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);
            _results = new List<RaycastResult>();
        }

        public bool IsPointerOverSelectableObject(Vector2 screenPosition)
        {
            _results.Clear();
            _pointerEventData.position = screenPosition;
            EventSystem.current.RaycastAll(_pointerEventData, _results);

            foreach (RaycastResult result in _results)
            {
                if (result.gameObject.layer == _ui || result.gameObject.GetComponent<Selectable>() != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}