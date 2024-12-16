using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_BullScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;

  [SerializeField]
  public GameObject shinya;

  [SerializeField]
  private GameObject tequila;

  [SerializeField]
  private GameObject tequilasParent;

  [SerializeField]
  private AudioSource outerBullAudioSource;
  [SerializeField]
  private AudioSource inBullAudioSource;


  private float amplitude = 0.1f; // 振幅（動きの幅）
  private float frequency = 1000f; // 周波数（速さ）

  private Vector3 originalPosition;

  private bool isShinya = false;
  private int hp = 10;
  private bool isCigaretteDamege = false;

  private Stage1Scene_MainCanvasScript mainCanvasScript;
  private AudioSource audioSource;

  private Transform bullTransform;

  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    audioSource = gameObject.GetComponent<AudioSource>();
    bullTransform = transform;
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

    originalPosition = bullTransform.position;
    if (isShinya)
    {
      // 横方向にブルブル動かす
      float shakeOffset = Mathf.Sin(Time.time * frequency) * amplitude;
      bullTransform.position = new Vector3(shakeOffset + originalPosition.x, 0 + originalPosition.y, originalPosition.z);
    }

    bullTransform.position = Vector3.MoveTowards(bullTransform.position, new Vector3(
      shinya.transform.position.x,
      shinya.transform.position.y,
      originalPosition.z
    ), Time.deltaTime * mainCanvasScript.bullSpeed);
  }

  // トリガーエリアに接触し続けている間の処理
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (mainCanvasScript.isDead)
    {
      return;
    }
    if (!mainCanvasScript.isRunning)
    {
      return;
    }

    if (collision.gameObject.name == "shinya")
    {
      isShinya = true;
      mainCanvasScript.DamageFromBull();

      StartCoroutine(DelayCoroutineShinyaDamage());
    }
    if (collision.gameObject.name == "cigarette")
    {
      if (isCigaretteDamege)
      {
        return;
      }
      StartCoroutine(DelayCoroutineBullDamage(collision));
    }
  }

  private IEnumerator DelayCoroutineShinyaDamage()
  {
    yield return new WaitForSeconds(0.1f);
    isShinya = false;
  }

  private IEnumerator DelayCoroutineBullDamage(Collider2D collision)
  {
    isCigaretteDamege = true;
    yield return new WaitForSeconds(1);
    isCigaretteDamege = false;

    hp--;
    mainCanvasScript.cigaretteDamege++;
    if (hp <= 0)
    {
      var newGameObject = Instantiate(tequila, bullTransform.position, tequila.transform.rotation, tequilasParent.transform);
      newGameObject.SetActive(true);
      mainCanvasScript.bullCount++;
      mainCanvasScript.cigaretteBullCount++;

      // 0から9までのランダムな整数を生成
      bool isInBull = Random.Range(0, 10) == 10;
      if (isInBull)
      {
        mainCanvasScript.inBullCount++;
        mainCanvasScript.GetExp(50);
      }
      else
      {
        mainCanvasScript.GetExp();
      }

      StartCoroutine(DelayCoroutineBullDestroy(isInBull));
    }
  }

  private IEnumerator DelayCoroutineBullDestroy(bool isInBull)
  {
    bullTransform.localScale = new Vector3(0, 0, 0);

    if (isInBull)
    {
      audioSource.clip = inBullAudioSource.clip;
    }
    else
    {
      audioSource.clip = outerBullAudioSource.clip;
    }

    audioSource.Play();

    yield return new WaitForSeconds(1);
    Destroy(gameObject);
  }
}
