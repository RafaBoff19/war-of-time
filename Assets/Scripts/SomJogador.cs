using UnityEngine;

public class SomJogador : MonoBehaviour
{
    public AudioClip somAtaque;
    public AudioClip somPassos;
    public AudioClip somDano;
    public AudioClip somMorte;
    public AudioClip somLevelUp;

    private AudioSource audioSource;
    private bool passosTocando = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TocarAtaque()
    {
        audioSource.PlayOneShot(somAtaque);
    }

    public void TocarDano()
    {
        audioSource.PlayOneShot(somDano);
    }

    public void TocarMorte()
    {
        audioSource.PlayOneShot(somMorte);
    }

    public void TocarLevelUp()
    {
        audioSource.PlayOneShot(somLevelUp);
    }

    public void TocarPassos(bool andando)
    {
        if (andando && !passosTocando)
        {
            passosTocando = true;
            audioSource.clip = somPassos;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (!andando && passosTocando)
        {
            passosTocando = false;
            audioSource.Stop();
        }
    }
}