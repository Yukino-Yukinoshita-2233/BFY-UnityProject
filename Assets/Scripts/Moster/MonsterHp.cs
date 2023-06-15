using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    [SerializeField]
    Slider Slider1;
    Animator Monster_animator;
    public float MonsterHP { get; set; } = 100;

    void Start()
    {
        Slider1 = gameObject.GetComponentInChildren<Slider>();
        //Monster_animator = transform.parent.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {      

        if (MonsterHP <= 0)
        {
            Debug.Log("Monster_DieState");

            //Monster_animator.SetTrigger("isDie");
            //if (!Monster_animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            //{
            //    DestroyImmediate(gameObject);

            //}

            DestroyImmediate(gameObject);
        }

        if (Slider1 != null)
        {
            Slider1.value = MonsterHP * 0.01f;
        }
    }

}
