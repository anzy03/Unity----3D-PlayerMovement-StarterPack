using UnityEngine;

public class RoatationController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 2.25f;

    private void Update()
    {
        var xMouseInput = Input.GetAxis("Mouse X");
        Cursor.lockState = CursorLockMode.Locked;
        transform.Rotate(0f, xMouseInput * _rotationSpeed, 0f);
    }
}
