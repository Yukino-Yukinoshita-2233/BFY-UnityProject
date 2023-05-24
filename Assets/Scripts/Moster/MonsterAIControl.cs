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
    [SerializeField]
    public float Monster_HP = 100;
    Transform Player_Transform, Monster_Transform;
    Animator Monster_animator;
    CharacterController Monster_CharacterController;
    public event HPChangeHandler OnHPChange;

    int Monster_isWalk;
    int Monster_isRun;
    int Monster_isAttack01;
    int Monster_isAttack02;
    int Monster_isGetHit;
    int Monster_isDie;


    void Start()
    {
        Monster_HP = 100;
        MonsterHpBar monsterHpBar = GetComponent<MonsterHpBar>();
        Player_Transform = GameObject.Find("Player").GetComponent<Transform>();
        Monster_Transform = GetComponent<Transform>();
        Monster_animator = GetComponent<Animator>();
        Monster_CharacterController = GetComponent<CharacterController>();
        GetAnimatorHash();
        monsterHpBar.GetMonsterHP(Monster_HP);
    }
    void GetAnimatorHash()
    {
        Monster_isWalk = Animator.StringToHash("isWalk");
        Monster_isRun = Animator.StringToHash("isRun");
        Monster_isAttack01 = Animator.StringToHash("isAttack01");
        Monster_isAttack02 = Animator.StringToHash("isAttack02");
        Monster_isGetHit = Animator.StringToHash("isGetHit");
        Monster_isDie = Animator.StringToHash("isDie");
    }

    void Update()
    {
        
        UpdateState();
    }

    void Monster_WalkState()
    {
        Debug.Log("Monster_WalkState");

        Vector3 dir = new Vector3(0, 0, 1);
        Monster_CharacterController.Move(transform.rotation * dir * Monster_WalkSpeed * Time.deltaTime);

        Monster_animator.SetBool(Monster_isWalk, true);

    }
    void Monster_RunState()
    {
        Debug.Log("Monster_RunState");

        Vector3 dir = new Vector3(0, 0, 1);
        Monster_CharacterController.Move(transform.rotation * dir * Monster_RunSpeed * Time.deltaTime);

        Monster_animator.SetBool(Monster_isRun, true);

    }
    void Monster_Attack01()
    {
        Debug.Log("Monster_Attack01");

        Monster_animator.SetTrigger(Monster_isAttack01);

    }
    void Monster_Attack02()
    {
        Debug.Log("Monster_Attack02");

        Monster_animator.SetTrigger(Monster_isAttack02);

    }
    void Monster_GetHitState()
    {
        Debug.Log("Monster_GetHitState");

        Monster_animator.SetTrigger(Monster_isGetHit);

    }
    void Monster_DieState()
    {
        Debug.Log("Monster_DieState");

        Monster_animator.SetTrigger(Monster_isDie);
        //DestroyObject(this);
    }
    void UpdateState()
    {

        if((Monster_Transform.position - Player_Transform.position).magnitude < (Vector3.one.magnitude)*2)
        {
            Timer = 0;
            Timer += Time.deltaTime;
            if (Timer>2)
            {
                int x = Random.Range(0, 3);
                if (x == 1)
                    Monster_Attack01();
                else
                    Monster_Attack02();
                Timer = 0;
            }
            if(Input.GetMouseButtonDown(0))
            {
                Monster_HP -= 10;
            }
        }
        else
        {

        }

        if(Monster_HP==0)
        {
            Monster_DieState();
        }
    }

    public delegate void HPChangeHandler(float newHP);
    public float HP
    {
        get { return Monster_HP; }
        set { OnHPChange?.Invoke(Monster_HP); }
    }
}

