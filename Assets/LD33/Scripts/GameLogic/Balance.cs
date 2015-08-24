using UnityEngine;

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
}
