using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchfinder : MonoBehaviour
{
    play pl;
    candy cn;
    public List<GameObject> matchcandy = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<play>();
        cn = FindObjectOfType<candy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void findmatch()
    {
        StartCoroutine(checkmatch());
    }
    IEnumerator checkmatch()
    {
        yield return new WaitForSeconds(0.1f); 
        for (int i = 0; i < pl.col; i++)
        {
            for (int j = 0; j < pl.row; j++) 
            { 
                if( j > 0 && j < pl.row - 1)
                {
                    GameObject currC = pl.candyy[i, j];

                    if (currC != null)
                    {
                        GameObject upC = pl.candyy[i, j + 1];
                        GameObject downC = pl.candyy[i, j - 1];

                        if (upC != null && downC != null)
                        {
                            if (currC.tag == upC.tag && currC.tag == downC.tag)
                            {
                                currC.GetComponent<candy>().ismatch = true;
                                upC.GetComponent<candy>().ismatch = true;
                                downC.GetComponent<candy>().ismatch = true;

                                if (!matchcandy.Contains(currC))
                                {
                                    matchcandy.Add(currC);
                                }
                                if (!matchcandy.Contains(upC))
                                {
                                    matchcandy.Add(upC);
                                }
                                if (!matchcandy.Contains(downC))
                                {
                                    matchcandy.Add(downC);
                                }
                                if(currC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(currC.GetComponent<candy>().Row);
                                }
                                if (upC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(upC.GetComponent<candy>().Row);
                                }
                                if (downC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(downC.GetComponent<candy>().Row);
                                }
                                if (currC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(currC.GetComponent<candy>().Col);
                                }
                                if (upC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(upC.GetComponent<candy>().Col);
                                }
                                if (downC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(downC.GetComponent<candy>().Col);
                                }

                            }

                        }
                    }
                }

                if( i > 0 && i < pl.col - 1 )
                {
                    GameObject currC = pl.candyy[i, j];

                    if (currC != null)
                    {
                        GameObject rightC = pl.candyy[i + 1, j];
                        GameObject leftC = pl.candyy[i - 1, j];

                        if (rightC != null && leftC != null)
                        {
                            if (currC.tag == rightC.tag && currC.tag == leftC.tag)
                            {
                                currC.GetComponent<candy>().ismatch = true;
                                rightC.GetComponent<candy>().ismatch = true;
                                leftC.GetComponent<candy>().ismatch = true;

                                if (!matchcandy.Contains(currC))
                                {
                                    matchcandy.Add(currC);
                                }
                                if (!matchcandy.Contains(rightC))
                                {
                                    matchcandy.Add(rightC);
                                }
                                if (!matchcandy.Contains(leftC))
                                {
                                    matchcandy.Add(leftC);
                                }
                                if (currC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(currC.GetComponent<candy>().Row);
                                }
                                if (rightC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(rightC.GetComponent<candy>().Row);
                                }
                                if (leftC.GetComponent<candy>().isrowbomb)
                                {
                                    getrow(leftC.GetComponent<candy>().Row);
                                }
                                if (currC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(currC.GetComponent<candy>().Col);
                                }
                                if (rightC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(rightC.GetComponent<candy>().Col);
                                }
                                if (leftC.GetComponent<candy>().iscolbomb)
                                {
                                    getcol(leftC.GetComponent<candy>().Col);
                                }
                            }
                            
                        }
                    }
                }
                
            }
        }
    }

    public void checkbomb()
    {
        if (pl.currentCandy != null)
        {
            candy currc = pl.currentCandy.GetComponent<candy>();
            if ((currc.angle >= -45f && currc.angle <= 45f)  || (currc.angle >= 135f && currc.angle <= -135f))
            {
                if (currc.ismatch)
                {
                    currc.ismatch = false;
                    currc.creatrowbomb();
                }
                else if (currc.oppcandy != null)
                {

                    if (currc.oppcandy.GetComponent<candy>().ismatch)
                    {
                        currc.oppcandy.GetComponent<candy>().ismatch = false;

                        currc.oppcandy.GetComponent<candy>().creatrowbomb();
                    }
                }
            }
            else
            {
                if (currc.ismatch)
                {
                    currc.ismatch = false;
                    currc.creatcolbomb();
                }
                else if (currc.oppcandy != null)
                {
                    if (currc.oppcandy.GetComponent<candy>().ismatch)
                    {
                        currc.oppcandy.GetComponent<candy>().ismatch = false;
                        currc.oppcandy.GetComponent<candy>().creatcolbomb();
                    }
                }
            }
        }
        
    }

    void getrow(int row)
    {
        for(int i = 0;i < pl.col;i++)
        {
            if (pl.candyy[i,row].tag != "key")
            {
                pl.candyy[i,row].GetComponent<candy>().ismatch = true;
            }
        }
    }

    void getcol(int col)
    {
        for (int i = 0; i < pl.row; i++)
        {
            if (pl.candyy[col, i].tag != "key")
            {
                pl.candyy[col, i].GetComponent<candy>().ismatch = true;
            }
        }
    }
}


