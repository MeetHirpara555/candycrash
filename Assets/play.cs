
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class play : MonoBehaviour
{
    public GameObject[,] candyy;
    public GameObject[] colorcandy;
    public int row = 8,col = 5;
    Vector2 mouspoint;
    public GameObject currentCandy,keyy;
    matchfinder mf;
    // Start is called before the first frame update
    void Start()
    {
        candyy = new GameObject[col,row];
        candygen();
        mf = FindObjectOfType<matchfinder>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void candygen()
    {
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                int ranCan = Random.Range(0, colorcandy.Length);
                while (checkMatchCandies(i, j, colorcandy[ranCan]))
                {
                    ranCan = Random.Range(0, colorcandy.Length);
                }
                
                GameObject g = Instantiate(colorcandy[ranCan], new Vector2(i, j), Quaternion.identity);
                g.transform.parent = this.transform;
                g.name = "("+i+","+j+")";
                g.GetComponent<candy>().Row = j;
                g.GetComponent<candy>().Col = i;
                candyy[i,j] = g;
            }
        }
        keygenerator();
    }

    void keygenerator()
    {
        int r = Random.Range(0, col-1);
        Destroy(candyy[r,row-1]);
        candyy[r, row-1] = null;
        int aa = row-1;
        keyy = Instantiate(keyy,new Vector2(r,row), Quaternion.identity);
        keyy.transform.parent = this.transform;
        keyy.name = "(" + r + "," +aa + ")";
        keyy.GetComponent<candy>().Row = aa;
        keyy.GetComponent<candy>().Col = r;
        candyy[r, aa] = keyy;

    }

    void keyloaction()
    {
        if (keyy.GetComponent<candy>().Row == 0)
        {
            print("you win");
        }
    }
    bool checkMatchCandies(int col, int row, GameObject g)
    {
        if (row > 1 && col > 1)
        {
            if (candyy[col, row - 1].tag == g.tag && candyy[col, row - 2].tag == g.tag)
            {
                return true;
            }
            if (candyy[col - 1, row].tag == g.tag && candyy[col - 2, row].tag == g.tag)
            {
                return true;
            }
        }
        else if (row > 1)
        {
            if (candyy[col, row - 1].tag == g.tag && candyy[col, row - 2].tag == g.tag)
            {
                return true;
            }
        }
        else if (col > 1)
        {
            if (candyy[col - 1, row].tag == g.tag && candyy[col - 2, row].tag == g.tag)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator decreasRow()
    {
        yield return new WaitForSeconds(0.4f);
        int count = 0;

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (candyy[i, j] == null)
                {
                    count++;
                }
                else if (count > 0)
                {
                    candyy[i, j].GetComponent<candy>().Row -= count;
                    candyy[i, j] = null;
                }
            }
            count = 0;
        }
        keyloaction();
        StartCoroutine(destroyafterrefill());

    }

    IEnumerator destroyafterrefill()
    {
        mf.matchcandy.Clear();
        StartCoroutine(genratetonul());
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (candyy[i, j] != null)
                {
                    if (candyy[i, j].GetComponent<candy>().ismatch)
                    {
                        candydestroy();
                        break;
                    }
                }

            }
        }
    }

    IEnumerator genratetonul()
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if(candyy[i, j] == null)
                {
                    int ranCan = Random.Range(0, colorcandy.Length);
                    GameObject g = Instantiate(colorcandy[ranCan], new Vector2(i, j), Quaternion.identity);
                    g.transform.parent = this.transform;
                    g.name = "(" + i + "," + j + ")";
                    g.GetComponent<candy>().Row = j;
                    g.GetComponent<candy>().Col = i;
                    candyy[i, j] = g;
                }
                
            }
        }
    }
    public void candydestroy()
    {
        if(mf.matchcandy.Count >= 4)
        {
            mf.checkbomb();
        }

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                if (candyy[i, j] != null)
                {
                    if (candyy[i, j].GetComponent<candy>().ismatch)
                    {
                        Destroy(candyy[i, j]);
                        candyy[i, j] = null;
                    }
                }

            }
        }
        StartCoroutine(decreasRow());
        
    }





}
