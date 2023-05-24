using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MonsterState
{
    Idem,
    Potrol_Wail,
    Potrol_Run,
    Track,
    Attack,
    Die,

}

public class MonsterAIControl : MonoBehaviour
{
    float Timer;
    float Monster_RatationSpeed = 200f;
    float Monster_WalkSpeed = 1f;
    float Monster_RunSpeed = 2f;
    int Monster_Life = 1;
    [SerializeField]
    public float Monster_HP = 100;
    Transform Player_Transform, Monster_Transform;
    Animator Monster_animator;
    CharacterController Monster_CharacterController;

    void Start()
    {
        Monster_HP = 100;
        MonsterHpBar monsterHpBar = GetComponent<MonsterHpBar>();
        Player_Transform = GameObject.Find("Player").GetComponent<Transform>();
        Monster_Transform = GetComponent<Transform>();
        Monster_animator = GetComponent<Animator>();
        Monster_CharacterController = GetComponent<CharacterController>();
        monsterHpBar.GetMonsterHP(Monster_HP);
        //Monster_IdleState();
    }

    void Update()
    {
        UpdateState();
    }
    public void Monster_IdleState()
    {
        Debug.Log("Monster_IdleState");
        Monster_animator.SetBool("isIdle", true);

    }
    public void Monster_WalkState()
    {
        Debug.Log("Monster_WalkState");
        Vector3 dir = new Vector3(0, 0, 1);
        Monster_CharacterController.Move(transform.rotation * dir * Monster_WalkSpeed * Time.deltaTime);
        Monster_animator.SetBool("isWalk", true);

    }
    void Monster_RunState()
    {
        Debug.Log("Monster_RunState");
        Vector3 dir = new Vector3(0, 0, 1);
        Monster_CharacterController.Move(transform.rotation * dir * Monster_RunSpeed * Time.deltaTime);
        Monster_animator.SetBool("isRun", true);

    }
    void Monster_Attack01()
    {
        Debug.Log("Monster_Attack01");
        Monster_animator.SetBool("isAttack01", true);

    }
    void Monster_Attack02()
    {
        Debug.Log("Monster_Attack02");

        Monster_animator.SetBool("isAttack02", true);

    }
    void Monster_GetHitState()
    {
        Debug.Log("Monster_GetHitState");
        Monster_animator.SetBool("isGetHit", true);

    }
    void Monster_DieState()
    {
        Debug.Log("Monster_DieState");
        Monster_animator.SetBool("isDie", true);
        float delay = 5.0f; // 延迟时间（以秒为单位）
        GameObject.Destroy(gameObject,delay);
    }
    void UpdateState()
    {
        if (Monster_HP <= 0 && Monster_Life == 1)
        {
            Monster_Life = 0;
            Monster_DieState();
        }
        else if ((Monster_Transform.position - Player_Transform.position).magnitude < Vector3.one.magnitude * 2 && Monster_Life == 1)
        {        
            //Debug.Log((Monster_Transform.position - Player_Transform.position).magnitude);

            Timer += Time.deltaTime;
            //Debug.Log(Timer);

            if (Timer>2)
            {
                Timer = 0;

                int x = Random.Range(0, 3);
                //Debug.Log(x);
                if (x == 1)
                    Monster_Attack01();
                else
                    Monster_Attack02();
                Timer = 0;
            }
            if(Input.GetMouseButtonDown(0))
            {
                Monster_GetHitState();
                Monster_HP -= 10;
            }
        }
        else
        {

        }

    }
}

