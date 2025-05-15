using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;

    public float rotationSpeed;

    [Header("Camera")]
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

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (inputDir != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
