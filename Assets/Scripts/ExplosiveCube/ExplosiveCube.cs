using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    private const int TotalPercentsCount = 100;

    [SerializeField] private CubeSpawner _spawner;

    [SerializeField] private float _explosionForce = 20f;
    [SerializeField] private float _explosionRadius = 1f;

    private int _divisionChance = 100;
    private Random _random = new Random();
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown() => TryDivide();

    public void Initialize(int divisionChance, Vector3 scale)
    {
        _divisionChance = divisionChance;
        transform.localScale = scale;
    }

    private void TryDivide()
    {
        int divisionChance = _random.Next(0, TotalPercentsCount);

        if (divisionChance <= _divisionChance)
        {
            _spawner.Spawn(transform, _divisionChance);
            _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }
}