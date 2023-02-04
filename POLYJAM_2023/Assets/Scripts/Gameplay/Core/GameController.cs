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

    public bool IsGameOver { get; private set; }


    public static GameController Instance;

    void Awake()
    {
        Instance = this;
        IsGameOver = false;
        SpawnGod(startGod);
        availableGods = Resources.LoadAll<God>("");
        Debug.Log("availableGods: "+availableGods.Length);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentGods.Count <= availableGods.Length - 1) SpawnGod(availableGods[currentGods.Count]);
        }
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
