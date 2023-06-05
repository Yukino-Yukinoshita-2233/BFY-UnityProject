using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;

public class MonsterLevel : MonoBehaviour
{
  private Level level { get; set; } = Level.Small;
  private BUFF buff { get; set; }

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

  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("Monster"))
    {
      // Debug.Log($"OnCollisionEnter: {other.gameObject.name}");
      switch (other.gameObject.GetComponent<MonsterLevel>().level)
      {
        default:
          break;
      }
    }
  }

}
