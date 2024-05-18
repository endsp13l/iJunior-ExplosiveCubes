using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed = 45f;
    [SerializeField] private Transform _rotationPoint;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        transform.RotateAround(_rotationPoint.position, Vector3.up, horizontal * _speed * Time.deltaTime);
    }
}