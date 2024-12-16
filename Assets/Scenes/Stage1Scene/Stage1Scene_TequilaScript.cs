using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_TequilaScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;
  private Transform shinyaTransform;

  public bool isBell = false;
  private Vector3 originalPosition;
  private float speed = 10f;

  private Stage1Scene_MainCanvasScript mainCanvasScript;
  private Transform selfTransform;

  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    selfTransform = transform;
    shinyaTransform = mainCanvasScript.shinya.transform;
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

    originalPosition = selfTransform.localPosition;
    if (isBell)
    {
      selfTransform.localPosition = Vector3.MoveTowards(selfTransform.localPosition, new Vector3(
        shinyaTransform.localPosition.x,
        shinyaTransform.localPosition.y,
        originalPosition.z
      ), Time.deltaTime * speed);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "shinya")
    {
      if (isBell)
      {
        mainCanvasScript.bellTequilaCount++;
      }
      mainCanvasScript.GetTequila();
      Destroy(gameObject);
    }
  }
}
