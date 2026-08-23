using Coven.AIA.Core.Utilities;
using Coven.MSA.UI;
using UnityEngine;

public class RoleSelector : GameScreen
{
    [SerializeField] CovenButton roleButtonPrefab;
    [SerializeField] Transform roleButtonParent;

    protected override void Awake()
    {
        base.Awake();

        Set();
    }

    void Set()
    {
        foreach(PlayerRole role in System.Enum.GetValues(typeof(PlayerRole)))
        {
            CovenButton button = Instantiate(roleButtonPrefab, roleButtonParent);

            string roleName = TextUtilities.SplitName(role.ToString());
            button.SetText(roleName);

            PlayerRole selectedRole = role;

            button.onClick.AddListener(() => SelectRole(selectedRole));
        }
    }

    void SelectRole(PlayerRole role)
    {
        AppController.instance.SelectRole(role);
    }
}
