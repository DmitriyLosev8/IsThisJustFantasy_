using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class SpriteChanger : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _sprites;

        public void ChangeSprite()
        {
            foreach (var sprite in _sprites)
            {
                if (_image.sprite != sprite)
                {
                    _image.sprite = sprite;

                    return;
                }
            }
        }
    }
}
