using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool sprint = false;
    private Vector2 direction, pointer;
    private Rigidbody rb;
    [SerializeField] private float movespeed = 300, sprintspeed = 450, sense = 1;
    [SerializeField] private Camera cam;
    private Vector3 camrotation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camrotation = cam.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //   transform.position += transform.forward * direction.y + transform.right * direction.x;
        Vector3 moveDirection = (sprint?sprintspeed:movespeed) * (transform.right * direction.x + transform.forward * direction.y).normalized;
        float y = rb.linearVelocity.y;
        moveDirection.y = y;
        rb.linearVelocity = moveDirection;
        camrotation.x += -pointer.y*sense;
        camrotation.x = Mathf.Clamp(camrotation.x, -90, 90);
        cam.transform.localEulerAngles = camrotation;
        transform.localEulerAngles += new Vector3(0, pointer.x, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        pointer = context.ReadValue<Vector2>();
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed) sprint = true;
        else sprint = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish")) SceneManager.LoadScene("Lich underground");
    }
}
