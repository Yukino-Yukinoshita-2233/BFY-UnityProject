using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private float moveSpeed = 1.5f;
  private Animator animator;
  private CharacterController controller;
  private int speedHash;
  private int isRunHash;

  void Start()
  {
    animator = GetComponent<Animator>();
    controller = GetComponent<CharacterController>();

    speedHash = Animator.StringToHash("speed");
    isRunHash = Animator.StringToHash("isRun");
  }

  void Update()
  {
    var info = animator.GetCurrentAnimatorStateInfo(0);
    if (!info.IsTag("Attack"))
    {
      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");
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
}
