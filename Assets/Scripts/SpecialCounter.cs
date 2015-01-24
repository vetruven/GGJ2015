using UnityEngine;
using System.Collections;

public class SpecialCounter : MonoBehaviour
{
    int toSpecialCount = 5;
    int specialDurationCount = 5;
    private bool killSpecialTimer = false;

    public void StartSpecialCount()
    {
        killSpecialTimer = false;
        StartCoroutine(StartCountSpecialCreate());
    }

    public void StopSpecialCount()
    {
        killSpecialTimer = true;
    }

    private IEnumerator StartCountSpecialCreate()
    {
        Debug.Log("Start Count to special");
        int lastCount = toSpecialCount;

        while (lastCount >= 0 && !killSpecialTimer)
        {
            EventManager.OSFMsg(lastCount.ToString());
            Debug.Log("Special in " + lastCount);
            lastCount--;
            yield return new WaitForSeconds(1);
        }

        if (!killSpecialTimer)
        {
            Debug.Log("CreateSpecial");
            EventManager.SpecialCreate();
            StartCoroutine(StartCountSpecialKill());
        }
        else
            killSpecialTimer = false;
    }

    private IEnumerator StartCountSpecialKill()
    {
        Debug.Log("Start Special Life");
        int lastCount = specialDurationCount;

        while (lastCount >= 0 && !killSpecialTimer)
        {
            EventManager.OSFMsg(lastCount.ToString());
            Debug.Log("Special Life " + lastCount);
            lastCount--;
            yield return new WaitForSeconds(1);
        }

        if (!killSpecialTimer)
        {
            Debug.Log("Special Kill");
            EventManager.SpecialKill();
            StartCoroutine(StartCountSpecialCreate());
        }
        else
            killSpecialTimer = false;
    }
}
