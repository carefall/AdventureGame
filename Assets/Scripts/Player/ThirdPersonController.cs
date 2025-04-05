using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    internal Animator animator;
    internal string Name;
    internal int Class;
    private Vector2 direction, pointer;
    private Vector3 rot;
    [SerializeField] private float sense, speed, sprintSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerCamera cam;
    private bool movedLastFrame = false;
    private Rigidbody rb;
    private float y, angle;
    private bool canRotate, sprint;
    private bool InDialogue;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (direction == Vector2.zero)
        {
            if (canRotate)
            {
                rot += new Vector3(pointer.y, pointer.x, 0) * Time.deltaTime * sense;
                rot.x = Mathf.Clamp(rot.x, -90, 90);
                orientation.eulerAngles = rot;
            }
            movedLastFrame = false;
            animator.SetBool("Move", false);
        }
        else
        {
            angle = y + Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            animator.SetBool("Move", true);
            if (!movedLastFrame) 
            {
               // transform.eulerAngles = new Vector3(0, orientation.eulerAngles.y, 0);
                y = orientation.eulerAngles.y;
            }
            movedLastFrame = true;
            if (canRotate)
            {
                rot += new Vector3(pointer.y, pointer.x, 0) * Time.deltaTime * sense;
                rot.x = Mathf.Clamp(rot.x, -90, 90);
                orientation.eulerAngles = rot;
            }
            Vector3 target = transform.eulerAngles;
            target.y = angle;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(target), 1f * Time.deltaTime);
            rb.linearVelocity = transform.forward * (sprint? sprintSpeed:speed);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void Look(InputAction.CallbackContext context)
    {
        pointer = context.ReadValue<Vector2>();
    }
    public void RMB(InputAction.CallbackContext context)
    {
        canRotate = context.performed;
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        sprint = context.performed;
        animator.SetBool("run", sprint);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (cam.target)
            {
                if (PlayerData.instance.viewingDialogue)
                {
                    return;
                }
                DialogueUI.instance.StartDialogue(cam.target.GetComponent<Entity>(), cam.target.GetComponent<Human>().dialogues[0]);

            }
        }
    }
}
