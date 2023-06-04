using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
  [SerializeField]
  Slider Slider1;
  public float MonsterHP { get; set; } = 100;

  void Start()
  {
    Slider1 = gameObject.gameObject.GetComponent<Slider>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Slider1 != null)
    {
      Slider1.value = MonsterHP * 0.01f;
    }
  }
}
