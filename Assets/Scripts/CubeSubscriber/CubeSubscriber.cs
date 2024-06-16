using UnityEngine;

public class CubeSubscriber : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeExploder _cubeExploder;

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += Subscribe;
        _cubeExploder.CubeExploded += Unsubscribe;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= Subscribe;
        _cubeExploder.CubeExploded -= Unsubscribe;
    }

    public void Subscribe(ExplosiveCube cube)
    {
        cube.Clicked += _cubeSpawner.TrySpawn;
        cube.Exploded += _cubeExploder.Explode;
    }

    public void Unsubscribe(ExplosiveCube cube)
    {
        cube.Clicked -= _cubeSpawner.TrySpawn;
        cube.Exploded -= _cubeExploder.Explode;
    }
}