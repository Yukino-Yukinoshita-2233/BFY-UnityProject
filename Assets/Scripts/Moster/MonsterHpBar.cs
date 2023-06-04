using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
  //GameObject Monster;
  Transform mainCameraTransform;
  Transform MonsterCanvasTranform;
  Transform MonsterHPBarTranform;

  [SerializeField]
  //Slider Slider1;
  //public float MonsterHP = 100;
  //MonsterAIControl monsterAIControl;


  // 让血条始终面向摄像机
  void Start()
  {
    //MonsterHP = 100;
    //Monster = transform.parent.parent.gameObject;
    //MonsterHP = Monster.GetComponent<MonsterAIControl>().Monster_HP;
    // 获取场景中的 Main Camera 对象的 Transform 组件
    mainCameraTransform = Camera.main.transform;
    //MonsterCanvasTranform = GameObject.Find("MosterCanvas").transform;
    MonsterHPBarTranform = gameObject.transform;
  }

  void Update()
  {
    //MonsterHP = Monster.GetComponent<MonsterAIControl>().Monster_HP;

    Follow();

  }
  //跟随
  void Follow()
  {
    //Debug.Log(Monster.name + ": " + "lookat");
    //MonsterCanvasTranform.LookAt(mainCameraTransform);
    MonsterHPBarTranform.LookAt(mainCameraTransform);
  }
  //public void GetMonsterHP(float HP)
  //{
  //    MonsterHP = HP;
  //}
}
