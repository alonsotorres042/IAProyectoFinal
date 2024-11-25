using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FuzzyFunction
{
    public string Name;
    public AnimationCurve Functioncurves;
    public float F_y;
    public float SingletonDistance;
    public FuzzyFunction()
    {
    }
    public void Init()
    {
    }
    public float Evaluate(float x)
    {

        F_y = Mathf.Clamp01(Functioncurves.Evaluate(x));
        return F_y;
    }
}
[System.Serializable]
public class CalculateDiffuse
{
    public List<FuzzyFunction> FunctionsMember = new List<FuzzyFunction>();
    public CalculateDiffuse()
    {
    }
    public float CalculateFuzzy(float distance)
    {
        float SumaW = 0;
        float MultW = 0;
        for (int i = 0; i < FunctionsMember.Count; i++)
        {
            float y = FunctionsMember[i].Evaluate(distance);
            SumaW += y;
            MultW += y * FunctionsMember[i].SingletonDistance;
        }
        //CALCULO SALIDA
        return (SumaW != 0) ? MultW / SumaW : MultW;
    }
}
public class LogicDiffuse : MonoBehaviour
{
    public CalculateDiffuse SpeedDependDistancePoint = new CalculateDiffuse();
    public CalculateDiffuse SpeedDependDistanceObstacle = new CalculateDiffuse();

}