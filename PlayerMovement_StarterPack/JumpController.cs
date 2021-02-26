using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpController : MonoBehaviour
{
    [Header("Jump Inputs")]
    [SerializeField]
    private KeyCode _jumpKey = KeyCode.Space;

    [SerializeField]
    private float _jumpSpeed = 12f;

    [Header("Physics Setting")]
    [SerializeField]
    private ForceMode _forceMode = ForceMode.VelocityChange;

    [Header("Ground Checking Settings")]
    [SerializeField]
    private float _groundCheckDistance = 0.1f;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _castOffset;

    private Rigidbody _rigidBody;
    private bool _jumpPressed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _jumpPressed |= Input.GetKeyDown(_jumpKey);
    }
    private void FixedUpdate()
    {
        if (_jumpPressed && IsGroundedAt(_castOffset))
        {
            var adjustedJumpSpeed = _jumpSpeed - _rigidBody.velocity.y;
            _rigidBody.AddForce(Vector3.up * _jumpSpeed, _forceMode);
        }

        _jumpPressed = false;
    }

    private bool IsGroundedAt(float offSetPosition)
    {
        var pos = new Vector3(transform.position.x, transform.position.y - offSetPosition, transform.position.z);

        Debug.DrawRay(pos, Vector3.down, Color.white, _groundCheckDistance);

        if (Physics.Raycast(pos, Vector3.down, _groundCheckDistance, _groundLayer))
        {
            print("Grounded");
            return true;
        }
        else
        {
            print("Not Grounded");
            return false;
        }
    }
}
