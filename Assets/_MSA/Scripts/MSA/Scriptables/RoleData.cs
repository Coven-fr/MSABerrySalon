using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Coven/Data/Role")]
public class RoleData : ScriptableObject
{
    [SerializeField] PlayerRole role;
    public PlayerRole Role => role;

    [SerializeField] IntroData intro;
    public IntroData Intro => intro;

    [Space(10)]

    [SerializeField] List<ElementContent> elements;
    public List<ElementContent> Elements => elements;
}
