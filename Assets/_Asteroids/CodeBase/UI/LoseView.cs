using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Asteroids.CodeBase.UI
{
    public class LoseView : MonoBehaviour
    {
        public event Action RestartRequested;

        [SerializeField, Required] private TextMeshProUGUI _scoreLabel;
        [SerializeField, Required] private Button _restartButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(RestartScene);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            _scoreLabel.text = score.ToString();
        }

        private void RestartScene()
        {
            RestartRequested?.Invoke();
        }
    }
}
