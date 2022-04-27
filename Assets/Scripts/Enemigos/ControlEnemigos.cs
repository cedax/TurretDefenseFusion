using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigos : Singleton<ControlEnemigos>
{
    public List<GameObject> enemigos;

    public List<GameObject> Enemigos => enemigos;

    private void Start() {
        enemigos = new List<GameObject>();
    }

    private void Update() {
        for (int i = 0; i < enemigos.Count; i++)
        {
            if (enemigos[i] == null)
            {
                enemigos.RemoveAt(i);
            }
        }
    }
}
