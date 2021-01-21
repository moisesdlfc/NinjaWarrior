using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target
    public Transform target;

    // Offset de la cámara
    public Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);

    // Tiempo que la cámara está parada al inicio
    public float dumpTime = 0.3f;

    // Velocidad de la cámara
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        // Le indica a Unity que intente renderizar al numero de fps indicado
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dejamos la y fija, que solo se mueva en el eje de las x's
        Vector3 destination = new Vector3(target.position.x, offset.y, offset.z);

        // Asignamos la posición de la cámara al destino
        // Movemos la cámara de forma suave
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dumpTime);
    }

    // Reinicio de la posición de la cámara
    public void ResetCameraPosition()
    {
        // Dejamos la y fija, que solo se mueva en el eje de las x's
        Vector3 destination = new Vector3(target.position.x, offset.y, offset.z);

        // Reasignamos la posición de la cámara al destino
        this.transform.position = destination;
    }
}
