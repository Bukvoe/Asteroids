using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Asteroids.CodeBase.UI
{
    public class LoseView : MonoBehaviour
    {
        public event Action MainMenuRequested;
        public event Action RestartRequested;
        public event Action ReviveRequested;

        [SerializeField, Required] private TextMeshProUGUI _scoreLabel;
        [SerializeField, Required] private TextMeshProUGUI _bestScoreLabel;
        [SerializeField, Required] private TextMeshProUGUI _runsLabel;
        [SerializeField, Required] private TextMeshProUGUI _ufoDestroyedLabel;
        [SerializeField, Required] private Button _restartButton;
        [SerializeField, Required] private Button _reviveButton;
        [SerializeField, Required] private Button _mainMenuButton;

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(ToMainMenu);
            _restartButton.onClick.AddListener(RestartScene);
            _reviveButton.onClick.AddListener(Revive);
        }

        private void OnDestroy()
        {
            _mainMenuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _reviveButton.onClick.RemoveAllListeners();
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

        private void Revive()
        {
            ReviveRequested?.Invoke();
        }

        private void ToMainMenu()
        {
            MainMenuRequested?.Invoke();
        }

        public void UpdateBestScore(int bestScore)
        {
            _bestScoreLabel.text = bestScore.ToString();
        }

        public void UpdateRuns(int runs)
        {
            _runsLabel.text = runs.ToString();
        }

        public void UpdateUfoDestroyed(int ufoDestroyed)
        {
            _ufoDestroyedLabel.text = ufoDestroyed.ToString();
        }
    }
}
