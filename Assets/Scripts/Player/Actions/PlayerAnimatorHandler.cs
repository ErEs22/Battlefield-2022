using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        InitializeObject();
    }

    void InitializeObject()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayTargetAnimation(string animationName, float crossTime)
    {
        anim.CrossFade(animationName, crossTime);
    }

    public void PlayTargetAnimation(string animationName)
    {
        anim.CrossFade(animationName, 0f);
    }

    public void UpdateAimMoveParameters(float v, float h)
    {
        anim.SetFloat("AimVertical", v);
        anim.SetFloat("AimHorizontal", h);
    }

    public void UpdateMoveParameters(float moveAmount)
    {
        anim.SetFloat("MoveAmount", moveAmount);
    }

    public void SetAnimatorTrigger(string parameterName)
    {
        anim.SetTrigger(parameterName);
    }

    public void SetAnimatorBool(string parameterName, bool value)
    {
        anim.SetBool(parameterName, value);
    }
}
