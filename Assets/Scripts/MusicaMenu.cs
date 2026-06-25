using UnityEngine;
 
public class MusicaMenu : MonoBehaviour
{
    public static MusicaMenu instancia;
 
    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // sobrevive à troca de cena
        }
        else
        {
            Destroy(gameObject); // evita duplicata se voltar ao menu
        }
    }
}