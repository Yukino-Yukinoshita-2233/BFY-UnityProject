using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : MonoBehaviour
{
    public Transform Monster;
    public Transform MonsterHp;
    private Transform mainCameraTransform;

        // 让血条始终面向摄像机
    // Start is called before the first frame update
    void Start()
    {
        // 获取场景中的 Main Camera 对象的 Transform 组件
        mainCameraTransform = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {

        Follow();
    }
    //跟随
    void Follow()
    {
        //MonsterHp.transform.position = Camera.main.WorldToScreenPoint(Monster.transform.position + Vector3.up);
        MonsterHp.transform.position = Monster.transform.position+ Vector3.up * 3;
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,mainCameraTransform.rotation * Vector3.up);


    }
}
