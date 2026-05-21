using UnityEngine;

public class DanoInimigo : MonoBehaviour
{
    public int dano = 10;
    public float intervaloDano = 1f;

    private float temporizador = 0f;

    void OnTriggerStay2D(Collider2D outro)
    {
        if (outro.CompareTag("Player"))
        {
            temporizador += Time.deltaTime;

            if (temporizador >= intervaloDano)
            {
                VidaJogador vida = outro.GetComponent<VidaJogador>();
                if (vida != null)
                    vida.ReceberDano(dano);

                temporizador = 0f;
            }
        }
    }
}
