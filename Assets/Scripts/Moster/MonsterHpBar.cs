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
        // 让血条始终面向摄像机
    // Start is called before the first frame update
    void Start()
    {
        // 获取场景中的 Main Camera 对象的 Transform 组件
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
    //跟随
    void Follow()
    {
        //MonsterCanvasTranform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
        MonsterCanvasTranform.LookAt(mainCameraTransform);
    }
}
