using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Position : MonoBehaviour
{
    public GameObject Player;

    public void PositionVillage()
    {
        Player.transform.position = new Vector2(-263,94);
    }

    public void PositionFarm()
    {
        Player.transform.position = new Vector2(-165, 83);
    }

    public void PositionMapRandom1()
    {
        Player.transform.position = new Vector2(-222, 56);
    }

    public void PositionMapRandom2()
    {
        Player.transform.position = new Vector2(-145, 58);
    }
}
