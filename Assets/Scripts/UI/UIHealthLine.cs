using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _healthRemainsLine;

    void Update()
    {
        _healthText.text = "Health " + Player.Instance.Health.ToString("");
        float h = Player.Instance.MaxHealth;
        _healthRemainsLine.fillAmount = Mathf.Lerp(_healthRemainsLine.fillAmount, Player.Instance.Health / h, Time.deltaTime * 10f);
    }
}
