using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    Transform mainCameraTransform;
    Transform MonsterCanvasTranform;
    Slider Slider1; 
    public float MonsterHP = 100;
        // ��Ѫ��ʼ�����������
    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ�����е� Main Camera ����� Transform ���
        mainCameraTransform = Camera.main.transform;
        MonsterCanvasTranform = GameObject.Find("MosterCanvas").transform;
        Slider1 = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        if(Slider1!=null)
        {
            Slider1.value = MonsterHP * 0.01f;
        }
        
    }
    //����
    void Follow()
    {
        //MonsterHp.transform.position = Camera.main.WorldToScreenPoint(Monster.transform.position + Vector3.up);
        MonsterCanvasTranform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }
}
