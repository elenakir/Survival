using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Awake()
    {
        StartCoroutine(Rotate());
    }

     IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0, 0, _speed);
            yield return null;
        }
    }
}
