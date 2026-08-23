using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intro", menuName = "Coven/Data/Intro")]
public class IntroData : ScriptableObject
{
    [SerializeField] List<StepData> steps;
    public List<StepData> Steps => steps;
}