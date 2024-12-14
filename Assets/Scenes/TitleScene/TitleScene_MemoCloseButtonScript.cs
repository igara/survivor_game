using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_MemoCloseButtonScript : MonoBehaviour
{
  [SerializeField]
  private GameObject memoCanvas;
  [SerializeField]
  private AudioSource dartlikeDownAudio;

  public void OnMouseDownMemoClose()
  {
    memoCanvas.SetActive(false);
    dartlikeDownAudio.Play();
  }
}
