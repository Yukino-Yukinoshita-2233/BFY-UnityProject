using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;

public class MonsterLevel : MonoBehaviour
{
  public GameObject BOSS;
  public float scaleFactor = 1.5f; // 缩放比例
  public float radius = 3.5f; // 怪物为中心的半径怪物合成
  public Level level { get; set; } = Level.Small;
  public BUFF buff { get; set; }

  private void Start()
  {
    if (gameObject.name.ToLower().IndexOf("grunt") > -1)
    {
      buff = BUFF.Grass;
    }
    else if (gameObject.name.ToLower().IndexOf("goblinnecromain") > -1)
    {
      buff = BUFF.Fire;
    }
    else if (gameObject.name.ToLower().IndexOf("goblinhuntermain") > -1)
    {
      buff = BUFF.Water;
    }
    else
    {
      buff = GetComponent<InventoryUI>().CBuff;
    }

  }

  private void Update()
  {
    Mix();
  }

  private void Mix()
  {
    // 获取所有怪物
    GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
    List<GameObject> monsterList = new List<GameObject>();
    // 把在合成范围里面的怪物放入列表
    foreach (var item in monster)
    {
      if (gameObject.name != item.name)
      {
        float distance = Vector3.Distance(transform.position, item.transform.position);
        if (distance <= radius)
        {
          Debug.Log($"{gameObject.name} and {item.name} distance: {distance} radius: {radius}");
          monsterList.Add(item);
          break;
        }
      }
    }

    foreach (var item in monsterList)
    {
      if (item != null)
      {
        var temp_level = item.GetComponent<MonsterLevel>().level;
        if (level == Level.Small && level == temp_level)
        {
          var monsterbuff = item.GetComponent<MonsterLevel>().buff;
          Debug.Log($"{item.name} buff: {monsterbuff}");
          switch (monsterbuff)
          {
            case BUFF.Fire:
              if (buff == BUFF.Fire)
              {
                // 怪物平A伤害增加
                gameObject.GetComponent<MonsterAIControl>().hurt += 10f;
                level = Level.Mid;
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }
              else if (buff == BUFF.Grass)
              {
                //草 怪物被消灭
                Debug.Log("Destroy: " + gameObject.name);
                DestroyImmediate(gameObject);
              }
              else if (buff == BUFF.Water)
              {
                //火 怪物被消灭
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }

              break;

            case BUFF.Grass:
              if (buff == BUFF.Fire)
              {
                //草 怪物被消灭
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }
              else if (buff == BUFF.Grass)
              {
                // 草 怪物体积增大
                transform.localScale *= scaleFactor;
                level = Level.Mid;
                Debug.Log("scaleFactor: " + scaleFactor);
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }
              else if (buff == BUFF.Water)
              {
                //水 怪物被消灭
                Debug.Log("Destroy: " + gameObject.name);
                DestroyImmediate(gameObject);
              }
              break;

            case BUFF.Water:
              if (buff == BUFF.Fire)
              {
                //火 怪物被消灭
                Debug.Log("Destroy: " + gameObject.name);
                DestroyImmediate(gameObject);
              }
              else if (buff == BUFF.Grass)
              {
                //水 怪物被消灭
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }
              else if (buff == BUFF.Water)
              {
                //水 怪物的生命力提升
                gameObject.GetComponent<MonsterHp>().MonsterHP *= 2;
                level = Level.Mid;
                Debug.Log("Destroy: " + item.name);
                DestroyImmediate(item);
              }
              break;

            default:
              break;
          }
        }

        if (level == Level.Mid && temp_level == Level.Mid)
        {
          // 生成BOSS
          if (BOSS != null)
          {
            Instantiate(BOSS, transform.position, transform.rotation);
            DestroyImmediate(gameObject);
            DestroyImmediate(item);
          }
        }

      }
    }
  }
}