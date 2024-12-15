using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_MainCanvasScript : MonoBehaviour
{
  [SerializeField]
  private Stage1Scene_JoystickControllerScript joystick; // 仮想スティック
  [SerializeField]
  private Transform cameraTransform;
  [SerializeField]
  private GameObject bigTequila;
  [SerializeField]
  private GameObject gameOverCanvas;
  [SerializeField]
  private Transform playerTransform;
  [SerializeField]
  public GameObject shinya;
  private SpriteRenderer shinyaSpriteRenderer;
  private Color shinyaRedColor = new Color(1, 0, 0, 1);
  private Color shinyaNormalColor = new Color(1, 1, 1, 1);
  [SerializeField]
  private Transform uiTransform;

  private float speed = 2f; // 移動速度
  private float inputThreshold = 0.1f;
  private float rotationSpeed = 5f;    // 角度変更のスムーズさ
  private float currentAngle = 0f;

  private float timeRemaining = 180f;
  // private float timeRemaining = 5f;

  // ブル生成間隔
  public float bullInterval = 5f;
  // 内部タイマー
  private float timer = 0f;

  [SerializeField]
  private TMP_Text timerText;
  public bool isRunning = true;
  public bool isDead = false;

  private int hp = 100;
  [SerializeField]
  private TMP_Text hpValueText;
  [SerializeField]
  private Slider hpSlider;
  private bool isInvincible = false;

  private int exp = 0;
  private int levelValue = 0;
  [SerializeField]
  private TMP_Text levelValueText;

  private int expMax = 2;

  [SerializeField]
  private Slider expSlider;

  [SerializeField]
  public GameObject tequilasParent;

  public GameObject selectCanvas;

  [SerializeField]
  private GameObject cigarette;
  public int cigaretteLevel = 0;
  public int cigaretteDamege = 0;
  public int cigaretteBullCount = 0;

  [SerializeField]
  public GameObject bull;
  [SerializeField]
  public GameObject bullsParent;
  public int bullCount = 0;
  public float bullSpeed = 0.2f;

  public int tequilaCount = 0;

  public int butamanCount = 0;

  public int bellCount = 0;
  public int bellTequilaCount = 0;

  [SerializeField]
  private AudioSource dartlikeUpAudio;
  [SerializeField]
  private AudioSource dartlikeDownAudio;
  [SerializeField]
  private AudioSource bgm;

  void Awake()
  {
    shinyaSpriteRenderer = shinya.GetComponent<SpriteRenderer>();
  }

  void Start()
  {
    hpValueText.text = hp.ToString();
    hpSlider.maxValue = hp;
    hpSlider.value = hp;

    cigarette.SetActive(false);

    LevelUp();
  }

  void Update()
  {
    if (isDead)
    {
      return;
    }
    if (!isRunning)
    {
      return;
    }
    // タイマーを加算
    var deltaTime = Time.deltaTime;
    timer += deltaTime;

    if (timeRemaining > 0)
    {
      timeRemaining -= deltaTime;
      UpdateTimerDisplay();
    }
    else
    {
      timeRemaining = 0;
      UpdateTimerDisplay();
      Dead();
      bigTequila.SetActive(true);
      bigTequila.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, bigTequila.transform.position.z);
      return;
    }

    // 仮想スティックの入力値を取得
    float horizontal = joystick.Horizontal();
    float vertical = joystick.Vertical();

    // 入力ベクトルを作成（Y軸の値も含む）
    Vector3 direction = new Vector3(horizontal, vertical, 0); // Z軸方向をY軸に置き換える場合もあり

    // ベクトルの大きさが閾値以上なら移動
    if (direction.magnitude >= inputThreshold)
    {
      direction.Normalize(); // ベクトルを正規化
      playerTransform.transform.Translate(direction * speed * Time.deltaTime);

      // 角度計算
      float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      targetAngle += 90f; // 右回りに90度加算して調整
      // 角度をスムーズに変更
      currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
      shinya.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    // カメラをプレイヤーの位置に追従させる
    if (cameraTransform != null)
    {
      cameraTransform.position = new Vector3(playerTransform.transform.position.x, playerTransform.transform.position.y, cameraTransform.position.z);

      uiTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, uiTransform.position.z);
      uiTransform.rotation = cameraTransform.rotation;

      gameOverCanvas.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, gameOverCanvas.transform.position.z);
      gameOverCanvas.transform.rotation = cameraTransform.rotation;

      selectCanvas.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, selectCanvas.transform.position.z);
      selectCanvas.transform.rotation = cameraTransform.rotation;
    }


    if (timer >= bullInterval)
    {
      SpawnRandomBull();
      timer = 0f; // タイマーをリセット
    }

  }

  void SpawnRandomBull()
  {
    for (int i = 0; i < bellCount + 1; i++)
    {
      Vector2 randomPoint = Random.insideUnitCircle * 7f;
      Vector3 spawnPosition = new Vector3(
        randomPoint.x + shinya.transform.position.x,
        randomPoint.y + shinya.transform.position.y,
        bull.transform.position.z
      );

      var newBull = Instantiate(bull, spawnPosition, shinya.transform.rotation, bullsParent.transform);
      newBull.SetActive(true);
    }
  }

  void UpdateTimerDisplay()
  {
    int minutes = Mathf.FloorToInt(timeRemaining / 60);
    int seconds = Mathf.FloorToInt(timeRemaining % 60);
    timerText.text = $"{minutes:D2}:{seconds:D2}";
  }

  public void Dead()
  {
    isRunning = false;
    isDead = true;
    hp = 0;
    hpValueText.text = hp.ToString();
    hpSlider.value = 0;
    shinyaSpriteRenderer.color = shinyaRedColor;

    bgm.Stop();
    gameOverCanvas.SetActive(true);
  }

  public void GetExp()
  {
    exp++;
    if (exp >= expMax)
    {
      LevelUp();
    }

    expSlider.value = exp;
  }

  public void GetCigarette()
  {
    dartlikeDownAudio.Play();

    cigarette.SetActive(true);

    cigaretteLevel++;
    var size = cigaretteLevel + 1;
    cigarette.transform.localScale = new Vector3(size, size, cigarette.transform.localScale.z);

    selectCanvas.SetActive(false);
    isRunning = true;
  }

  public void GetButaman()
  {
    dartlikeDownAudio.Play();

    butamanCount++;

    hp += 50;
    hpValueText.text = hp.ToString();
    if (hp > hpSlider.maxValue)
    {
      hpSlider.maxValue = hp;
    }
    hpSlider.value = hp;

    selectCanvas.SetActive(false);
    isRunning = true;
  }

  public void GetBell()
  {
    dartlikeDownAudio.Play();

    bellCount++;

    foreach (Transform childTransform in tequilasParent.transform)
    {
      var script = childTransform.gameObject.GetComponent<Stage1Scene_TequilaScript>();
      script.isBell = true;
    }
    bullInterval -= 0.2f;
    bullSpeed += 0.1f;

    selectCanvas.SetActive(false);
    isRunning = true;
  }

  public void GetTequila()
  {
    tequilaCount++;
    GetExp();

    hp -= 1;
    hpValueText.text = hp.ToString();
    hpSlider.value = hp;
    if (hp <= 0)
    {
      Dead();
    }

    StartCoroutine(DelayCoroutineGetTequila());
  }

  private IEnumerator DelayCoroutineGetTequila()
  {
    shinyaSpriteRenderer.color = shinyaRedColor;

    yield return new WaitForSeconds(0.1f);

    if (0 < hp)
    {
      shinyaSpriteRenderer.color = shinyaNormalColor;
    }
  }

  private void LevelUp()
  {
    dartlikeUpAudio.Play();
    exp = 0;
    expMax = (int)(expMax * 1.5f);
    expSlider.maxValue = expMax;
    expSlider.value = exp;
    levelValue++;
    levelValueText.text = levelValue.ToString();

    isRunning = false;
    selectCanvas.SetActive(true);
  }

  public void DamageFromBull()
  {
    if (isInvincible)
    {
      return;
    }

    StartCoroutine(DelayCoroutineDamageFromBull());
  }

  private IEnumerator DelayCoroutineDamageFromBull()
  {
    isInvincible = true;
    shinyaSpriteRenderer.color = shinyaRedColor;
    hp -= 1;
    hpValueText.text = hp.ToString();
    hpSlider.value = hp;
    if (hp <= 0)
    {
      Dead();
    }

    yield return new WaitForSeconds(0.1f);
    isInvincible = false;
    shinyaSpriteRenderer.color = shinyaNormalColor;
  }
}
