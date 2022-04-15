using UnityEngine;

public class GizmoPivote : MonoBehaviour
{
    [SerializeField] private float pivoteTamanio = 0.5f;
    [SerializeField] private Color color = Color.green;

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, pivoteTamanio);
    }
}
