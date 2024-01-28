using System;

[Serializable]
public class GamepadRegister
{
    public bool[] PlayerRegistered = new bool[Enum.GetNames(typeof(PlayerIdentity)).Length];
    public int GetEmptyPlayerSpot()
    {
        var emptySlot = -1;
        for (int i = 0; i < PlayerRegistered.Length; i++)
        {
            if (!PlayerRegistered[i])
            {
                emptySlot = i;
                PlayerRegistered[i] = true;
                break;
            }
        }
        return emptySlot;
    }

    public bool CheckAllClear()
    {
        bool result = true;

        for (int i = 0; i < PlayerRegistered.Length; i++)
        {
            if (PlayerRegistered[i] == false)
            {
                result = false;
            }
        }
        return result;
    }
}
