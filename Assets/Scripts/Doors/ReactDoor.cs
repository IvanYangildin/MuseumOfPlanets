using UnityEngine;
using UnityEngine.Events;

// connects simple door events with unity events
public class ReactDoor : MonoBehaviour
{
    [SerializeField]
    DoorObject door;

    public UnityEvent OnOpen = new UnityEvent();
    public UnityEvent OnClose = new UnityEvent();
    public UnityEvent OnOpening = new UnityEvent();
    public UnityEvent OnClosing = new UnityEvent();

    private void Awake()
    {
        door.OnOpen += () => { if (OnOpen != null) OnOpen.Invoke(); };
        door.OnClose += () => { if (OnClose != null) OnClose.Invoke(); };
        door.OnOpening += () => { if (OnOpening != null) OnOpening.Invoke(); };
        door.OnClosing += () => { if (OnClosing != null) OnClosing.Invoke(); };
    }
}