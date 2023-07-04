using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFlower : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Animator animator;

    private void Awake()
    {
        animator.keepAnimatorControllerStateOnDisable = true;
    }
}
