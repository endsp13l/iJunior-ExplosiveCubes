using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(DivisionSolver))]
[RequireComponent(typeof(Rigidbody))]
public class Divider : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 6;

    private float _scaleMultiplier = 0.5f;
    private float _divisionChanceMultiplier = 0.5f;
    
    private float _explosionForce = 20f;
    private float _explosionRadius = 1f;
    
    private DivisionSolver _divisionSolver;
    private Random _random = new Random();

    private void Awake()
    {
        _divisionSolver = GetComponent<DivisionSolver>();
    }

    private void OnEnable()
    {
        _divisionSolver.Divided += Divide;
    }

    private void OnDisable()
    {
        _divisionSolver.Divided -= Divide;
    }

    private void Divide( int divisionChance, Vector3 scale)
    {
        int count = GetRandomCount();
        Transform currentCube;
       
        for (int i = 0; i < count; i++)
        {
            currentCube = CreateCube();
            SetCubeStats(currentCube, divisionChance, scale);
        }
        
        GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        Destroy(gameObject);
    }

    private int GetRandomCount() => _random.Next(_minCubesCount, _maxCubesCount);

    private Transform CreateCube() => Instantiate(_cubePrefab, transform.position, Quaternion.identity).transform;

    private void SetCubeStats(Transform cube, int divisionChance, Vector3 scale)
    {
        cube.GetComponent<DivisionSolver>().SetDivisionChance((int)(divisionChance * _divisionChanceMultiplier));
        cube.localScale = scale * _scaleMultiplier;
    }
}