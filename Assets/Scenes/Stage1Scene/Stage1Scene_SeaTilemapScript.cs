using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_SeaTilemapScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;
  private Stage1Scene_MainCanvasScript mainCanvasScript;

  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "shinya")
    {
      mainCanvasScript.Dead();
    }
  }
}
