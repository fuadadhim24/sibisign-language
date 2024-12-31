using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public int Target;
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
        AcakSoal();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AcakSoal();
        
    }
}
