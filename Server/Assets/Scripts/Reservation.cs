using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reservation 
{
    int resDate;
    int courtNumber;
    int timeScale;
    bool available;

    public Reservation(int date, int num, int time)
    {
        resDate = date;
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
