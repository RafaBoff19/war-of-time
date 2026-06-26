using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBoss : MonoBehaviour
{
    public GameObject painelBoss;
    public Slider barraVidaBoss;
    public TextMeshProUGUI textoNomeBoss;

    private BossGisilva bossAtual;

    void Start()
    {
        painelBoss.SetActive(false);
    }

    void Update()
    {
        // Procura o boss na cena se ainda não encontrou
        if (bossAtual == null)
        {
            BossGisilva boss = FindObjectOfType<BossGisilva>();
            if (boss != null)
            {
                bossAtual = boss;
                painelBoss.SetActive(true);
                barraVidaBoss.maxValue = bossAtual.vidaMaxima;
                textoNomeBoss.text = "Vorzak - O Antigo";
            }
        }
        else
        {
            // Atualiza a barra de vida
            barraVidaBoss.value = bossAtual.vidaAtual;

            // Esconde o painel se o boss morreu
            if (bossAtual == null)
                painelBoss.SetActive(false);
        }
    }
    public void EsconderBoss()
    {
    if (painelBoss != null)
        painelBoss.SetActive(false);
    }
}