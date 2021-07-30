using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Button _buttonOpen;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private Button _buttonBackToGame;
    [SerializeField] private Button _buttonExit;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0;

        _buttonOpen.onClick.AddListener(() =>
        {
            _animator.speed = 1;
            _animator.SetBool("ifOpened", true);
            Time.timeScale = 0;
        });

        _buttonClose.onClick.AddListener(() =>
        {
            _animator.SetBool("ifOpened", false);
            Time.timeScale = 1;
        });

        _buttonBackToGame.onClick.AddListener(() =>
        {
            _animator.SetBool("ifOpened", false);
            Time.timeScale = 1;
        });

        _buttonExit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
