using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Coven/Data/Role")]
public class RoleData : ScriptableObject
{
    [SerializeField] string displayName;
    public string DisplayName => displayName;

    [SerializeField] PlayerRole role;
    public PlayerRole Role => role;

    [SerializeField] IntroData intro;
    public IntroData Intro => intro;

    [Space(10)]

    [SerializeField] List<ElementContent> elements;
    public List<ElementContent> Elements => elements;

    [Space(10)]

    [TextArea(2, 10)]
    [SerializeField] string endText;
    public string EndText => endText;
}
