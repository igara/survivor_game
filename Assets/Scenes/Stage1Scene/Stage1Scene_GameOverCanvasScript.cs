using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_GameOverCanvasScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;
  private Stage1Scene_MainCanvasScript mainCanvasScript;

  [SerializeField]
  private TMP_Text cigaretteText;

  [SerializeField]
  private TMP_Text dartText;

  [SerializeField]
  private TMP_Text bullText;

  [SerializeField]
  private TMP_Text tequilaText;

  [SerializeField]
  private TMP_Text bellText;

  [SerializeField]
  private TMP_Text butamanText;


  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
  }
  private void OnEnable()
  {
    cigaretteText.text = $@"取得数: {mainCanvasScript.cigaretteLevel.ToString()}
ダメージ数: {mainCanvasScript.cigaretteDamege.ToString()}
ブル数: {mainCanvasScript.cigaretteBullCount.ToString()}";
    // dartText.text = mainCanvasScript.bellCount.ToString();
    bullText.text = $@"撃破数: {mainCanvasScript.bullCount.ToString()}
インブル数: {mainCanvasScript.inBullCount.ToString()}";
    tequilaText.text = $@"取得数: {mainCanvasScript.tequilaCount.ToString()}";
    bellText.text = $@"取得数: {mainCanvasScript.bellCount.ToString()}
ご褒美取得数: {mainCanvasScript.bellTequilaCount.ToString()}";
    butamanText.text = $@"取得数: {mainCanvasScript.butamanCount.ToString()}";
  }
}
