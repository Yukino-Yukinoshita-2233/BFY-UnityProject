using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float moveSpeed = 3f;
    private float mouseX;
    private float mouseY;
    private Animator animator;
    private CharacterController controller;
    private int speedHash;
    private int isRunHash;
    float Monster_Gravity = -0.98f;
    Vector3 Gravity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        GetAnimatorHash();
    }

    void GetAnimatorHash()
    {
        speedHash = Animator.StringToHash("speed");
        isRunHash = Animator.StringToHash("isRun");
    }

    void Update()
    {
        Gravity = new Vector3(0, Monster_Gravity, 0);
        controller.Move(transform.rotation * Gravity * Time.deltaTime);


        HandleMovement();
        MouseController();

    }

    void HandleMovement()
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsTag("Attack"))
        {
            float horizontal = Input.GetAxis(GameConstants.AxisNameHorizontal);
            float vertical = Input.GetAxis(GameConstants.AxisNameVertical);
            Vector3 dir = new Vector3(horizontal, 0, vertical);
            if (dir != Vector3.zero)
            {
                controller.Move(transform.rotation * dir * moveSpeed * Time.deltaTime);
                animator.SetBool(isRunHash, true);
                animator.SetFloat(speedHash, moveSpeed);
            }
            else
            {
                animator.SetBool(isRunHash, false);
            }
        }
    }

    void MouseController()
    {
        mouseX = Input.GetAxis(GameConstants.MouseAxisNameHorizontal) * rotationSpeed * 10 * Time.deltaTime;
        transform.Rotate(new Vector3(0f, mouseX, 0f), Space.Self);

        mouseY -= Input.GetAxis(GameConstants.MouseAxisNameVertical) * rotationSpeed * 10 * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -40f, 40f);
        Camera.main.transform.localEulerAngles = new Vector3(mouseY, 0, 0);
    }
}
