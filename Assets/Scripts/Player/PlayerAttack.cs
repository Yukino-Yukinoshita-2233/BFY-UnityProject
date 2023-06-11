using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;

public class PlayerAttack : MonoBehaviour
{
  private Animator animator;
  private int attackHash;
  private bool isAttacking;
  private float preparehurt = 10f; // 平A基础伤害
  private float hurt = 0f; // 平A当前伤害
  private float hurtbuff = 0.06f; // 平A加成
  private float hurtdebuff = 0.05f; // 平A削减
  private float attackDistance = 3.5f; // 平A距离
  private float attackRadius = 8.5f; // 平A范围

  void Start()
  {
    animator = GetComponent<Animator>();
    attackHash = Animator.StringToHash("attack");
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      // 如果不在攻击状态中，则开始第一次攻击
      if (!isAttacking)
      {
        isAttacking = true;
        // 播放第一次攻击动画
        animator.SetInteger(attackHash, 1);

        // 攻击
        SelectMonster();

        // 启动协程，重置攻击定时器
        StartCoroutine(Timer());
      }
      else
      {
        // 如果在攻击状态中，则判断当前连击数是否达到2
        if (animator.GetInteger(attackHash) == 1)
        {
          // 播放第二次攻击动画
          animator.SetInteger(attackHash, 2);

          // 攻击
          SelectMonster();
        }
      }
    }
  }

  /// <summary>
  /// 重置攻击定时器
  /// </summary>
  /// <returns></returns>
  IEnumerator Timer()
  {
    yield return new WaitForSeconds(0.7f);

    isAttacking = false;

    // 重置攻击状态机参数
    animator.SetInteger(attackHash, 0);
  }

  /// <summary>
  /// 选择角色当前BUFF
  /// </summary>
  void SelectBuff(BUFF monsterBuff, float monsterHP)
  {
    var playerBuff = GetComponentInChildren<InventoryUI>().CBuff;
    Debug.Log("Player_BUFF: " + playerBuff);
    Debug.Log("Monster_BUFF: " + monsterBuff);

    switch (monsterBuff)
    {
      case BUFF.Fire:
        if (playerBuff == BUFF.Fire)
        {
          hurt = preparehurt;
        }
        else if (playerBuff == BUFF.Grass)
        {
          hurt = preparehurt - (monsterHP * hurtbuff);
        }
        else if (playerBuff == BUFF.Water)
        {
          hurt = preparehurt + (monsterHP * hurtdebuff);
        }

        break;

      case BUFF.Grass:
        if (playerBuff == BUFF.Fire)
        {
          hurt = preparehurt + (monsterHP * hurtdebuff);
        }
        else if (playerBuff == BUFF.Grass)
        {
          hurt = preparehurt;
        }
        else if (playerBuff == BUFF.Water)
        {
          hurt = preparehurt - (monsterHP * hurtbuff);
        }

        break;

      case BUFF.Water:
        if (playerBuff == BUFF.Fire)
        {
          hurt = preparehurt - (monsterHP * hurtbuff);
        }
        else if (playerBuff == BUFF.Grass)
        {
          hurt = preparehurt + (monsterHP * hurtdebuff);
        }
        else if (playerBuff == BUFF.Water)
        {
          hurt = preparehurt;
        }

        break;
      default:
        break;
    }
  }

  /// <summary>
  /// 选择当前可攻击的怪物
  /// </summary>
  private void SelectMonster()
  {
    // 获取所有怪物
    GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
    List<GameObject> monsterList = new List<GameObject>();
    // 把在攻击范围里面的怪物放入列表
    foreach (var item in monster)
    {
      float distance = Vector3.Distance(transform.position, item.transform.position);
      float angle = Vector3.Angle(transform.forward, item.transform.position - transform.position);
      if (distance <= attackDistance && angle <= attackRadius)
      {
        Debug.Log($"Player and {item.name} distance: {distance}, angle: {angle}, attackDistance: {attackDistance}, attackRadius:{attackRadius}");
        monsterList.Add(item);
      }
    }

    foreach (var item in monsterList)
    {
      // 添加BUFF
      var monsterHP = item.GetComponentInChildren<MonsterHp>().MonsterHP;
      var monsterBuff = item.GetComponentInChildren<MonsterLevel>().buff;
      SelectBuff(monsterBuff, monsterHP);

      // 血量移除
      item.GetComponentInChildren<MonsterHp>().MonsterHP -= hurt;

      Debug.Log($"Hurt {item.name}: {hurt}");
      Debug.Log($"{item.name} HP: {monsterHP}");
    }
  }
}
