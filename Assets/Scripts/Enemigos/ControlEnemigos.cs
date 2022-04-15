using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigos : Singleton<ControlEnemigos>
{
    private List<GameObject> enemigos;

    public List<GameObject> Enemigos => enemigos;

    private void Start() {
        enemigos = new List<GameObject>();
    }
}
