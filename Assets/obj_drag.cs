using UnityEngine;

public class obj_drag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 savePosisi;
    public bool isDiatasObj;

    Transform saveObj;
    public int id;
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
            }
            else
            {
                transform.position = savePosisi;
            }
        }
        else { 
            transform.position = savePosisi;    
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
