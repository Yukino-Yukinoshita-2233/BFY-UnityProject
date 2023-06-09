using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
  public float PlayerHp { get; set; } = 100;
  Slider Slider1;
  void Start()
  {
    Slider1 = gameObject.GetComponent<Slider>();
    Slider1.value = PlayerHp / 100;
  }

  void Update()
  {
    Slider1.value = PlayerHp / 100;
  }
}
