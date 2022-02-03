using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillActivation : MonoBehaviour
{
    public UnityEvent OnUsingSkill;

    private float clickTime;
    private float minClickTime = 2.5f;
    public bool isClick;

    void Update()
    {
        if(isClick)
        {
            clickTime += Time.deltaTime;
        }
        else
        {
            clickTime = 0;
        }
    }

    public void ButtonDown()
    {
        isClick = true;
    }

    public void ButtonUp()
    {
        isClick = false;

        if (clickTime >= minClickTime)
        {
            OnUsingSkill.Invoke();
        }
    }
}