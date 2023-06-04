using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHp : MonoBehaviour
{
    [SerializeField]
    GameObject Monster;
    Slider Slider1;
    public float MonsterHP = 100;
    MonsterAIControl monsterAIControl;

    // Start is called before the first frame update
    void Start()
    {
        MonsterHP = 100;
        Monster = transform.parent.parent.gameObject;
        MonsterHP = Monster.GetComponent<MonsterAIControl>().Monster_HP;

    }

    // Update is called once per frame
    void Update()
    {
        MonsterHP = Monster.GetComponent<MonsterAIControl>().Monster_HP;

        if (Slider1 != null)
        {
            Slider1.value = MonsterHP * 0.01f;
        }

    }
}
