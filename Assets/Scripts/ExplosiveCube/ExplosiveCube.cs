using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class ExplosiveCube : MonoBehaviour
{
    private Renderer _renderer;

    public int DivisionChance { get; private set; }

    public event Action<Transform, int> Clicked;
    public event Action<ExplosiveCube> Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
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

    public void Explode() => Exploded?.Invoke(this);
}