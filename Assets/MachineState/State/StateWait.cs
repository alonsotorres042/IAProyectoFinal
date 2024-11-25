using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class StateWait : StateMove
{

    protected Coroutine coroutine;
    protected bool WaitTime = false;
    public float TimeWait;
    public float TimeWaitMax;
    public TMPro.TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {

        base.LoadComponent();

    }
    public void StartCoroutineWait()
    {
        //textMeshPro.gameObject.SetActive(true);

        coroutine = StartCoroutine(RunWait());
    }
    public override void Enter()
    {
        TimeWait = TimeWaitMax;
        base.Enter();
    }
    IEnumerator RunWait()
    {
        WaitTime = true;
        while (TimeWait > 0)
        {
            TimeWait--;
            if (textMeshPro != null)
                textMeshPro.text = TimeWait.ToString("00");
            yield return new WaitForSeconds(1f);
        }


        WaitTime = false;
        //StopCoroutine(coroutine);
        //coroutine=null;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null; // Limpia la referencia después de detenerlo
        }

    }
    public override void Execute()
    {

        base.Execute();
    }
    public override void Exit()
    {
        //textMeshPro.gameObject.SetActive(false);

        base.Exit();
    }
}
