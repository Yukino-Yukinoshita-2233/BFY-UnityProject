using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  private Animator animator;
  private int attackHash;
  private bool isAttacking;
  private float hurt = 10f; // 平A伤害
  private bool haveMonster = false;
  private GameObject Monster;

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

        // 扣血
        if (haveMonster && Monster != null)
        {
          AttackMonster();
        }

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

          // 扣血
          if (haveMonster && Monster != null)
          {
            AttackMonster();
          }
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

  void AttackMonster()
  {
    var playerBuff = GetComponentInChildren<InventoryUI>().CBuff;
    Debug.Log("Player_BUFF: " + playerBuff);

    var monsterBuff = InventoryUI.BUFF.Fire;
    Debug.Log("Monster_BUFF: " + monsterBuff);

    var monsterHP = Monster.GetComponentInChildren<MonsterHpBar>().MonsterHP;

    switch (monsterBuff)
    {
      case InventoryUI.BUFF.Fire:
        if (playerBuff == InventoryUI.BUFF.Grass)
        {
          hurt -= monsterHP * 0.05f;
        }

        if (playerBuff == InventoryUI.BUFF.Water)
        {
          hurt += monsterHP * 0.06f;
        }

        break;

      case InventoryUI.BUFF.Grass:
        if (playerBuff == InventoryUI.BUFF.Fire)
        {
          hurt += monsterHP * 0.06f;
        }

        if (playerBuff == InventoryUI.BUFF.Water)
        {
          hurt -= monsterHP * 0.05f;
        }

        break;

      case InventoryUI.BUFF.Water:
        if (playerBuff == InventoryUI.BUFF.Fire)
        {
          hurt -= monsterHP * 0.05f;
        }

        if (playerBuff == InventoryUI.BUFF.Grass)
        {
          hurt += monsterHP * 0.06f;
        }

        break;
      default:
        break;
    }

    // 血量移除
    Monster.GetComponentInChildren<MonsterHpBar>().MonsterHP -= hurt;
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("Monster"))
    {
      Monster = other.gameObject;
      Debug.Log("OnCollisionEnter: " + Monster.name);
      haveMonster = true;
    }
  }

  private void OnCollisionExit(Collision other)
  {
    if (other.gameObject.CompareTag("Monster"))
    {
      Monster = null;
      Debug.Log("OnCollisionExit: " + other.gameObject.name);
      haveMonster = false;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Monster"))
    {
      Monster = other.gameObject;
      Debug.Log("OnTriggerEnter: " + Monster.name);
      haveMonster = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Monster"))
    {
      Monster = null;
      Debug.Log("OnTriggerExit: " + other.gameObject.name);
      haveMonster = false;
    }
  }
}
