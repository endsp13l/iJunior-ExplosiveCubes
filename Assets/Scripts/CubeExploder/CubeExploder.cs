using System;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    private const int BasicScaleValue = 1;

    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 1f;

    public event Action<Transform> Exploded;

    public void Explode(Transform targetTransform)
    {
        float explosionForce = _explosionForce;
        float explosionRadius = _explosionRadius;
        float modifier = BasicScaleValue / targetTransform.localScale.x;

        if (targetTransform.localScale != Vector3.one)
        {
            Debug.Log("Apply modifier" + modifier);
            explosionForce *= modifier;
            explosionRadius *= modifier;
        }
        
        Exploded?.Invoke(targetTransform);

        targetTransform.GetComponent<Rigidbody>()
            .AddExplosionForce(explosionForce, targetTransform.position, explosionRadius);
        
        Debug.Log("explosion end");
    }
}