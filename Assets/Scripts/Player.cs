using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    public static Player Instance => _instance;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxHealth;

    public event Action OnWin;
    public event Action OnFail;

    public int MaxHealth => _maxHealth;

    public int Health
    {
        get  { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                OnFail?.Invoke();
                Time.timeScale = 0;
            }
            else if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    private static Player _instance;
    private int _health;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        _health = _maxHealth;
        transform.position = _spawnPoint.position;
    }
    
    public void Restart()
    {
        _health = _maxHealth;
        transform.position = _spawnPoint.position;
    }

    public void ApplyDamage(int amount)
    {
        Health -= amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            OnWin?.Invoke();
            Time.timeScale = 0;
        }
    }
}
