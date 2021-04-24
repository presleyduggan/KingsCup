using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //int[] cards = new int[52];
    List<int> cards = new List<int>();
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> fill = new List<GameObject>();
    public List<GameObject> jack = new List<GameObject>();
    public List<GameObject> add = new List<GameObject>();
    int count;
    int one;
    System.Random rand = new System.Random();
    float etime;
    public GameObject bottle;
    public GameObject liquid;
    public bool pausegame;
    public GameObject pausepanel;
    public GameObject DeveloperPanel;
    public TMP_InputField cardinput;
    public bool freecam;
    public Toggle camtoggle;
    public int kingcount;
    public AudioSource poursound;
    public GameObject donepanel;
    public bool keepplaying;
    public Text resettext;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 52; i++)
        {
            resettext.enabled = false;
            cards.Add(i + 1);
            pausegame = false;
            freecam = false;
            kingcount = 0;
            keepplaying = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
        etime += Time.deltaTime;
        string minutes = ((int)etime / 60).ToString();
        string seconds = (etime % 60).ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && pausegame == false)
        {
            PauseGame();
        }

        if(fill[3].active == true && !keepplaying)
        {
            donepanel.active = true;
            keepplaying = true;
            Time.timeScale = 0.0f;

        }

        

    }


    public void DrawCard()
    {
        ParticleSystem parts = liquid.GetComponent<ParticleSystem>();

        int Index = rand.Next(0, cards.Count);
        int deckindex = cards[Index];
        Debug.Log(cards[Index]);
        deck[deckindex-1].SetActive(true);
        //FillCup();
        StartCoroutine(WaitTime(Index, deckindex));
        /*
        while(timer+10000 > (int)etime)
        {
            etime += Time.deltaTime;
        }
        */
        //deck[Index].SetActive(false);



        // remove card
        cards.RemoveAt(Index);
        StopParticles();
    }

    IEnumerator WaitTime(int index, int deckindex)
    {
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(disappear(index, deckindex));

    }

    private IEnumerator disappear(int index, int deckindex)
    {
        deck[deckindex-1].SetActive(false);
        if(deckindex == 13 || deckindex == 26 || deckindex == 39 || deckindex == 52)
        {
            kingcount++;
            FillCup();
            bottle.active = true;
            poursound.Play();
            StartCoroutine(PourTime());
        }
       
            yield return null;
    }

    private IEnumerator PourTime()
    {
        
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(StopParticles());
    }

    public void FillCup()
    {
        ParticleSystem parts = liquid.GetComponent<ParticleSystem>();


        parts.Play();
        liquid.GetComponent<Particles>().flow = true;
    }

    private IEnumerator StopParticles()
    {
        bottle.active = false;

        ParticleSystem parts = liquid.GetComponent<ParticleSystem>();

        liquid.GetComponent<Particles>().flow = false;

        if(kingcount < 5)
        {
            fill[kingcount-1].SetActive(true);
            jack[kingcount-1].SetActive(true);
            add[kingcount-1].SetActive(true);
            add[kingcount+3].SetActive(true); 
        }

        yield return null;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pausegame = true;
        pausepanel.active = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pausegame = false;
        pausepanel.active = false;
        donepanel.active = false;
    }

    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        pausegame = false;
        keepplaying = false;
        SceneManager.LoadScene(0);
    }

    public void DeveloperMenu()
    {
        Time.timeScale = 0.0f;
        pausegame = false;
        pausepanel.active = false;
        DeveloperPanel.active = true;
    }

    public void SpawnCard()
    {
        string input = cardinput.text;
        int input2 = int.Parse(input);
        if (input2 > 0 && input2 < 53)
        {
            deck[input2 - 1].SetActive(true);
            StartCoroutine(WaitTime(0, input2));
        }
        else
        {
            cardinput.text = "Dumbass";
        }
    }

    public void ChangeCam()
    {
        freecam = !freecam;
        resettext.enabled = freecam;
    }

    public void CloseDebug()
    {
        Time.timeScale = 1.0f;
        DeveloperPanel.active = false;
        pausepanel.active = false;
        pausegame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game was Quit");
    }

    



}
