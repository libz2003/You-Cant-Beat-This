using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretButton : MonoBehaviour
{
    public GameObject turret;
    public int turretCost;
    public GameObject ghostTurret;

    public void PickTurret()
    {
        // check if BuildManager has the same turretToBuild
        SoundEffectManager.PlayButton();
        if (BuildManager.instance.GetTurretToBuild() == turret)
        {
            // if yes, deselect it
            BuildManager.instance.UnsetTurretToBuild();
        }
        else
        {
            // if not, select it
            BuildManager.instance.SetTurretAndGhost(turret, ghostTurret, turretCost);
        }
    }
}
