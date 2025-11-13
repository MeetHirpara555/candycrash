using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    public int Row, Col, targetX,targetY, prevrow,prevcol;
    Vector2 firstpos, lastpos;
    public float angle;
    public GameObject oppcandy,rowbomb,colbomb;
    public bool ismatch = false,isrowbomb = false,iscolbomb=false;
    play pl;
    matchfinder mf;
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<play>();
        mf = FindObjectOfType<matchfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        targetX = Col;
        targetY = Row;

        if(targetX != transform.position.x)
        {
            if(Mathf.Abs(targetX - transform.position.x) > 0.1f)
            {
                Vector2 newpos = new Vector2(targetX, transform.position.y);
                transform.position = Vector2.Lerp(transform.position,newpos,0.4f);
                if (pl.candyy[Col, Row] != this.gameObject)
                {
                    pl.candyy[Col, Row] = this.gameObject;
                }
            }
            else
            {
                Vector2 newpos = new Vector2(targetX, transform.position.y);
                transform.position =newpos;
            }
        }

        if (targetY != transform.position.y)
        {
            if (Mathf.Abs(targetY - transform.position.y) > 0.1f)
            {
                Vector2 newpos = new Vector2(transform.position.x, targetY);
                transform.position = Vector2.Lerp(transform.position, newpos, 0.4f);
                if (pl.candyy[Col, Row] != this.gameObject)
                {
                    pl.candyy[Col, Row] = this.gameObject;
                }
            }
            else
            {
                Vector2 newpos = new Vector2(transform.position.x, targetY);
                transform.position = newpos;
            }
        }

        mf.findmatch();
     
    }

    private void OnMouseDown()
    {
        firstpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pl.currentCandy = this.gameObject;
    }

    private void OnMouseUp()
    {
        lastpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        findangle();
    }

    void findangle()
    {
        if(Mathf.Abs(lastpos.x - firstpos.x) > 0.5f ||  Mathf.Abs(lastpos.y - firstpos.y) > 0.5f)
        {
            Vector2 offset = new Vector2(lastpos.x - firstpos.x, lastpos.y - firstpos.y);
            angle = Mathf.Atan2(offset.y, offset.x)*Mathf.Rad2Deg;
            movedirection();
        }
        
    }

    void movedirection()
    {
        if(angle >= -45f &&  angle <= 45f)
        {
            oppcandy = pl.candyy[Col+1,Row];
            oppcandy.GetComponent<candy>().Col -= 1;
            prevrow = Row;
            prevcol = Col;
            Col += 1;
        }
        else if (angle >= 45f && angle <= 135f)
        {
            oppcandy = pl.candyy[Col, Row + 1];
            oppcandy.GetComponent<candy>().Row -= 1;
            prevrow = Row;
            prevcol = Col;
            Row += 1;
        }
        else if (angle >=135f || angle <= -135f)
        {
            oppcandy = pl.candyy[Col - 1, Row];
            oppcandy.GetComponent<candy>().Col+= 1;
            prevrow = Row;
            prevcol = Col;
            Col -= 1;
            Col -= 1;
        }
        else if (angle >= -135 && angle <= 45f)
        {
            oppcandy = pl.candyy[Col, Row - 1];
            oppcandy.GetComponent<candy>().Row += 1;
            prevrow = Row;
            prevcol = Col;
            Row -= 1;
        }
        StartCoroutine(matchno());
      
    }

    IEnumerator matchno()
    {
        yield return new WaitForSeconds(0.4f);
        if (oppcandy != null)
        {
            if(!ismatch &&  !oppcandy.GetComponent<candy>().ismatch)
            {
                oppcandy.GetComponent<candy>().Row = Row;
                oppcandy.GetComponent<candy>().Col = Col;
                Row = prevrow;
                Col = prevcol;
                oppcandy = null;
            }
            else
            {
                pl.candydestroy();
            }
        }
    }
    
    public void creatrowbomb()
    {
        isrowbomb = true;
        Instantiate(rowbomb,transform);
    }

    public void creatcolbomb()
    {
        iscolbomb = true;
        Instantiate(colbomb, transform);
    }
}
