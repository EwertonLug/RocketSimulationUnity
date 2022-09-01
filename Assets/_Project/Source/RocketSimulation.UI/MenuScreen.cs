using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RocketSimulation.UI
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Button _buttonReload;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private TextMeshProUGUI _stopwatchText;

        private readonly string _sceneName = "RocketLauncherSimulation";

        private float _currentStopWatchTime = 10f;

        private void Awake()
        {
            _buttonReload.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(_sceneName);
            });

            _buttonExit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void FixedUpdate()
        {
            _currentStopWatchTime -= Time.deltaTime;

            if (_currentStopWatchTime <= 0)
            {
                _stopwatchText.enabled = false;
                _buttonReload.gameObject.SetActive(true);
                _buttonExit.gameObject.SetActive(true);
            }
            else
            {
                _stopwatchText.SetText(_currentStopWatchTime.ToString("N0"));
                _buttonReload.gameObject.SetActive(false);
                _buttonExit.gameObject.SetActive(false);
            }
        }
    }
}