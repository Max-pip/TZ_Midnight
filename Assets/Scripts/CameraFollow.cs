using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _targetRigidbody;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    void FixedUpdate()
    {
        Vector3 playerForward = (_targetRigidbody.velocity + _target.transform.forward).normalized;
        Vector3 targetPos = _target.position + _target.transform.TransformVector(_offset) + playerForward;
        transform.position = Vector3.Lerp(transform.position, targetPos,_speed * Time.deltaTime);
        transform.LookAt(_target);
    }
}
