using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] private float _hueMin = 0f;
    [SerializeField] private float _hueMax = 1f;
    [SerializeField] private float _saturationMin = 0.5f;
    [SerializeField] private float _saturationMax = 1f;
    [SerializeField] private float _valueMin = 0.5f;
    [SerializeField] private float _valueMax = 1f;
    
    private Renderer _renderer;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Random.ColorHSV(_hueMin, _hueMax,
            _saturationMin, _saturationMax, _valueMin, _valueMax);
    }
}