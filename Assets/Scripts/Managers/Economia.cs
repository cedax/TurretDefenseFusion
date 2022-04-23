using UnityEngine;
using TMPro;

public class Economia : Singleton<Economia> {
    [SerializeField] private TextMeshProUGUI monedasTMP;
    public int Monedas { get; private set; }

    public void AgregarMonedas(int monedas) {
        Monedas += monedas;
        monedasTMP.text = Monedas.ToString();
    }

    public void RestarMonedas(int monedas) {
        Monedas -= monedas;
        monedasTMP.text = Monedas.ToString();
    }

    public int Balance(){
        return Monedas;
    }

    private void Start() {
        Monedas = 100;
        monedasTMP.text = Monedas.ToString();
    }

    private void Update() {
        if(Monedas > 1000000){
            monedasTMP.text = "BANEADO";
        }
    }
}