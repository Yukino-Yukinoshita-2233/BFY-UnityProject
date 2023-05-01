using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  private GameObject player;
  private Vector3 offset;


  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
    offset = transform.position - player.transform.position;
  }

  private void LateUpdate()
  {
    transform.position = offset + player.transform.position;
    transform.LookAt(player.transform.position);
  }
}
