using UnityEngine;

public class ControlTorretas : Singleton<ControlTorretas>
{
    [SerializeField] private GameObject[] torretas;
    public GameObject[] Torretas => torretas;

    private void Start() {
        torretas = new GameObject[10];
    }
}
