using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class obj_drag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector]public Vector2 savePosisi;
    [HideInInspector] public bool isDiatasObj;

    Transform saveObj;
    public int id;
    public Text teks;
    [Space]

    public UnityEvent onDragBenar;

    void Start()
    {
        savePosisi = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
        transform.position = pos;   
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        //transform.position = savePosisi;    
        if (isDiatasObj)
        {
            int id_tempat_drop = saveObj.GetComponent<tempat_drop>().id;

            if(id == id_tempat_drop) {
                transform.SetParent(saveObj);
                transform.localPosition = Vector3.zero;
                transform.localScale = new Vector2(0.6f, 0.6f);

                saveObj.GetComponent<SpriteRenderer>().enabled = false;
                onDragBenar.Invoke();

                //jika sukses
                GameSystem.instance.DataSaatIni++;
                Data.DataScore += 200;
            }
            else
            {
                transform.position = savePosisi;

                //jika salah
                Data.DataDarah--;
            }
        }
        else { 
            transform.position = savePosisi;    

            //jika tempat tidak ada
        }
    }
    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            isDiatasObj = true;
            saveObj = trig.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            isDiatasObj = false;
        }
    }
}
