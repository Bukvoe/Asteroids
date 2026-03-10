using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Asteroids.CodeBase.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action PlayRequested;
        public event Action QuitRequested;

        [SerializeField, Required] private Button _playButton;
        [SerializeField, Required] private Button _quitButton;

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        private void OnPlayButtonClicked()
        {
            PlayRequested?.Invoke();
        }

        private void OnQuitButtonClicked()
        {
            QuitRequested?.Invoke();
        }
    }
}
