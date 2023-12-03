using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservation 
{
    public int courtNumber;
    public int timeScale;
    public bool available;

    public Reservation(int num, int time)
    {
        courtNumber = num;
        timeScale = time;
        available = true;
    }

    public bool Reserve()
    {
        if (available)
        {
            available = false;
            return true;
        }
        else return false;
    }

    public bool Cancel()
    {
        if (!available)
        {
            available = true;
            return true;
        }
        else return false;
    }
}
