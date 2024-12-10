using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_MemoButtonScript : MonoBehaviour
{
  [SerializeField] private GameObject memoCanvas;

  public void OnMouseDownMemo()
  {
    if (memoCanvas.activeSelf) return;

    memoCanvas.SetActive(true);
  }
}
