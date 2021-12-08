using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterInstance : MonoBehaviour
{
    private int turnNumber;
    public int TurnNumber
    {
        get { return turnNumber; }
        private set { turnNumber = value; }
    }
    [SerializeField]
    public PlayerCharacter player;
    [SerializeField]
    public AICharacter enemy;
    public ICharacter currentCharacter;
    public UnityEvent<PlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<PlayerCharacter> onPlayerTurnEnd;
    // Start is called before the first frame update
    public UnityEvent<AICharacter> onEnemyTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnEnd;
    public UnityEvent<ICharacter> onTurnBegin;
    public UnityEvent<ICharacter> onTurnEnd;
    public GameObject pEf1 = null;
    public GameObject pEf2 = null;
    public GameObject pEf3 = null;
    void Start()
    {
        currentCharacter = player;
        player.onAbilityCast.AddListener(OnAbilityCastCallback);
        //MusicManager.Instance.PlayTrack(MusicManager.TrackID.Battle);
    }
    public void OnAbilityCastCallback(Ability casted)
    {
        AdvancedTurns();
    }
    public void AdvancedTurns()
    {
        bool i = false;
        turnNumber++;
        GameObject g1 = null, g2 = null, g3 = null ;
        if(currentCharacter==player)
        {
            currentCharacter = enemy;
            i = !i;
            //if(i)
            //{
            //    Instantiate(pEf3, new Vector3(-.07f, -.09f, .83f), Quaternion.AngleAxis(90, Vector3.up));
            //}
            g1=Instantiate(pEf1, new Vector3(-.07f, -.09f, .83f), Quaternion.AngleAxis(0, Vector3.up));
            if (g2 != null)
            {
                delayM();

                DestroyImmediate(g2, true);
            }
            if (g3 != null)
            {
                delayM();
                DestroyImmediate(g3, true);
            }
            player.onAbilityCast.RemoveListener(OnAbilityCastCallback);
            enemy.onAbilityCast.AddListener(OnAbilityCastCallback);

            onPlayerTurnEnd.Invoke(player);
            onEnemyTurnBegin.Invoke(enemy);
        }
        else
        {
            currentCharacter = player;
            if (g1 != null)
            {
                delayM();
                DestroyImmediate(g1, true);
            }
            if (i)
            {
                g3=Instantiate(pEf3, new Vector3(-4.69f, 0.26f, .207f), Quaternion.AngleAxis(90, Vector3.up));
            }
            
                g2 =Instantiate(pEf2, new Vector3(-.74f, 1.14f, -3.98f), Quaternion.AngleAxis(12, Vector3.up));
            enemy.onAbilityCast.RemoveListener(OnAbilityCastCallback);
            player.onAbilityCast.AddListener(OnAbilityCastCallback);
            onEnemyTurnEnd.Invoke(enemy);
            onPlayerTurnBegin.Invoke(player);
        }
        onTurnBegin.Invoke(currentCharacter);
        currentCharacter.TakeTurn(this);
        //turnNumber++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndBattle()
    {
        FindObjectOfType<WorldTraveller>().ExitEncounter();
    }
    public IEnumerator delayM()
    {
        yield return new WaitForSeconds(3f);
    }
}
