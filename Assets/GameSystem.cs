using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Data
{

    public static int DataLevel, DataScore, DataWaktu, DataDarah;
}
public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public static bool NewGame = true;
    int maxLevel = 20;

    [Header("Data Permaianan")]
    public int Target, DataSaatIni;
    public bool GameAktif;
    public bool GameSelesai;

    [Header("Komponen UI")]
    public Text teks_level;
    public Text teks_waktu;
    public Text teks_score;
    public RectTransform ui_darah;
    [Space]
    public bool SistemAcak;

    [System.Serializable]
    public class DataGame
    {
        public string Nama;
        public Sprite Gambar;
    }
    [Header("Settingan Standar")]
    public DataGame[] DataPermainan;
    [Space]
    [Space]
    [Space]

    public obj_tempat_drop[] Drop_Tempat;
    public obj_drag[] Drag_Obj;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameAktif = false; 
        GameSelesai = false;
        ResetData();
        Target = Drop_Tempat.Length;
        GameAktif = true;
        AcakSoal();
                GameAktif = true;
        DataSaatIni = 0;
    }

    void ResetData()
    {
        if (NewGame)
        {
            NewGame = false;
            Data.DataWaktu = 60 * 3;
            Data.DataScore = 0;
            Data.DataDarah = 5;
            Data.DataLevel = 0;
        }
    }

    [HideInInspector]public List<int> _AcakSoal = new List<int>();
    [HideInInspector] public List<int> _AcakPos = new List<int>();
    int rand, rand2;

    public void AcakSoal()
    {
        _AcakSoal.Clear();
        _AcakPos.Clear();

        _AcakSoal = new List<int>(new int[Drag_Obj.Length]);
        for (int i = 0; i < _AcakSoal.Count; i++)
        {
            rand = Random.Range(1, DataPermainan.Length);
            while(_AcakSoal.Contains(rand))
                rand = Random.Range(1, DataPermainan.Length);

            _AcakSoal[i] = rand;
            Drag_Obj[i].id = rand - 1;
            Drag_Obj[i].teks.text= DataPermainan[rand -1].Nama;
        }

        _AcakPos = new List<int>(new int[Drop_Tempat.Length]);
        for (int i = 0; i < _AcakPos.Count; i++)
        {   
            rand2 = Random.Range(1, _AcakSoal.Count+1);
            while(_AcakPos.Contains(rand2))
                rand2 = Random.Range(1, _AcakSoal.Count+1);

            _AcakPos[i] = rand2;

            Drop_Tempat[i].Drop.id = _AcakSoal[rand2 - 1] - 1;
            Drop_Tempat[i].Gambar.sprite = DataPermainan[Drop_Tempat[i].Drop.id].Gambar;

        }

    }

    float s;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    AcakSoal();
        if (GameAktif && !GameSelesai)
        {
            if (Data.DataWaktu > 0)
            {
                s += Time.deltaTime;
                if (s >= 1) {
                    Data.DataWaktu--;
                    s= 0;
                }
            }

            if(Data.DataWaktu <= 0)
            {
                GameAktif = false;
                GameSelesai = true;

                //game kalah
            }
            Debug.Log("Target: "+Target);
            Debug.Log("DataSaatIni: "+DataSaatIni);
            Debug.Log("Droptempat: "+Drop_Tempat.Length);
            if (DataSaatIni >= Target) {
                GameSelesai = true;
                GameAktif = false;

                //game menang
                if (Data.DataLevel<(maxLevel-1)) {
                    if (Data.DataLevel <4)
                    {
                        Data.DataLevel++;
                        //pindah level
                        UnityEngine.SceneManagement.SceneManager.LoadScene("sibisign_page7 " + Data.DataLevel);
                    }
                    else
                    {
                        //pindah 
                        SceneManager.LoadScene("sibisign_page7 " + Data.DataLevel);
                    }

                }
                else
                {
                    //game selesai pindah ke menu gamifikasi

                    NewGame = true;
                    SceneManager.LoadScene("sibisign_page2");
                }
            }
        }
        setInfoUI();
        if (Data.DataDarah == 0)
        {
            NewGame = true;
            SceneManager.LoadScene("sibisign_page2");
        }
        
    }

    public void setInfoUI()
    {
        teks_level.text = (Data.DataLevel +1).ToString();
        int Menit = Mathf.FloorToInt(Data.DataWaktu / 60);
        int Detik= Mathf.FloorToInt(Data.DataWaktu % 60);
        teks_waktu.text = Menit.ToString("00") + ":" + Detik.ToString("00");

        teks_score.text = Data.DataScore.ToString();
        ui_darah.sizeDelta = new Vector2(39.03398f * Data.DataDarah, 33.8689f);
    }


}
