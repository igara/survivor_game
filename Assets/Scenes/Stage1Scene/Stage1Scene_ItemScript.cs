using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1Scene_ItemScript : MonoBehaviour
{
  public bool isFirstOnCollisionEnter = false;

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (!isFirstOnCollisionEnter)
    {
      isFirstOnCollisionEnter = true;
    }
  }
}
