using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Explosion))]
public class ExplosiveCube : MonoBehaviour
{
    private Renderer _renderer;
    private Explosion _explosion;

    public int DivisionChance { get; private set; }

    public event Action<Transform, int> Clicked;
    public event Action<Transform> Exploded;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _explosion = GetComponent<Explosion>();
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(transform, DivisionChance);
        Destroy(gameObject);
    }

    public void Initialize(int divisionChance, Vector3 scale, Color color)
    {
        DivisionChance = divisionChance;
        transform.localScale = scale;
        _renderer.material.color = color;
    }

    public void Explode()
    {
        _explosion.Explode();
        Exploded?.Invoke(transform);
    }
}