using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class ExplosionView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionEffectLifetime = 3f;

    public void ShowEffect(Transform target)
    {
        _explosionEffect = Instantiate(_explosionEffect, target.position, Quaternion.identity);
        _explosionEffect.Play();

        Destroy(_explosionEffect.gameObject, _explosionEffectLifetime);
    }
}