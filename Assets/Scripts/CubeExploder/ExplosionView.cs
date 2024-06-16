using UnityEngine;

public class ExplosionView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private float _explosionEffectLifetime = 3f;

    public void ShowEffect(Transform target)
    {
        ParticleSystem effect = Instantiate(_explosionEffectPrefab, target.position, Quaternion.identity);
        effect.Play();

        Destroy(effect.gameObject, _explosionEffectLifetime);
    }
}