using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instancia;
    public static T Instancia{
        get{
            if(instancia == null){
                instancia = FindObjectOfType<T>();
                if(instancia == null){
                    GameObject nuevoGameObject = new GameObject();
                    nuevoGameObject.name = typeof(T).Name;
                    instancia = nuevoGameObject.AddComponent<T>();
                }
            }
            return instancia;
        }
    }

    protected virtual void Awake() {
        instancia = this as T;
    }
}