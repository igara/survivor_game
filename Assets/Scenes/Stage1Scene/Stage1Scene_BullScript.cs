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

  private float speed = 0.2f;

  private Vector3 originalPosition;

  private bool isShinya = false;
  private int hp = 10;
  private bool isCigaretteDamege = false;

  void Update()
  {
    var mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    if (mainCanvasScript.isDead)
    {
      return;
    }
    if (!mainCanvasScript.isRunning)
    {
      return;
    }

    originalPosition = transform.position;
    if (isShinya)
    {
      // 横方向にブルブル動かす
      float shakeOffset = Mathf.Sin(Time.time * frequency) * amplitude;
      transform.position = new Vector3(shakeOffset + originalPosition.x, 0 + originalPosition.y, originalPosition.z);
    }

    transform.position = Vector3.MoveTowards(transform.position, new Vector3(
      shinya.transform.position.x,
      shinya.transform.position.y,
      originalPosition.z
    ), Time.deltaTime * speed);
  }

  // トリガーエリアに接触し続けている間の処理
  private void OnTriggerStay2D(Collider2D collision)
  {
    var mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
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
    if (hp <= 0)
    {
      var newGameObject = Instantiate(tequila, transform.position, transform.rotation, tequilasParent.transform);
      newGameObject.SetActive(true);
      var mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();

      mainCanvasScript.bullCount++;

      StartCoroutine(DelayCoroutineBullDestroy());
    }
  }

  private IEnumerator DelayCoroutineBullDestroy()
  {
    gameObject.transform.localScale = new Vector3(0, 0, 0);
    var audioSource = gameObject.GetComponent<AudioSource>();

    // 0から9までのランダムな整数を生成
    int randomValue = Random.Range(0, 10);

    if (randomValue == 0) // 1/10の確率で実行
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
