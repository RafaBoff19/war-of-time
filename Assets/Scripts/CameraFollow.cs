using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;
    public float suavidade = 5f;

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 posicaoAlvo = new Vector3(alvo.position.x, alvo.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicaoAlvo, suavidade * Time.deltaTime);
    }
}