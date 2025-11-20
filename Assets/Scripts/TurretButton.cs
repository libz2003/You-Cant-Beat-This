using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretButton : MonoBehaviour
{
    public GameObject turret;

    public void PickTurret()
    {
        // check if BuildManager has the same turretToBuild
        if (BuildManager.instance.GetTurretToBuild() == turret)
        {
            // if yes, deselect it
            BuildManager.instance.SetTurretToBuild(null);
        }
        else
        {
            // if not, select it
            BuildManager.instance.SetTurretToBuild(turret);
        }
    }
}
