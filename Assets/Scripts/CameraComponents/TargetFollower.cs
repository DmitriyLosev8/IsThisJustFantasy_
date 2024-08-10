using UnityEngine;

namespace Assets.Scripts.CameraComponents
{
    internal class TargetFollower : MonoBehaviour
    {
        [Header("Position")]

        [SerializeField] private float _offsetPositionY;
        [SerializeField] private float _offsetPositionX;
        [SerializeField] private float _offsetPositionZ;

        [Header("Rotation")]

        [SerializeField] private float _offsetRotationY;
        [SerializeField] private float _offsetRotationX;
        [SerializeField] private float _offsetRotationZ;

        private Transform _target;

        private int _quaternionWValue = 15;

        private void LateUpdate()
        {
            SetPosition();
            SetRotation();
        }

        public void Init(Transform tartget)
        {
            _target = tartget;
        }

        private void SetPosition()
        {
            transform.position = new Vector3(
                _target.transform.position.x + _offsetPositionX,
                _target.transform.position.y + _offsetPositionY,
                _target.transform.position.z + _offsetPositionZ);
        }

        private void SetRotation()
        {
            transform.rotation = new Quaternion(
                _offsetRotationX,
                _offsetRotationY,
                _offsetRotationZ,
                _quaternionWValue);
        }
    }
}