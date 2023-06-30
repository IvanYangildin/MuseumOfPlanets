using UnityEngine;

public class BotLever : MonoBehaviour
{
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    float minAngle, maxAngle;
    float angle = 0;
    public float Angle => angle;

    delegate void stateFunc();
    stateFunc state;
    [SerializeField]
    InputBot InputBot;

    private void Awake()
    {
        state = normalizeState;
    }

    void plusState()
    {
        angle -= rotSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    void minusState()
    { 
        angle += rotSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    void normalizeState()
    {
        if (angle < 0)
        {
            angle += rotSpeed * Time.deltaTime;
            if (angle > 0) angle = 0;
        }
        if (angle > 0)
        {
            angle -= rotSpeed * Time.deltaTime;
            if (angle < 0) angle = 0;
        }
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    private void FixedUpdate()
    {
        state();
        transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public void ProcessRot(float rot)
    {
        if (rot == 0)
        {
            state = normalizeState;
        }
        else if (rot > 0)
        {
            state = plusState;
        }
        else
        { 
            state = minusState;
        }
    }

}