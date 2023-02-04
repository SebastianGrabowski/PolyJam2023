using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private God startGod;
    [SerializeField] private GodObject godObject;
    [SerializeField] private Transform[] godParents;

    private God[] availableGods;
    private List<GodData> currentGods = new List<GodData>();

    public UnityAction OnGameOver { get; set; }
    public UnityAction OnPauseMenu { get; set; }
    public UnityAction OnHelp { get; set; }

    public bool IsGameOver { get; private set; }

    public static int RunCount;


    public static GameController Instance;

    public bool Pause;

    void Awake()
    {
        Instance = this;
        IsGameOver = false;
        SpawnGod(startGod);
        availableGods = Resources.LoadAll<God>("");
        Debug.Log("availableGods: "+availableGods.Length);

    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        if (RunCount == 0)
        {
            HandleHelp();
        }
        RunCount++;
    }

    public static float DT;
    public static float ScaleTime;

    void Update()
    {
        DT = Time.deltaTime * ScaleTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGod(availableGods[1]);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && !UI.PauseMenu.IsOpen)
        {
            OnPauseMenu?.Invoke();
        }

        ScaleTime = Mathf.Lerp(ScaleTime, Pause ? 0.0f : 1.0f, Time.unscaledDeltaTime * 5.0f);
    }
    
    public void HandleHelp()
    {
        OnHelp?.Invoke();
    }

    public void HandlePause()
    {
        OnPauseMenu?.Invoke();
    }

    private void SpawnGod(God god)
    {
        var newSkillLevels = (int[])god.SkillLevels.Clone();
        var data = new GodData(god.Name, god.Description, god.Damage, god.Range, god.Rate, newSkillLevels, god.Sprite, god.IconUI, god.AbillityType, god.Abillity);
        currentGods.Add(data);

        var obj = Instantiate(godObject, godParents[god.ID].position, Quaternion.identity);
        obj.SetGod(data);
    }

    public void HandleGameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }

    public void HandleReplay()
    {
        //reset all statics etc
        

        //reload scene, fade before????
        FindObjectOfType<Fade>().FadeOut(()=>{ 
            Gameplay.Units.Unit.AllUnits.Clear(); 
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
            });
        
    }

    public void HandleExit()
    {
        //fade before????
        FindObjectOfType<Fade>().FadeOut(()=>{ Application.Quit(); });
    }
}
