using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour
{
    public Transform Monster;
    public Transform MonsterHp;
    private Transform mainCameraTransform;

        // ��Ѫ��ʼ�����������
    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ�����е� Main Camera ����� Transform ���
        mainCameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {

        Follow();
    }
    //����
    void Follow()
    {
        //MonsterHp.transform.position = Camera.main.WorldToScreenPoint(Monster.transform.position + Vector3.up);
        MonsterHp.transform.position = Monster.transform.position+ Vector3.up * 3;
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,mainCameraTransform.rotation * Vector3.up);


    }
}
