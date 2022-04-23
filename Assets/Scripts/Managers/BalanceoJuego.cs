using UnityEngine;

public class BalanceoJuego : Singleton<BalanceoJuego>{
    public float dañoBaseDeTorretas;
    public float potenciaDeFuego;
    public float dañoHechoPorSegundo;
    public float maximoDañoHechoPorSegundo;
    public int costoBaseTorreta;
    private float promedioDaño;
    public int enemigosBase;
    public float multiplicadorDeVelocidad;
    public float multiplicadorDeDañoMisil;
    public void calcularDañoPorSegundo(){
        if (dañoHechoPorSegundo > maximoDañoHechoPorSegundo) {
            maximoDañoHechoPorSegundo = dañoHechoPorSegundo;
        }
        dañoHechoPorSegundo = 0;
    }

    private void Update() {
        
    }

    private void Start() {
        InvokeRepeating("calcularDañoPorSegundo", 1f, 1f);
    }

}
