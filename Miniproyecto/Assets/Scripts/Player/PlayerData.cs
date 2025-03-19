using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int lifes;

    public Vector2 checkPointposition;

    public PlayerData(int health, int bones)
    {
        this.lifes = lifes;
    }

    /*public PlayerData(int lifes, Vector2 checkPointposition) : this(lifes)
    {
        this.checkPointposition = checkPointposition;
    }*/
}
