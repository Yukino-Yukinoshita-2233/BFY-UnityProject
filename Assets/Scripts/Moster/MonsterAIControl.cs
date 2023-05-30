using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Patrol,
    Track,
    Attack,
    Die,
}
public enum MonsterPatrolState
{
    Patrol_Idem,
    Patrol_Walk,
    Patrol_Run,
}
public enum MonsterAttackState
{
    Attack_Attack01,
    Attack_Attack02,
}

public class MonsterAIControl : MonoBehaviour
{
    float Timer;
    float Monster_RatationSpeed = 200f;
    float Monster_WalkSpeed = 1f;
    float Monster_RunSpeed = 2f;
    int Monster_Life = 1;
    int StateNum;


    MonsterState nowMonsterState;
    MonsterPatrolState monsterPatrolState;
    MonsterAttackState monsterAttackState;
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
        //Monster_IdleState();
        nowMonsterState = MonsterState.Patrol;
    }

    void Update()
    {
        FindPlayer();
        SelectState();
    }
    void SelectState()
    {
        switch(nowMonsterState)
        {
            case MonsterState.Patrol:
                {
                    Timer += Time.deltaTime;

                    //if (Timer > Random.Range(5, 20))
                    //{
                        double Rota = Random.Range(-365, 366) * 0.05;
                        int Direction = Random.Range(-1, 2);

                    if(Direction==-1)
                        for (int i=0;i<Random.Range(0,365);i++)
                            Monster_Transform.eulerAngles += new Vector3(0, -1 * 1, 0);
                    else if(Direction == 1)
                        for (int i = 0; i < Random.Range(0, 365); i+= (int)Time.deltaTime)
                            Monster_Transform.eulerAngles += new Vector3(0, 1 * 1, 0);

                    //Timer = 0;

                    //}

                    if (Timer > Random.Range(5, 20))
                    {
                        switch (Random.Range(1, 4))
                        {
                            case 1:
                                Monster_IdleState();
                                break;
                            case 2:
                                Monster_WalkState();
                                break;
                            case 3:
                                Monster_RunState();
                                break;
                        }
                        Timer = 0;
                    }
                }
                break;
            case MonsterState.Track:
                {
                    Monster_TrackState();
                }
                break;
            case MonsterState.Attack:
                {
                    Timer += Time.deltaTime;
                    if (Timer > 2)
                    {
                        int x = Random.Range(0, 3);
                        //Debug.Log(x);
                        if (x == 1)
                        {
                            monsterAttackState = MonsterAttackState.Attack_Attack01;
                            Monster_Attack01();
                        }
                        else
                        {
                            monsterAttackState = MonsterAttackState.Attack_Attack02;
                            Monster_Attack02();
                        }
                        Timer = 0;
                    }

                }
                break;
            case MonsterState.Die:
                Monster_DieState();

                break;
        }
    }
    
    void UpdateState()
    {/*


        if (nowMonsterState == MonsterState.Die)
        {
            Monster_DieState();
        }
        if (nowMonsterState == MonsterState.Attack)
        {
            Timer += Time.deltaTime;
            if (Timer>2)
            {
                Timer = 0;

                int x = Random.Range(0, 3);
                //Debug.Log(x);
                if (x == 1)
                {
                    monsterAttackState = MonsterAttackState.Attack_Attack01;
                    Monster_Attack01();
                }
                else
                {
                    monsterAttackState = MonsterAttackState.Attack_Attack02;
                    Monster_Attack02();
                }
                Timer = 0;
            }
        }
        else if(nowMonsterState == MonsterState.Patrol)
        {
            Timer += Time.deltaTime;
            switch (Random.Range(1, 4))
            {
                case 1:
                    Monster_IdleState();
                    break;
                case 2:
                    Monster_WalkState();
                    break;
                case 3:
                    Monster_RunState();
                    break;
            }
            if (Timer > Random.Range(5, 30))
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                        Monster_IdleState();
                        break;
                    case 2:
                        Monster_WalkState();
                        break;
                    case 3:
                        Monster_RunState();
                        break;
                }

            }

        }
        else if(nowMonsterState == MonsterState.Track)
        {

        }
    */
    }
    
    void FindPlayer()
    {
        if ((Monster_Transform.position - Player_Transform.position).magnitude >= Vector3.one.magnitude * 2 && (Monster_Transform.position - Player_Transform.position).magnitude < Vector3.one.magnitude * 4 && nowMonsterState != MonsterState.Die)
        {
            nowMonsterState = MonsterState.Track;
        }
        else if ((Monster_Transform.position - Player_Transform.position).magnitude < Vector3.one.magnitude * 2 && nowMonsterState != MonsterState.Die)
        {
            nowMonsterState = MonsterState.Attack;
        }
        else
        {
            nowMonsterState = MonsterState.Patrol;
        }
    }
    public void Monster_TrackState()
    {
        Monster_Transform.LookAt(Player_Transform);
        Vector3 dir = new Vector3(0, 0, 1.50f);
        Monster_CharacterController.Move(transform.rotation * dir * Monster_RunSpeed * Time.deltaTime);

        Debug.Log("Monster_TrackState");

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
        Vector3 dir = new Vector3(0, 0, 1.50f);
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
        GameObject.Destroy(gameObject, delay);
    }

}

