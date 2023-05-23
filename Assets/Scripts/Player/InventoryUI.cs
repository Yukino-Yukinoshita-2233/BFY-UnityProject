using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    bool sword;
    bool bow;
    bool watchPotion;
    bool firePotion;
    bool grassPotion;
    bool key;

    GameObject swordImage;
    GameObject watchAttributeImage;
    GameObject fireAttributeImage;
    GameObject grassAttributeImage;
    GameObject keyImage;
    GameObject[] AttributeImage = new GameObject[3];

    int AttributeNum;

    private RawImage image; // 图片组件
    Color Color1 = new Color(1, 1, 1, 1); // 目标透明度值
    Color Color2 = new Color(1, 1, 1, 0.3f); // 目标透明度值
    void Start()
    {
        initialize();
    }


    void Update()
    {
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
        AttributeImage[0] = watchAttributeImage = GameObject.Find("Watch");
        AttributeImage[1] = fireAttributeImage = GameObject.Find("Fire");
        AttributeImage[2] = grassAttributeImage = GameObject.Find("Grass");

        for (int i = 0; i < 3; i++) 
        {
            ChangeAlphaValue(AttributeImage[i], false);
        }

        swordImage = GameObject.Find("Sword");
        keyImage = GameObject.Find("Key");
        ChangeAlphaValue(keyImage, false);

        AttributeNum = 0;
    }
    //修改透明度
    void ChangeAlphaValue(GameObject Image, bool isSelect)
    {
        image = Image.GetComponent<RawImage>();
        Debug.Log(image.color);

        if (isSelect)
            image.color = Color1;
        else
            image.color = Color2;
        Debug.Log(image.color);

    }
    //切换属性
    void ChangeAttribute()
    {
        if (Input.GetKeyDown(KeyCode.E) & AttributeNum <= 1)
        {
            ChangeAlphaValue(AttributeImage[AttributeNum], false);
            AttributeNum++;
            ChangeAlphaValue(AttributeImage[AttributeNum], true);
            Debug.Log(AttributeImage[AttributeNum].name + "ON");

        }
        else if (Input.GetKeyDown(KeyCode.E) & AttributeNum == 2)
        {
            ChangeAlphaValue(AttributeImage[AttributeNum], false);
            AttributeNum = 0;
            ChangeAlphaValue(AttributeImage[AttributeNum], true);
            Debug.Log(AttributeImage[AttributeNum].name + "ON");
        }





    }




}
