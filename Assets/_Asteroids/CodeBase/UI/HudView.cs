using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Asteroids.CodeBase.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI _positionLabel;
        [SerializeField, Required] private TextMeshProUGUI _angleLabel;
        [SerializeField, Required] private TextMeshProUGUI _speedLabel;

        [SerializeField, Required] private RectTransform _chargesRoot;
        [SerializeField, Required] private TextMeshProUGUI _chargesLabel;

        [SerializeField, Required] private TextMeshProUGUI _cooldownLabel;

        [SerializeField, Required] private TextMeshProUGUI _scoreLabel;

        public void UpdatePosition(Vector2 position)
        {
            _positionLabel.text = $"x: {position.x:F2} y: {position.y:F2}";
        }

        public void UpdateAngle(float angle)
        {
            _angleLabel.text = $"{angle:F2}";
        }

        public void UpdateSpeed(float speed)
        {
            _speedLabel.text = $"{speed:F2}";
        }

        public void ShowCharges()
        {
            _chargesRoot.gameObject.SetActive(true);
        }

        public void UpdateCharges(int current, int max)
        {
            _chargesLabel.text = $"{current}/{max}";
        }

        public void HideCharges()
        {
            _chargesRoot.gameObject.SetActive(false);
        }

        public void ShowCooldown()
        {
            _cooldownLabel.gameObject.SetActive(true);
        }

        public void UpdateCooldown(float reload)
        {
            _cooldownLabel.text = $"(reloading {reload:F1}s)";
        }

        public void HideCooldown()
        {
            _cooldownLabel.gameObject.SetActive(false);
        }

        public void UpdateScore(int value)
        {
            _scoreLabel.text = $"{value}";
        }
    }
}
