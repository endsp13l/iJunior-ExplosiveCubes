using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplosionView))]
public class CubeExploder : MonoBehaviour
{
    private const string CubeLayerName = "ExplosiveCube";

    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 1f;

    private ExplosionView _explosionView;
    private float _targetScale;

    public event Action<ExplosiveCube> CubeExploded;

    private void Awake()
    {
        _explosionView = GetComponent<ExplosionView>();
    }

    public void Explode(ExplosiveCube cube)
    {
        Transform targetTransform = cube.transform;
        _targetScale = targetTransform.localScale.x;

        _explosionView.ShowEffect(targetTransform);
        CubeExploded?.Invoke(cube);

        foreach (Rigidbody explosiveCube in GetCubes(targetTransform))
        {
            explosiveCube.AddExplosionForce(GetExplosionForce(_targetScale), targetTransform.position,
                GetExplosionRadius(_targetScale));
        }
    }

    private List<Rigidbody> GetCubes(Transform targetTransform)
    {
        int layerMask = LayerMask.GetMask(CubeLayerName);

        Collider[] hits =
            Physics.OverlapSphere(targetTransform.position, GetExplosionRadius(_targetScale), layerMask);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }

    private float GetExplosionForce(float scale) => _explosionForce / scale;

    private float GetExplosionRadius(float scale) => _explosionRadius / scale;
}