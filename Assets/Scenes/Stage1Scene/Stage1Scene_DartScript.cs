using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_DartScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;
  private Stage1Scene_MainCanvasScript mainCanvasScript;

  private float timeRemaining = 1f;

  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
  }

  void Update()
  {
    if (mainCanvasScript.isDead)
    {
      return;
    }
    if (!mainCanvasScript.isRunning)
    {
      return;
    }

    var deltaTime = Time.deltaTime;

    if (timeRemaining > 0)
    {
      timeRemaining -= deltaTime;

      transform.position += -transform.right * 8f * deltaTime;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
  }
}
