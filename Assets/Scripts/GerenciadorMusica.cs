using UnityEngine;

public class GerenciadorMusica : MonoBehaviour
{
    public AudioClip trilhaBatalha;
    public AudioClip trilhaBoss;

    private AudioSource audioSource;
    private bool tocandoBoss = false;

    public static GerenciadorMusica instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TocarBatalha();
    }

    public void TocarBatalha()
    {
        if (tocandoBoss) return;
        audioSource.clip = trilhaBatalha;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TocarBoss()
    {
        tocandoBoss = true;
        audioSource.clip = trilhaBoss;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PararMusica()
    {
        audioSource.Stop();
    }

    public void VoltarBatalha()
    {
        tocandoBoss = false;
        TocarBatalha();
    }
}