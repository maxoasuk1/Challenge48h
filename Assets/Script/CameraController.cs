using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float speed = 75.0f;
    
    [SerializeField] private InputActionAsset inputAsset;
    private float rotateXAmount = 0.0f;
    private float rotateYAmount = 0.0f;

    private float x = 0.0f;
    private float y = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputAsset.Enable();
        InputActionMap m = inputAsset.FindActionMap("Camera");
        InputAction rotateX = m.FindAction("RotationX");
        
        rotateX.started += RotateXCallback;
        rotateX.performed += RotateXCallback;
        rotateX.canceled += RotateXCallback;
        
        InputAction rotateY = m.FindAction("RotationY");
        
        rotateY.started += RotateYCallback;
        rotateY.performed += RotateYCallback;
        rotateY.canceled += RotateYCallback;
        
        transform.position = target.position + new Vector3(0.0f, 0.0f, -distance);
        transform.LookAt(target);
    }

    public void RotateXCallback(InputAction.CallbackContext obj)
    {
        rotateXAmount = obj.ReadValue<float>();
    }

    public void RotateYCallback(InputAction.CallbackContext obj)
    {
        rotateYAmount = obj.ReadValue<float>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rotateYAmount != 0.0f || rotateXAmount != 0.0f)
        {
            x += rotateXAmount * speed * Time.deltaTime;
            y += rotateYAmount * speed * Time.deltaTime;
        }
        
        Quaternion rotation = Quaternion.Euler(x, y, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            
        transform.rotation = rotation;
        transform.position = position;
    }
}
