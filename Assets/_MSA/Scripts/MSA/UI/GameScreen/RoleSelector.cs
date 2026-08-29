using Coven.AIA.Core.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class RoleSelector : GameScreen
{
    [SerializeField] RoleButton roleButtonPrefab;
    [SerializeField] Transform roleButtonParent;

    public void Set(List<RoleData> roles)
    {
        foreach(RoleData role in roles)
        {
            RoleButton button = Instantiate(roleButtonPrefab, roleButtonParent);

            string roleName = TextUtilities.SplitName(role.DisplayName);
            button.SetText(roleName);

            PlayerRole selectedRole = role.Role;

            button.AddListener(() => SelectRole(selectedRole));
        }
    }

    void SelectRole(PlayerRole role)
    {
        AppController.instance.SelectRole(role);
    }
}
