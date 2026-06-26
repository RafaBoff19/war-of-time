using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HoverBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Texto do Botão")]
    public TextMeshProUGUI texto;

    [Header("Fundo do Botão")]
    public Image fundoHover;

    [Header("Cores do Texto")]
    public Color corNormal = Color.white;
    public Color corHover = new Color(0.2f, 0.6f, 1f, 1f);

    [Header("Cores do Fundo")]
    public Color fundoNormal = new Color(0f, 0f, 0f, 0f);
    public Color fundoDestaque = new Color(0f, 0.4f, 1f, 0.4f);

    [Header("Tamanho")]
    public float tamanhoNormal = 15f;
    public float tamanhoHover = 18f;

    [Header("Sons")]
    public AudioClip somHover;
    public AudioClip somClique;

    private AudioSource audioSource;

    void Start()
    {
        if (texto == null)
            texto = GetComponentInChildren<TextMeshProUGUI>();

        audioSource = GetComponent<AudioSource>();

        AplicarNormal();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        texto.color = corHover;
        texto.fontSize = tamanhoHover;

        if (fundoHover != null)
            fundoHover.color = fundoDestaque;

        if (somHover != null && audioSource != null)
            audioSource.PlayOneShot(somHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AplicarNormal();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (somClique != null && audioSource != null)
            audioSource.PlayOneShot(somClique);

        AplicarNormal();
    }

    void AplicarNormal()
    {
        texto.color = corNormal;
        texto.fontSize = tamanhoNormal;

        if (fundoHover != null)
            fundoHover.color = fundoNormal;
    }
}