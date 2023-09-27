using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    #region Fields
    public event Action OnScoreUpdate;

    [field: SerializeField] public Rigidbody Rigidbody { get;  private set; }
    [field: SerializeField] public Transform DetailSpawn { get; private set; }

    [Header("Update Parameters")]
    [SerializeField] private float _updateAccel = 15f;
    [SerializeField] private float _updateGripX = 12f;
    [SerializeField] private float _updateGripZ = 3f;
    [SerializeField] private float _updateTopSpeed = 30f;
    [SerializeField] private float _updateRotationVelocity = 0.8f;
    [field: SerializeField] public float LimitRotation { get; private set; } = 170;
    private float _rotate;
    private float _slip;

    // The actual value to be used (modification of parameters)
    private float _accel;
    private float _gripX;
    private float _gripZ;
    private float _topSpeed;
    private float _rotationVelocity;

    [Header("Center of mass")]
    [SerializeField] private Vector3 _centerOfMass = new Vector3(0f, -1f, 0f);

    [Header("Rotation parameters")]
    [SerializeField] private AnimationCurve _slipEnter;
    [SerializeField] private AnimationCurve _slipBack;
    [SerializeField] private float _slipModification = 20f;
    [SerializeField] private float _minRotSpeed = 1f;
    [SerializeField] private float _maxRotSpeed = 4f;

    [Header("Trail")]
    [SerializeField] private TrailRenderer _leftTrail;
    [SerializeField] private TrailRenderer _rightTrail;

    private float _rightDragValue = 1f;
    private float _forwardDragValue = 1f;

    private bool _isRotating = false;
    public bool IsSlip { get; private set; } = false;
    public bool IsCanInvokeEvent { get; set; } = true;

    public float ForwardValue { get; set; } = 0f;
    public float TurnValue { get; set; } = 0f;

    private Vector3 _velocityVector = new Vector3(0, 0, 0);
    private Vector3 _pVelocityVector = new Vector3(0, 0, 0);

    private Bounds _bounds;
    private MeshCollider _myMeshCollider;
    #endregion

    public void Initialization()
    {
        AllForStart();
    }

    private void AllForStart()
    {
        _myMeshCollider = GetComponentInChildren<MeshCollider>();
        _bounds = GetBounds(gameObject);

        Rigidbody.centerOfMass = Vector3.Scale(_bounds.extents, _centerOfMass);
    }

    private void FixedUpdate()
    {
        _accel = _updateAccel;
        _gripX = _updateGripX;
        _gripZ = _updateGripZ;
        _topSpeed = _updateTopSpeed;
        _rotationVelocity = _updateRotationVelocity;
        _rotate = LimitRotation;

        FixedUpdateMethods();

        Rigidbody.velocity = transform.TransformDirection(_velocityVector);
    }

    #region FixedUpdateMethods
    private void FixedUpdateMethods()
    {
        AdjustmentOfSlope();
        RotateBehaviour();
        Controller();
        RotateVelocityVector();
        Grip();
        MaxSpeed();
        Trail();
    }

    private void AdjustmentOfSlope()
    {
        _accel = _accel * Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad);
        _accel = _accel > 0f ? _accel : 0f;
        _gripZ = _gripZ * Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad);
        _gripZ = _gripZ > 0f ? _gripZ : 0f;
        _gripX = _gripX * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
        _gripX = _gripX > 0f ? _gripX : 0f;
    }

    private void RotateBehaviour()
    {
        if (_pVelocityVector.magnitude < _minRotSpeed)
        {
            _rotate = 0;
        } else
        {
            _rotate = _pVelocityVector.magnitude / _maxRotSpeed * _rotate;
        }

        if (_rotate > LimitRotation) _rotate = LimitRotation;

        Slip();

        _rotate *= (1f - 0.3f * _slip);
        _rotationVelocity *= (1f - _slip);
    }

    private void Slip()
    {
        if (!IsSlip)
        {
            _slip = this._slipEnter.Evaluate(Mathf.Abs(_pVelocityVector.x) / _slipModification);
            if (_slip == 1f) IsSlip = true;
            if (IsSlip && IsCanInvokeEvent)
            {
                OnScoreUpdate?.Invoke();
                IsCanInvokeEvent = false;
            }
        }
        else
        {
            _slip = this._slipBack.Evaluate(Mathf.Abs(_pVelocityVector.x) / _slipModification);
            if (_slip != 1f) IsSlip = false;
        }
    }

    private void RotateVelocityVector()
    {
        _velocityVector = transform.InverseTransformDirection(Rigidbody.velocity);

        if (_isRotating)
        {
            _velocityVector = _velocityVector * (1f - _rotationVelocity) + _pVelocityVector * _rotationVelocity;
        }
    }

    private void Grip()
    {
        // Sideway grip
        _rightDragValue = _velocityVector.x > 0f ? 1f : -1f;
        _velocityVector.x -= _rightDragValue * _gripX * Time.deltaTime;
        if (_velocityVector.x * _rightDragValue < 0f) _velocityVector.x = 0f;

        // Straight grip
        _forwardDragValue = _velocityVector.z > 0f ? 1f : -1f;
        _velocityVector.z -= _forwardDragValue * _gripZ * Time.deltaTime;
        if (_velocityVector.z * _forwardDragValue < 0f) _velocityVector.z = 0f;
    }

    private void MaxSpeed()
    {
        if (_velocityVector.z > _topSpeed)
        {
            _velocityVector.z = _topSpeed;
        } else if (_velocityVector.z < -_topSpeed)
        {
            _velocityVector.z = -_topSpeed;
        }
    }

    private void Controller()
    {
        if (ForwardValue > 0.5f || ForwardValue < -0.5f)
        {
            Rigidbody.velocity += transform.forward * ForwardValue * _accel * Time.deltaTime;
            _gripZ = 0f;
        }

        _isRotating = false;

        _pVelocityVector = transform.InverseTransformDirection(Rigidbody.velocity);

        if (TurnValue > 0.5f || TurnValue < -0.5f)
        {
            float dir = (_pVelocityVector.z < 0) ? -1 : 1;
            RotateGradConst(TurnValue * dir);
        }
    }

    Vector3 drot = new Vector3(0f, 0f, 0f);

    private void RotateGradConst(float isCW)
    {
        drot.y = isCW * _rotate * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(drot.y, transform.up);
        _isRotating = true;
    }

    private void Trail()
    {
        if (_leftTrail == null && _rightTrail == null)
        {
            return;
        }

        if (IsSlip)
        {
            _leftTrail.emitting = true;
            _rightTrail.emitting = true;
        } else
        {
            _leftTrail.emitting = false;
            _rightTrail.emitting = false;
        }
    }
    #endregion

    private Bounds GetBounds(GameObject obj)
    {

        // Switch every collider to renderer for more accurate result
        Bounds bounds = new Bounds();
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        if (colliders.Length > 0)
        {

            //Find first enabled renderer to start encapsulate from it
            foreach (Collider collider in colliders)
            {

                if (collider.enabled)
                {
                    bounds = collider.bounds;
                    break;
                }
            }

            //Encapsulate (grow bounds to include another) for all collider
            foreach (Collider collider in colliders)
            {
                if (collider.enabled)
                {
                    bounds.Encapsulate(collider.bounds);
                }
            }
        }
        return bounds;
    }
}
