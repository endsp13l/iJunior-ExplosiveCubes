using System;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(ColorSetter))]
public class CubeSpawner : MonoBehaviour
{
    private const int TotalPercentsCount = 100;

    [SerializeField] private ExplosiveCube _cubePrefab;

    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 6;

    private float _scaleMultiplier = 0.5f;
    private float _spawnChanceMultiplier = 0.5f;

    private Vector3 _scale = Vector3.one;
    private int _spawnChance = 100;

    private Random _random = new Random();
    private ColorSetter _colorSetter;

    public event Action<ExplosiveCube> CubeSpawned;

    private void Awake()
    {
        _colorSetter = GetComponent<ColorSetter>();
        InitializeScene();
    }

    public void TrySpawn(Transform targetTransform, int spawnChance)
    {
        if (spawnChance <= 0)
            return;

        if (GetRandomChance() <= spawnChance)
            Spawn(targetTransform, spawnChance);
        else
            ExplodeCube(targetTransform);
    }

    private void Spawn(Transform targetTransform, int spawnChance)
    {
        int count = GetRandomCubesCount();
        Vector3 scale = targetTransform.localScale;
        Vector3 position = targetTransform.position;

        ApplyModifier(scale, spawnChance);

        for (int i = 0; i < count; i++)
            CreateCube(position, _scale, _spawnChance);
    }

    private void ApplyModifier(Vector3 scale, int spawnChance)
    {
        _scale = scale * _scaleMultiplier;
        _spawnChance = (int)(spawnChance * _spawnChanceMultiplier);
    }

    private void CreateCube(Vector3 position, Vector3 scale, int spawnChance)
    {
        ExplosiveCube cube = Instantiate(_cubePrefab, position, Quaternion.identity);

        cube.Initialize(spawnChance, scale, _colorSetter.GetRandomColor());
        CubeSpawned?.Invoke(cube);
    }

    private void ExplodeCube(Transform targetTransform)
    {
        if (targetTransform.TryGetComponent(out ExplosiveCube cube))
            cube.Explode();
    }

    private void InitializeScene()
    {
        for (int i = 0; i < GetRandomCubesCount(); i++)
            CreateCube(transform.position, _scale, _spawnChance);
    }

    private int GetRandomCubesCount() => _random.Next(_minCubesCount, _maxCubesCount);
    private int GetRandomChance() => _random.Next(0, TotalPercentsCount);
}