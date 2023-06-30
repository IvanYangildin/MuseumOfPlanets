using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementPuzzle : MonoBehaviour
{
    [SerializeField]
    TriggerZone flowerTrigger, radioTrigger;
    [SerializeField]
    KeyItem flowerCheck, radioCheck;
    [SerializeField]
    DoorObject door;

    bool flowerArranged = false;
    bool radioArranged = false;

    void toArrange(Collider obj, bool isInput)
    {
        KeyItem item = obj.GetComponent<KeyItem>();
        if (item != null)
        {
            if (flowerCheck.IsRightKey(item)) flowerArranged = isInput;
            if (radioCheck.IsRightKey(item)) radioArranged = isInput;
        }
        if (flowerArranged && radioArranged) door.Unlock();
        else door.Lock();
    }

    private void Awake()
    {
        flowerTrigger.OnEnter += obj => toArrange(obj, true);
        flowerTrigger.OnExit += obj => toArrange(obj, false);

        radioTrigger.OnEnter += obj => toArrange(obj, true);
        radioTrigger.OnExit += obj => toArrange(obj, false);
    }
}
