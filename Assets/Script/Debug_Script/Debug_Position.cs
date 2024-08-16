using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Position : MonoBehaviour
{
    public GameObject Player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionCity()
    {
        Player.transform.position = new Vector2(-391,90);

        
    }

    public void PositionVillage()
    {
        Player.transform.position = new Vector2(-263,94);

        
    }

    public void PositionFarm()
    {
        Player.transform.position = new Vector2(-97,91);

    }

    public void PositionMapRandom1()
    {
        Player.transform.position = new Vector2(-69, 19);

    }

    public void PositionMapRandom2()
    {
        Player.transform.position = new Vector2(-179, 0);

    }
}
