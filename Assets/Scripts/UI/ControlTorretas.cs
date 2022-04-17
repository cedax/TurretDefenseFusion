using UnityEngine;

public class ControlTorretas : Singleton<ControlTorretas>
{
    [SerializeField] private GameObject[] torretas;
    public GameObject[] Torretas => torretas;

    public int Dificultad { get; private set; }
    
    private void Update() {
        for (int i = 0; i < torretas.Length; i++) {
            
        }    
    }

    private void Start() {
        torretas = new GameObject[10];
    }
}
