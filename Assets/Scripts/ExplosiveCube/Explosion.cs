using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplosiveCube))]
[RequireComponent(typeof(ExplosionView))]
public class Explosion : MonoBehaviour
{
    private const string CubeLayerName = "ExplosiveCube";
    
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 1f;

    private ExplosionView _explosionView;
    private ExplosiveCube _explosiveCube;

    private void Awake()
    {
        _explosionView = GetComponent<ExplosionView>();
        _explosiveCube = GetComponent<ExplosiveCube>();
    }

    public void Explode()
    {
        _explosionView.ShowEffect(transform);
        
        foreach (Rigidbody cube in GetCubes())
        {
            cube.AddExplosionForce(GetExplosionForce(), transform.position, GetExplosionRadius());
        }
    }

    private List<Rigidbody> GetCubes()
    {
        //int layer = _explosiveCube.gameObject.layer;
        int layer = LayerMask.GetMask(CubeLayerName);
        
        Collider[] hits =
            Physics.OverlapSphere(transform.position, GetExplosionRadius(), layer);
        
        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody)
                cubes.Add(hit.attachedRigidbody);
        }
        
        return cubes;
    }

    private float GetExplosionForce() => _explosionForce / transform.localScale.x;

    private float GetExplosionRadius() => _explosionRadius / transform.localScale.x;
}