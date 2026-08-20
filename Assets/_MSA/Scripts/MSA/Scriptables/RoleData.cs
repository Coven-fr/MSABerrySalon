using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "Coven/Data/Role")]
public class RoleData : ScriptableObject
{
    [SerializeField] IntroData intro;
    public IntroData Intro => intro;

    [SerializeField] string roleName;
    public string RoleName => roleName;

    [SerializeField] List<ElementContent> elements;
    public List<ElementContent> Elements => elements;
}
