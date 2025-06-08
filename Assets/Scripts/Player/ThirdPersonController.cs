using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] internal Animator animator;
    internal string Name;
    internal int Class;
    private Vector2 direction, pointer;
    private Vector3 rot;
    [SerializeField] private float sense, speed, sprintSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private PlayerCamera cam;
    [SerializeField] Transform handR, handL, hip, torso;
    [SerializeField] Weapon weapon;
    [SerializeField] Inventory inv;
    private bool movedLastFrame = false;
    private bool drawing;
    private bool drawn;
    private Rigidbody rb;
    private float y, angle;
    private bool canRotate, sprint;
    private bool InDialogue;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DrawBehaviour.DrawEnter += DrawEnter;
        DrawBehaviour.DrawExit += DrawExit;
        SheatheBehaviour.SheatheExit += SheatheExit;
        DrawBehaviour.DrawUpdate += DrawUpdate;
        SheatheBehaviour.SheatheUpdate += SheatheUpdate;
    }


    void Update()
    {
        if (drawing)
        {
            return;
        }
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
    public void Inventory(InputAction.CallbackContext context)
    {
        if (!context.started)
        { return; }
        inv.transform.parent.gameObject.SetActive(!inv.transform.parent.gameObject.activeInHierarchy);
    }
    public void Draw(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        drawing = true;
        rb.linearVelocity = Vector3.zero;
        if (drawn)
        {

            animator.Play(weapon.GetWeaponSO().GetSheatheAnimationName());
        }
        else 
        {
            animator.Play(weapon.GetWeaponSO().GetDrawAnimationName());
        }
        Invoke("StopDrawing", 1);
    }
    private void StopDrawing()
    {
        drawing = false;
        drawn = !drawn;
        if (!drawn)
        {
// sword.parent = hip;
// sword.transform.localPosition = Vector3.zero;
//            sword.transform.localEulerAngles = Vector3.zero;
        }
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
                if (cam.targetType ==  typeof(Entity))
                {
                    if (PlayerData.instance.viewingDialogue)
                    {
                        return;
                    }
                    DialogueUI.instance.StartDialogue(cam.target.GetComponent<Entity>(), cam.target.GetComponent<Human>().dialogues[0]);
                }
                else if (cam.targetType == typeof(Collectable))
                {
                    inv.AddItem(cam.target.GetComponent<Collectable>().GetItem());
                    Destroy(cam.target);
                    cam.target = null;
                }
            }
        }
    }
    private void DrawEnter() 
    {
        if (weapon.GetWeaponSO().type == WeaponSO.Type.Single)
        {
            weapon.transform.parent = handR;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero; 
        } 
    }
    private void SheatheExit()
    {
        if (weapon.GetWeaponSO().type == WeaponSO.Type.Single)
        {
            weapon.transform.parent = hip;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero;
        }
    }
    private void DrawExit()
    {

    }
    private void DrawUpdate(AnimatorStateInfo info)
    {
        if (weapon.GetWeaponSO().type == WeaponSO.Type.Bow)
        {
            if (info.normalizedTime > 0.3)
            {
                weapon.transform.parent = handL;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localEulerAngles = Vector3.zero;
            }
        }
    }
    private void SheatheUpdate(AnimatorStateInfo info)
    {
        if (weapon.GetWeaponSO().type == WeaponSO.Type.Bow)
        {
            if (info.normalizedTime > 0.45)
            {
                weapon.transform.parent = torso;
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localEulerAngles = Vector3.zero;
            }
        }
    }
    private void OnDestroy()
    {
        DrawBehaviour.DrawEnter -= DrawEnter;
        DrawBehaviour.DrawExit -= DrawExit;
        SheatheBehaviour.SheatheExit -= SheatheExit;
        DrawBehaviour.DrawUpdate -= DrawUpdate;
        SheatheBehaviour.SheatheUpdate -= SheatheUpdate;
    }
}
