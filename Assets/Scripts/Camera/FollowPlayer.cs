using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  private GameObject player;
  private CharacterController controller;
  private float mouseSensitivity = 150f;
  private float verticalRotation;
  private float horizontalRotation;
  private Vector3 offset;


  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    controller = player.GetComponent<CharacterController>();

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    offset = transform.position - player.transform.position;
  }

  private void LateUpdate()
  {
    // 鼠标控制视角旋转
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    verticalRotation -= mouseY;
    verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

    horizontalRotation += mouseX;
    horizontalRotation = Mathf.Clamp(horizontalRotation, -180f, 180f);

    Quaternion cameraRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    Quaternion playerRotation = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f);

    // transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    transform.rotation = playerRotation * cameraRotation;
    transform.position = player.transform.position + playerRotation * offset;

    player.transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
    controller.transform.rotation = Quaternion.Euler(0, horizontalRotation, 0);
  }
}
