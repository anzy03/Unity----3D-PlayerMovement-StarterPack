using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    /*
        This is a basic player movement script there the player movement is determined by using Physics & Maths.
    */
    [SerializeField]
    private float _moveSpeed = 20f;

    [SerializeField]
    private float _acceleration = 5f;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _castOffset = 0.25f;

    [SerializeField]
    private ForceMode _forceMode = ForceMode.Force;

    private Rigidbody _rigidBody;
    private Camera _camera;
    private Vector2 _moveInput;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        var rayCastPos = transform.position;
        rayCastPos += transform.forward * _castOffset;
        rayCastPos += Vector3.up * 0.25f;

        Ray downRay = new Ray(rayCastPos, Vector3.down);

        if (Physics.Raycast(downRay, out var hit, Mathf.Infinity, _groundLayer))
        {
            var groundNormal = hit.normal;
            var rightVector = _camera.transform.right;
            var forwardVector = Vector3.Cross(-groundNormal, rightVector);
            Debug.DrawRay(transform.position, forwardVector * 3f, Color.red);

            var currentVelocity = _rigidBody.velocity;
            var targetVelocity = (forwardVector * _moveInput.y + rightVector * _moveInput.x).normalized * _moveSpeed;
            targetVelocity.y = _rigidBody.velocity.y;
            var force = (targetVelocity - currentVelocity) * _acceleration;

            _rigidBody.AddForce(force, _forceMode);
        }
    }


}
