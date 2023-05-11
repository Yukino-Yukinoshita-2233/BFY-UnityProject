using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  private Animator animator;
  private int attackHash;
  private bool isAttacking;

  void Start()
  {
    animator = GetComponent<Animator>();
    attackHash = Animator.StringToHash("attack");
  }

  void Update()
  {
    this.GetAttack();
  }

  /// <summary>
  /// 获取攻击状态
  /// </summary>
  void GetAttack()
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

        //

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

          //
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
}
