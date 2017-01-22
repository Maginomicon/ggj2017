using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int joyStick;
    public SelectCard selectCard;

    public Player(int joystick)
    {
        this.joyStick = joystick;
        selectCard = null;
    }
}
