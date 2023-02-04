using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
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

    private GodObject currentGodHovered;

    void Awake()
    {
        Instance = this;
        IsGameOver = false;
        
        availableGods = Resources.LoadAll<God>("");
        foreach(var god in availableGods)
        {
            SpawnGod(god);
        }
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

    private float _TimeUpdate;

    void Update()
    {
        DT = Time.deltaTime * ScaleTime;
        _TimeUpdate += DT;
        if(_TimeUpdate >= 1.0f)
        {
            _TimeUpdate -= 1.0f;
            Gameplay.TimeController.Value++;
        }

        if(Input.GetKeyDown(KeyCode.Space) && currentGods.Count <= availableGods.Length - 1) 
            SpawnGod(availableGods[currentGods.Count]);

        if(Input.GetKeyDown(KeyCode.Escape) && !UI.PauseMenu.IsOpen)
        {
            OnPauseMenu?.Invoke();
        }

        ScaleTime = Mathf.Lerp(ScaleTime, Pause ? 0.0f : 1.0f, Time.unscaledDeltaTime * 5.0f);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null && hit.collider.CompareTag("God"))
        {
            currentGodHovered = hit.collider.GetComponentInParent<GodObject>();
            if(currentGodHovered) currentGodHovered.OnMouseOverGod();
        }
        else if(currentGodHovered != null)
        { 
            currentGodHovered.MouseExitGod();
            currentGodHovered = null;
        }
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
        var data = new GodData(god.Name, god.Description, god.Sprite, god.IconUI, god.AbillityType, god.Abillity, god.Skills);
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
