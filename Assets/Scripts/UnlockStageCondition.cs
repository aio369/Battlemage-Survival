using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStageCondition : MonoBehaviour
{
    [SerializeField] DataContainer container;
    [SerializeField] FlagsTable flagTable;
    [SerializeField] string unlockFlag = "10k Coins";

    private void OnEnable()
    {
        if (container.coins > 10000)
        {
            Flag flag = flagTable.GetFlag(unlockFlag);
            flag.state = true;
        }
    }
}
