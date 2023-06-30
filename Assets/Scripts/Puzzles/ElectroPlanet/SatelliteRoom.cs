using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomState
{
    protected SatelliteRoom room;

    public RoomState(SatelliteRoom room)
    {
        this.room = room;
    }
}

public class RoomEmpty : RoomState
{
    DoorObject prevDoor => room.PrevDoorway.Door;
    TriggerZone enterTrigger => room.PrevDoorway.Trigger;

    public RoomEmpty(SatelliteRoom room) : base(room)
    {
        enterTrigger.OnEnter += Enter;
    }

    public void Enter(Collider arg)
    {
        prevDoor.Close();
        enterTrigger.OnEnter -= Enter;
        
        room.State = new RoomChoosingDoor(room);
    }
}

public class RoomChoosingDoor : RoomState
{
    ElectroPuzzle pazzle => room.Pazzle;

    public RoomChoosingDoor(SatelliteRoom room) : base(room)
    {
        room.OnTriggered += Choose;
    }

    public void Choose(BotDoorway doorway)
    {
        if (doorway.NextRoom != null)
        {
            room.ContinuePortal.GetComponent<Teleport>().enabled = false;
            room.PreviousPortal.GetComponent<Teleport>().enabled = false;

            SatelliteRoom nextRoom = doorway.NextRoom;
            if (pazzle.Enter(doorway.DoorType))
            {
                nextRoom = pazzle.FinishRoom;
            }

            doorway.Door.Open();
            nextRoom.PrevDoorway.Door.Open();

            nextRoom.PrevDoorway.SummonPortal(room.PreviousPortal);
            doorway.SummonPortal(room.ContinuePortal);

            room.OnTriggered -= Choose;
            room.State = new RoomLocked(room, nextRoom);

            room.ContinuePortal.GetComponent<Teleport>().enabled = true;
            room.PreviousPortal.GetComponent<Teleport>().enabled = true;
        }
    }
}

public class RoomLocked : RoomState
{
    SatelliteRoom next;
    IEnumerable<DoorObject> doors => room.Doors;

    public RoomLocked(SatelliteRoom room, SatelliteRoom next) : base(room)
    {
        foreach (var d in doors)
        {
            d.Lock();
        }

        this.next = next;
        next.OnTriggered += Exit;
    }

    public void Exit(BotDoorway arg)
    {
        foreach (var d in room.Doors)
        {
            d.Unlock();
            d.Close();
        }

        next.OnTriggered -= Exit;
        room.State = new RoomEmpty(room);
        
    }
}

public enum RoomStateID { Empty, Choosing, Locked };

public class SatelliteRoom : MonoBehaviour
{
    [SerializeField]
    ElectroPuzzle pazzle;
    public ElectroPuzzle Pazzle => pazzle;

    [SerializeField]
    List<BotDoorway> nextDoorway = new List<BotDoorway>();
    public List<BotDoorway> NextDoorway => nextDoorway;

    [SerializeField]
    BotDoorway prevDoorway;
    public BotDoorway PrevDoorway => prevDoorway;

    [SerializeField]
    Portal continuePortal, previousPortal;
    public Portal ContinuePortal => continuePortal;
    public Portal PreviousPortal => previousPortal;

    public delegate void ReactDoorway(BotDoorway doorway);
    public event ReactDoorway OnTriggered;

    [SerializeField]
    RoomStateID stateID;
    private RoomState state;
    public RoomState State 
    { 
        get => state;
        set
        {
            state = value;
            if (state is RoomEmpty) stateID = RoomStateID.Empty;
            else if (state is RoomChoosingDoor) stateID = RoomStateID.Choosing;
            else if (state is RoomLocked) stateID = RoomStateID.Locked;
        }
    }

    public IEnumerable<TriggerZone> Triggers
    {
        get
        {
            foreach (var d in NextDoorway)
            {
                yield return d.Trigger;
            }
            yield return PrevDoorway.Trigger;
        }
    }

    public IEnumerable<DoorObject> Doors
    {
        get
        {
            foreach (var d in NextDoorway)
            {
                yield return d.Door;
            }
            yield return PrevDoorway.Door;
        }
    }

    private void Awake()
    {
        foreach (var doorway in NextDoorway)
        {
            doorway.Trigger.OnEnter += arg => OnTriggered?.Invoke(doorway);
        }
        PrevDoorway.Trigger.OnEnter += arg => OnTriggered?.Invoke(PrevDoorway);

        switch (stateID)
        {
            case RoomStateID.Empty:
                State = new RoomEmpty(this);
                break;
            case RoomStateID.Choosing:
                State = new RoomChoosingDoor(this);
                break;
            default:
                State = null;
                break;
        }
    }

}