using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Game.Constants;

public class InventoryUI : MonoBehaviour
{
  bool sword;
  bool bow;
  bool watchPotion;
  bool firePotion;
  bool grassPotion;
  bool key;

  GameObject swordImage;
  GameObject keyImage;
  Dictionary<BUFF, GameObject> objectDictionary = new Dictionary<BUFF, GameObject>();
  int AttributeNum;

  public BUFF CBuff = BUFF.Water;
  BUFF oldCBuff;

  private RawImage image; // 图片组件
  Color Color1 = new Color(1, 1, 1, 1); // 目标透明度值
  Color Color2 = new Color(1, 1, 1, 0.3f); // 目标透明度值
  void Start()
  {
    initialize();
  }


  void Update()
  {
    CurrentBuff();//获取输入
    ChangeAttribute();

  }

  //初始化
  void initialize()
  {
    sword = false;
    bow = false;
    watchPotion = false;
    firePotion = false;
    grassPotion = false;
    key = false;

    objectDictionary[BUFF.Water] = GameObject.Find("Water");
    objectDictionary[BUFF.Fire] = GameObject.Find("Fire");
    objectDictionary[BUFF.Grass] = GameObject.Find("Grass");
    ChangeAlphaValue(objectDictionary[BUFF.Water], false);
    ChangeAlphaValue(objectDictionary[BUFF.Fire], false);
    ChangeAlphaValue(objectDictionary[BUFF.Grass], false);

    swordImage = GameObject.Find("Sword");
    keyImage = GameObject.Find("Key");
    ChangeAlphaValue(keyImage, false);

    AttributeNum = 0;
  }
  //修改透明度
  void ChangeAlphaValue(GameObject Image, bool isSelect)
  {
    image = Image.GetComponent<RawImage>();
    if (isSelect)
      image.color = Color1;//100%透明度
    else
      image.color = Color2;//30%透明度

  }

  void CurrentBuff()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      oldCBuff = CBuff;
      CBuff = BUFF.Water;
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      oldCBuff = CBuff;
      CBuff = BUFF.Fire;
    }
    else if (Input.GetKeyDown(KeyCode.Alpha3))
    {
      oldCBuff = CBuff;
      CBuff = BUFF.Grass;
    }
  }
  //切换属性
  void ChangeAttribute()
  {
    switch (CBuff)
    {
      case BUFF.Water:
        ChangeAlphaValue(objectDictionary[oldCBuff], false);
        ChangeAlphaValue(objectDictionary[CBuff], true);
        break;
      case BUFF.Fire:
        ChangeAlphaValue(objectDictionary[oldCBuff], false);
        ChangeAlphaValue(objectDictionary[CBuff], true);
        break;
      case BUFF.Grass:
        ChangeAlphaValue(objectDictionary[oldCBuff], false);
        ChangeAlphaValue(objectDictionary[CBuff], true);
        break;
    }
  }
}
