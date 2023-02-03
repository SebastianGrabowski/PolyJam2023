using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private God startGod;
    [SerializeField] private GodObject godObject;
    [SerializeField] private Transform[] godParents;

    private God[] availableGods;
    private List<GodData> currentGods = new List<GodData>();

    void Awake()
    {
        SpawnGod(startGod);
        availableGods = Resources.LoadAll<God>("");
        Debug.Log("availableGods: "+availableGods.Length);
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGod(availableGods[1]);
        }
    }

    private void SpawnGod(God god)
    {
        var data = new GodData(god.Name, god.Description, god.Damage, god.Range, god.Rate, god.Sprite, god.IconUI, god.AbillityType, god.Abillity);
        currentGods.Add(data);

        var obj = Instantiate(godObject, godParents[god.ID].position, Quaternion.identity);
        obj.SetGod(data);
    }
}
