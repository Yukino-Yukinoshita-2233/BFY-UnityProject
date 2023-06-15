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
    public float hurt { get; set; } = 10f; // 平A基础伤害
    private float attackDistance = 8f; // 平A距离
    private float attackRadius = 10f; // 平A范围
    float PatrolTimer = 0, RevolveTimer = 0, RevolveXTimer = 0, AttackTimer = 0;
    float Monster_RatationSpeed = 200f;
    float Monster_WalkSpeed = 1f;
    float Monster_RunSpeed = 2f;
    float Monster_Gravity = -0.98f;
    Vector3 Gravity;

    bool isRevolve = false;
    MonsterState nowMonsterState;
    MonsterPatrolState nowMonsterPatrolState;
    MonsterAttackState nowMonsterAttackState;
    [SerializeField]
    Transform Player_Transform, Monster_Transform;
    Animator Monster_animator;
    CharacterController Monster_CharacterController;

    void Start()
    {
        PatrolTimer = 0;
        RevolveTimer = 0;
        RevolveXTimer = 0;
        AttackTimer = 0;
        MonsterHpBar monsterHpBar = GetComponent<MonsterHpBar>();
        Player_Transform = GameObject.Find("Player").GetComponent<Transform>();
        Monster_Transform = GetComponent<Transform>();
        Monster_animator = GetComponent<Animator>();
        Monster_CharacterController = GetComponent<CharacterController>();
        //Monster_IdleState();
        nowMonsterState = MonsterState.Patrol;
        nowMonsterPatrolState = MonsterPatrolState.Patrol_Idem;
    }
    //
    void Update()
    {
        Gravity = new Vector3(0, Monster_Gravity, 0);
        Monster_CharacterController.Move(transform.rotation * Gravity * Time.deltaTime);

        UpdateState();
    }
    void UpdateState()
    {
        FindPlayer();
        switch (nowMonsterState)
        {
            case MonsterState.Patrol:
                Monster_RevolveState();
                Monster_Patrol();
                break;
            case MonsterState.Track:
                Monster_TrackState();
                break;
            case MonsterState.Attack:
                Monster_Attack();
                break;
            case MonsterState.Die:
                //Monster_DieState();
                break;
        }
    }

    void FindPlayer()
    {
        if ((Monster_Transform.position - Player_Transform.position).magnitude >= Vector3.one.magnitude * 2 && (Monster_Transform.position - Player_Transform.position).magnitude < Vector3.one.magnitude * 5 && nowMonsterState != MonsterState.Die)
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


    public void Monster_Patrol()
    {
        PatrolTimer += Time.deltaTime;

        if (PatrolTimer > Random.Range(5, 20))
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    nowMonsterPatrolState = MonsterPatrolState.Patrol_Idem;
                    break;
                case 2:
                    nowMonsterPatrolState = MonsterPatrolState.Patrol_Walk;
                    break;
                case 3:
                    nowMonsterPatrolState = MonsterPatrolState.Patrol_Run;
                    break;
            }
            PatrolTimer = 0;
        }
        switch (nowMonsterPatrolState)
        {
            case MonsterPatrolState.Patrol_Idem:
                Monster_IdleState();
                break;
            case MonsterPatrolState.Patrol_Walk:
                Monster_WalkState();
                break;
            case MonsterPatrolState.Patrol_Run:
                Monster_RunState();
                break;
        }

    }

    public void Monster_RevolveState()
    {
        RevolveTimer += Time.deltaTime;
        if (RevolveTimer > Random.Range(10, 20))
        {
            isRevolve = true;
            RevolveTimer = 0;
        }
        if (isRevolve == true)
        {
            //Debug.Log("Monster_RevolveState");

            Quaternion rotate = Quaternion.Euler(0, Random.Range(-365, 366), 0);
            Monster_Transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * 1000);
            isRevolve = false;
        }

    }
    void Monster_IdleState()
    {
        //Debug.Log("Monster_IdleState");
        SetAnimationStates(true, false, false, false);
    }
    void Monster_WalkState()
    {
        //Debug.Log("Monster_WalkState");
        Vector3 dir = new Vector3(0, 0, Monster_WalkSpeed);
        Monster_CharacterController.Move(transform.rotation * dir * Time.deltaTime);
        SetAnimationStates(false, true, false, false);
    }
    void Monster_RunState()
    {
        //Debug.Log("Monster_RunState");
        Vector3 dir = new Vector3(0, 0, Monster_RunSpeed);
        Monster_CharacterController.Move(transform.rotation * dir * Time.deltaTime);
        SetAnimationStates(false, false, true, false);
    }
    void Monster_TrackState()
    {
        ////Debug.Log("Monster_TrackState");
        //Monster_Transform.LookAt(Player_Transform.position);
        Monster_Transform.LookAt(new Vector3(Player_Transform.position.x, Monster_Transform.position.y, Player_Transform.position.z));
        Vector3 dir = new Vector3(0, 0, Monster_RunSpeed);
        Monster_CharacterController.Move(transform.rotation * dir * Time.deltaTime);
        SetAnimationStates(false, false, false, true);
    }
    void Monster_Attack()
    {
        //Debug.Log("Monster_Attack");
        AttackTimer += Time.deltaTime;
        if (AttackTimer > 2)
        {
            int x = Random.Range(0, 3);
            if (x == 1)
            {
                nowMonsterAttackState = MonsterAttackState.Attack_Attack01;
                Monster_Attack01();
            }
            else
            {
                nowMonsterAttackState = MonsterAttackState.Attack_Attack02;
                Monster_Attack02();
            }
            // 获取角色
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.GetChild(1).GetChild(0).GetComponent<PlayerHpBar>().PlayerHp -= hurt;
            AttackTimer = 0;
        }
    }
    void Monster_Attack01()
    {
        //Debug.Log("Monster_Attack01");
        Monster_animator.SetTrigger("isAttack01");

    }
    void Monster_Attack02()
    {
        //Debug.Log("Monster_Attack02");
        Monster_animator.SetTrigger("isAttack02");

    }
    void Monster_GetHitState()
    {
        //Debug.Log("Monster_GetHitState");
        Monster_animator.SetTrigger("isGetHit");
    }
    /*
    void Monster_DieState()
    {
        //Debug.Log("Monster_DieState");
        Monster_animator.SetTrigger("isDie");
        if(Monster_animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            DestroyImmediate(gameObject);
            
        }
    }*/
    public void SetAnimationStates(bool isIdle, bool isWalk, bool isRun, bool isTrack)
    {
        Monster_animator.SetBool("isIdle", isIdle);
        Monster_animator.SetBool("isWalk", isWalk);
        Monster_animator.SetBool("isRun", isRun);
        Monster_animator.SetBool("isTrack", isTrack);
    }

}

