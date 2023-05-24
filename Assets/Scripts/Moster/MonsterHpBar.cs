using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    Transform mainCameraTransform;
    Transform MonsterCanvasTranform;
    Slider Slider1;

    [SerializeField]
    public float MonsterHP = 100;
    MonsterAIControl monsterAIControl;


    // ��Ѫ��ʼ�����������
    void Start()
    {
        MonsterHP = 100;
        MonsterHP = GetComponent<MonsterAIControl>().Monster_HP;
        // ��ȡ�����е� Main Camera ����� Transform ���
        mainCameraTransform = Camera.main.transform;
        MonsterCanvasTranform = GameObject.Find("MosterCanvas").transform;
        Slider1 = GameObject.Find("MonsterHpBar").gameObject.GetComponent<Slider>();

    }

    void Update()
    {
        MonsterHP = GetComponent<MonsterAIControl>().Monster_HP;

        Follow();
        if (Slider1!=null)
        {
            Slider1.value = MonsterHP * 0.01f;
        }
        
    }
    //����
    void Follow()
    {
        MonsterCanvasTranform.LookAt(mainCameraTransform);
    }
    public void GetMonsterHP(float HP)
    {
        MonsterHP = HP;
    }
}
