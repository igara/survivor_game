using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_MemoCloseButtonScript : MonoBehaviour
{
  [SerializeField] private GameObject memoCanvas;

  public void OnMouseDownMemoClose()
  {
    print("Memo close button clicked");
    memoCanvas.SetActive(false);
  }
}
