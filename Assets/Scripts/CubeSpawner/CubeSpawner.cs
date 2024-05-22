using UnityEngine;
using Random = System.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosiveCube _cubePrefab;
    
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 6;

    private float _scaleMultiplier = 0.5f;
    private float _divisionChanceMultiplier = 0.5f;

    private Random _random = new Random();

    public void Spawn(Transform targetTransform, int divisionChance)
    {
        ExplosiveCube currentCube;

        int count = GetRandomCount();
        Vector3 scale = targetTransform.localScale * _scaleMultiplier;
        divisionChance = (int)(divisionChance * _divisionChanceMultiplier);

        for (int i = 0; i < count; i++)
        {
            currentCube = Instantiate(_cubePrefab, targetTransform.position, Quaternion.identity);
            currentCube.Initialize(divisionChance, scale);
        }
    }

    private int GetRandomCount() => _random.Next(_minCubesCount, _maxCubesCount);
}