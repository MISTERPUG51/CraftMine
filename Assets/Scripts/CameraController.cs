using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 turn;
    public Transform CameraTransform;
    public float sensitivity = 2.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        CameraTransform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        
    }
}
