using UnityEngine;

public class GerenciadorMusica : MonoBehaviour
{
    public AudioClip trilhaBatalha;
    public AudioClip trilhaBoss;

    [Range(0f, 1f)]
    public float volume = 0.4f;

    private AudioSource audioSource;
    private bool tocandoBoss = false;

    public static GerenciadorMusica instancia;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        TocarBatalha();
    }

    public void TocarBatalha()
    {
        if (tocandoBoss) return;
        audioSource.volume = volume;
        audioSource.clip = trilhaBatalha;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TocarBoss()
    {
        tocandoBoss = true;
        audioSource.volume = volume;
        audioSource.clip = trilhaBoss;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PararMusica()
    {
        audioSource.Pause(); // pausa sem resetar a posição
    }

    public void RetomarMusica()
    {
        audioSource.UnPause(); // continua de onde parou
    }

    public void VoltarBatalha()
    {
        tocandoBoss = false;
        TocarBatalha();
    }

    public void SetVolume(float novoVolume)
    {
        volume = novoVolume;
        audioSource.volume = volume;
    }
}