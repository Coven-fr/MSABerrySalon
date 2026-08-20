using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intro", menuName = "Coven/Data/Intro")]
public class IntroData : ScriptableObject
{
    [TextArea(2, 10)]
    [SerializeField] List<string> messages;
    public List<string> Messages => messages;
}