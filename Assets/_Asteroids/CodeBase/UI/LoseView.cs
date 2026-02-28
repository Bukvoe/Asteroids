using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Asteroids.CodeBase.UI
{
    public class LoseView : MonoBehaviour
    {
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

        private void RestartScene()
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}
