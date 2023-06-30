using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    [SerializeField]
    float meltSpeed, meltDeath;

    public bool IsMelting;
    public bool IsFullyMelted { private set; get; } = false;

    private void FixedUpdate()
    {
        if (IsMelting && (transform.localScale.z > 0))
        {
            Vector3 scale = transform.localScale;
            scale.y -= meltSpeed * Time.deltaTime;
            scale.y = Mathf.Clamp(scale.y, 0, 1);
            transform.localScale = scale;

            Vector3 pos = transform.localPosition;
            pos.y -= meltSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, 0, 1);
            transform.localPosition = pos;

            if (transform.localScale.y <= meltDeath)
            {
                IsFullyMelted = true;
                Destroy(gameObject);
            }
        }
    }
}
