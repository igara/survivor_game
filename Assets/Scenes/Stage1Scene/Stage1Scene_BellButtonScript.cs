using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_BellButtonScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;

  public void OnMouseDownBell()
  {
    var script = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    script.GetBell();
  }
}
