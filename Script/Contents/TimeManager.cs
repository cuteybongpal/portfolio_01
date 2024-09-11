using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static Define;

public class TimeManager : MonoBehaviour 
{
    public int nightTime;
    public int dayTime;
    public int timeSpeed;
    public int currentHour;
    public int currentMinute;

    public timeState TimeState { 
        get
        {
            return TimeState;
        }
        set
        {
            TimeState = value;
            if (TimeState == timeState.day)
            {
                currentHour = dayTime;
                currentMinute = 0;
                StartCoroutine(timeFlow());
            }
        }
    }

    IEnumerator timeFlow()
    {
        while (true)
        {
            if (TimeState == timeState.night)
            {
                yield return new WaitForSeconds(timeSpeed);
                currentMinute += 1;
                if (currentMinute >= 60)
                {
                    currentMinute = 0;
                    currentHour += 1;
                    if (currentHour >= nightTime)
                    {
                        TimeState = timeState.night;
                    }
                }
            }
        }
    }

}
