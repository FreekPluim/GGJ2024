using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JuggleBehaviour : MonoBehaviour
{
    [Header("Juggle Variables")]
    public GameObject JuggleUI;
    public Transform JuggleKeysUI_Fast;
    public int missedInputs;
    public GameObject Z, X, C;
    public Dictionary<KeyCode, GameObject> KeysGameobjects = new Dictionary<KeyCode, GameObject>();
    public JuggleSO jug;
    public PuzzleHolder holder;

    public TextMeshProUGUI timerUI;
    public int maxTime;
    int time;
    Coroutine TimerCo;

    private void OnEnable()
    {
        PlayerMovement.instance.anim.SetInteger("miniGame", 2);
        JuggleUI.SetActive(true);
        JuggleSO jug = GetComponent<PuzzleHolder>().puzzle as JuggleSO;
        JuggleSetup(jug);
        SetupJuggleQuick(jug);
        PuzzleManager.instance.puzzleFinish.AddListener(OnFinish);
        time = maxTime;
        TimerCo = StartCoroutine(timerCo());

    }
    void Update()
    {
        jug.update(this, holder);
        timerUI.text = time.ToString();

        if(time <= 0)
        {
            PuzzleManager.instance.puzzleFinish.Invoke();
        }
    }
    void JuggleSetup(JuggleSO juggle)
    {
        if (KeysGameobjects.Count == 0)
        {
            KeysGameobjects.Add(KeyCode.X, X);
            KeysGameobjects.Add(KeyCode.Z, Z);
            KeysGameobjects.Add(KeyCode.C, C);
        }

        juggle.SetInputs();
    }

    void OnFinish()
    {
        StopCoroutine(TimerCo);
        JuggleUI.SetActive(false);
        PuzzleManager.instance.puzzleFinish.RemoveListener(OnFinish);
    }

    void SetupJuggleQuick(JuggleSO juggle)
    {
        foreach (var item in juggle.keyInputs)
        {
            Instantiate(KeysGameobjects[item], JuggleKeysUI_Fast);
        }
    }

    IEnumerator timerCo()
    {
        for (int i = 0; i < maxTime; i++)
        {
            time--;
            yield return new WaitForSeconds(1);
        }
    }

}
