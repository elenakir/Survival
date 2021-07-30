using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
    [Header("Panels")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _nextPanel;
    [Header("Buttons")]
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private Button _buttonRestartAfterWin;
    [SerializeField] private Button _buttonExit;

    private GameObject _currentLevel;
    private int _currentLevelNum;

    void Awake()
    {
        _currentLevelNum = 0;
        _currentLevel = Instantiate(_levels[0]);

        Player.Instance.OnFail += ShowFailPanel;
        Player.Instance.OnWin += ShowNextPanel;

        _buttonRestart.onClick.AddListener(() => 
        {
            UpdateLevel();
        });

        _buttonNext.onClick.AddListener(() =>
        {
            _currentLevelNum++;
            UpdateLevel();
        });

        _buttonExit.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _buttonRestartAfterWin.onClick.AddListener(() =>
        {
            _currentLevelNum = 0;
            UpdateLevel();
        });
    }

    private void UpdateLevel()
    {
        Time.timeScale = 1;
        Destroy(_currentLevel);
        HideAll();
        _currentLevel = Instantiate(_levels[_currentLevelNum]);
        Player.Instance.Restart();
    }

    private void ShowFailPanel()
    {
        _failPanel.SetActive(true);
    }

    private void ShowNextPanel()
    {
        if (_currentLevelNum < _levels.Length - 1)
        {
            _nextPanel.SetActive(true);
        }
        else _winPanel.SetActive(true);
    }

    private void HideAll()
    {
        _nextPanel.SetActive(false);
        _failPanel.SetActive(false);
        _winPanel.SetActive(false);
    }
}
