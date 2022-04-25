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
    public float multiplicadorVelocidadDisparo;
    public float multiplicadorDeDañoMisil;
    public int JefesSuperados;
    public void calcularDañoPorSegundo(){
        if (dañoHechoPorSegundo > maximoDañoHechoPorSegundo) {
            maximoDañoHechoPorSegundo = dañoHechoPorSegundo;
        }
        dañoHechoPorSegundo = 0;
    }

    private void Update() {
        
    }

    private void Start() {
        multiplicadorVelocidadDisparo = 0;
        InvokeRepeating("calcularDañoPorSegundo", 1f, 1f);
    }

}
