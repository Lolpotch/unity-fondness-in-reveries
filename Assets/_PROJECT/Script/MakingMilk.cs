using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MakingMilkMechanic : MonoBehaviour
{
    public int jumlahTuanganSelesai = 5;
    public Sprite botolSusuPenuhSprite; 
    public GameObject botolSusu;
    //public AudioClip sfxSelesai;

    private AudioSource audioSource;
    public int jumlahTuangan = 0;
    private Image botolSusuImage; 

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        botolSusuImage = botolSusu.GetComponent<Image>();
    }

    public void TambahTuangan()
    {
        jumlahTuangan++;
        Debug.Log("Tuangan ke-" + jumlahTuangan);

        if (jumlahTuangan >= jumlahTuanganSelesai)
        {
            StartCoroutine(SelesaiMekanisme());
        }
    }

    private IEnumerator SelesaiMekanisme()
    {
        //if (sfxSelesai != null)
        //{
        //    audioSource.PlayOneShot(sfxSelesai);
        //    Debug.Log("Sfx Diputar");
        //}
        MechanicsManager.Instance.isMakingMilkPlayed = true;
        Debug.Log("Mekanisme selesai!");

        yield return new WaitForSeconds(3f);

        botolSusuImage.sprite = botolSusuPenuhSprite;
        Debug.Log("Botol susu telah penuh!");
    }
}
