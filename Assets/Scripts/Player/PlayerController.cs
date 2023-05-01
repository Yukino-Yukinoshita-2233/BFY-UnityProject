using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private float speed = 2;
  private Animator animator;
  private int speedHash;
  private int isRunHash;

  void Start()
  {
    animator = GetComponent<Animator>();
    speedHash = Animator.StringToHash("speed");
    isRunHash = Animator.StringToHash("isRun");
  }

  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    Vector3 dir = new Vector3(horizontal, 0, vertical);
    if (dir != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(dir);
      animator.SetBool(isRunHash, true);
      animator.SetFloat(speedHash, speed);
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    else
    {
      animator.SetBool(isRunHash, false);
    }
  }
}
