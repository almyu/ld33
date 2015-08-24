﻿using UnityEngine;

public class Balance : MonoSingleton<Balance> {
    public int AlarmDecreasingFactor;
    public int AlarmLevelDoorLight;
    public int AlarmLevelFlashight;
    public float FlashightDuration;
    public float SpottedPlayerAlarmFactor;
    public float DoorOpenedDuration;
    public float LevelDuration;
    public float TimeBetweenAlarms;
    public float WaitBeforeOpenDoor = 3f;
    public float WaitBeforeTurnOnFlashlight;
    public float SarumanFollowSpottedPlayerTime = 5.0f;
    public float DoorOpenerTimerFrom = 10.0f;
    public float DoorOpenerTimerTo = 25.0f;
}
