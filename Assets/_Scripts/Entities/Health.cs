using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 2;
    [SerializeField] private GameObject _hitFx;

    public UnityEvent OnDie;

    public void GetHit(int damage)
    {
        _health -= damage;

        Instantiate(_hitFx, transform.position, Quaternion.identity);

        Debug.LogWarning($"{gameObject.name} health is {_health}.");

        if (_health <= 0)
        {
            OnDie?.Invoke();
            Debug.LogWarning($"{gameObject.name} is died!");
        }
    }
}