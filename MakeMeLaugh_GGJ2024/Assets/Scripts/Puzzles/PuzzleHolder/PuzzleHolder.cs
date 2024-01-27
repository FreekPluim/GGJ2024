using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PuzzleHolder : MonoBehaviour
{
    bool playerActive = false;

    public GameObject stageCamera;
    public GameObject puzzleCamera;
    public GameObject puzzleObject;
    public Puzzle puzzle;

    [Header("UI ELEMENTS")]
    public GameObject JuggleUI;
    public GameObject FartUI;

    [Header("Events")]
    public UnityEvent onJuggleFinish;
    public UnityEvent onFartFinish;
    public UnityEvent onBallanceFinish;

    [Header("Juggle Variables")]
    public OverlapChecker overlap;
    public Transform JuggleKeysUI_Fast, JuggleKeysUI_Timing;
    Coroutine juggleTimingQueue;
    public int missedInputs;
    public GameObject Z, X, C;
    public Dictionary<KeyCode, GameObject> KeysGameobjects = new Dictionary<KeyCode, GameObject>();

    [Header("Fart")]
    public Slider slider;
    public int current;
    private void Awake()
    {
        PuzzleManager.instance.puzzleFinish.AddListener(FinishPuzzle);
    }
    public void SetPuzzle(Puzzle puzzle)
    {
        this.puzzle = puzzle;
    }
    private void Update()
    {
        if(puzzle != null && puzzleObject.activeSelf == false)
        {
            puzzleObject.SetActive(true);

            if (puzzle is JuggleSO)
            {
                JuggleSetup(puzzle as JuggleSO);
            }
        }
        else if(puzzle == null && puzzleObject.activeSelf == true)
        {
            puzzleObject.SetActive(false);
        }

        if(puzzle != null && playerActive)
        {
            if(puzzle is JuggleSO)
            {
                JuggleUpdate(puzzle as JuggleSO);
            }
            if (puzzle is FartSO)
            {
                FartUpdate(puzzle as FartSO);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleCamera.SetActive(true);
            stageCamera.SetActive(false);
            StartCoroutine(TimerToStart());
        }
    }
    IEnumerator TimerToStart()
    {
        yield return new WaitForSeconds(1.5f);
        playerActive = true;
        
        if (puzzle is JuggleSO) JuggleStart();
        if (puzzle is FartSO) FartStart();
    }
    public void FinishPuzzle()
    {
        puzzle = null;
        playerActive = false;
        stageCamera.SetActive(true);
        puzzleCamera.SetActive(false);

        if (JuggleUI.activeSelf) JuggleUI.SetActive(false);
        if (FartUI.activeSelf) FartUI.SetActive(false);

        if(juggleTimingQueue != null) StopCoroutine(juggleTimingQueue);
        PuzzleManager.instance.puzzleFinish.RemoveListener(FinishPuzzle);
    }

    /// <summary>
    /// Juggle
    /// </summary>
    private void JuggleStart()
    {
        JuggleUI.SetActive(true);
        JuggleSO jug = puzzle as JuggleSO;
        if (!jug.TimingBased) SetupJuggleQuick(jug);
        if (jug.TimingBased) juggleTimingQueue = StartCoroutine(SetupJuggleTiming(jug));
    }
    void JuggleUpdate(JuggleSO juggle)
    {
        juggle.update(this);
    }
    IEnumerator SetupJuggleTiming(JuggleSO juggle)
    {
        foreach (var item in juggle.keyInputs)
        {
            Instantiate(KeysGameobjects[item], JuggleKeysUI_Timing);
            yield return new WaitForSeconds(1f);
        }
    }
    void JuggleSetup(JuggleSO juggle)
    {
        PuzzleManager.instance.puzzleFinish.AddListener(FinishPuzzle);

        if (KeysGameobjects.Count == 0)
        {
            KeysGameobjects.Add(KeyCode.X, X);
            KeysGameobjects.Add(KeyCode.Z, Z);
            KeysGameobjects.Add(KeyCode.C, C);
        }

        juggle.SetInputs();

    }
    void SetupJuggleQuick(JuggleSO juggle)
    {
        foreach (var item in juggle.keyInputs)
        {
            Instantiate(KeysGameobjects[item], JuggleKeysUI_Fast);
        }
    }

    /// <summary>
    /// Fart
    /// </summary>
    private void FartStart()
    {
        FartUI.SetActive(true);
    }
    private void FartUpdate(FartSO puzzle)
    {
        puzzle.update(this);
        slider.value = current;
        slider.maxValue = puzzle.max;

        if (current >= puzzle.max)
        {
            //TODO: Play reverb fart sound
            //TODO: Add points
            onFartFinish.Invoke();
            PuzzleManager.instance.puzzleFinish.Invoke();
        }
    }

    /// <summary>
    /// Ballance
    /// </summary>

}

