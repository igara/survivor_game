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

  public bool isBell = false;
  private Vector3 originalPosition;
  private float speed = 10f;

  private Stage1Scene_MainCanvasScript mainCanvasScript;

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

    originalPosition = transform.position;
    if (isBell)
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(
        mainCanvasScript.shinya.transform.position.x,
        mainCanvasScript.shinya.transform.position.y,
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
