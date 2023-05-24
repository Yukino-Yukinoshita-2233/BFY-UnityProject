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

    public enum BUFF
    {
        Water = 0,
        Fire,
        Grass,
    }

    GameObject swordImage;
    GameObject keyImage;
    Dictionary<BUFF, GameObject> objectDictionary = new Dictionary<BUFF, GameObject>();
    int AttributeNum;

    public BUFF CBuff = BUFF.Water;
    BUFF oldCBuff;

    private RawImage image; // ͼƬ���
    Color Color1 = new Color(1, 1, 1, 1); // Ŀ��͸����ֵ
    Color Color2 = new Color(1, 1, 1, 0.3f); // Ŀ��͸����ֵ
    void Start()
    {
        initialize();
    }


    void Update()
    {
        CurrentBuff();//��ȡ����
        ChangeAttribute();

    }

    //��ʼ��
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
    //�޸�͸����
    void ChangeAlphaValue(GameObject Image, bool isSelect)
    {
        image = Image.GetComponent<RawImage>();
        if (isSelect)
            image.color = Color1;//100%͸����
        else
            image.color = Color2;//30%͸����

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
        Debug.Log("CBuff" + CBuff);
    }
    //�л�����
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
