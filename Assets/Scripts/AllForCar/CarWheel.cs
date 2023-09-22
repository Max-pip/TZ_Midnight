using UnityEngine;

public class CarWheel : MonoBehaviour
{
    [SerializeField] private CarController _carController;

    [SerializeField] private float _modifierForRotate = 0.2f;
    private Vector3 _initRotation;

    void Start()
    {
        _initRotation = transform.localEulerAngles;    
    }

    void FixedUpdate()
    {
        float rotate = _carController.TurnValue * _carController.LimitRotation * _modifierForRotate;
        transform.localEulerAngles = _initRotation + new Vector3(0f, rotate, 0f);
    }
}
