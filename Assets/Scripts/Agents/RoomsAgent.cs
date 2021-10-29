using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoomsAgent : MonoBehaviour
{    
    public enum Phase
    {
        init,
        //projeto 1
        Elicitação,
        Especificação,
        Validação,
        Arquitetura,
        Componentes,
        BancoDeDados,
        Interface,
        TesteComp,
        Sistema,
        Cliente,
        EvoReq,
        EvoSis,
        EvoMudanças,
        Novo,
        Saída,

        //projeto 2
        Requisitos,
        Sprint,
        Incremento 
    }

    private bool subscribed;
    private Room playerLocation;
    public static bool previousPhaseCompleted = false;
    [SerializeField] private Phase roomState;
    [SerializeField] private Phase previousRoomState;


    public delegate void CompletePhase(string phase);
    public static CompletePhase OnPhaseCompleted;
    //public static CompletePhase OnPhaseNotCompleted;

    private void Start()
    {
        roomState = Phase.init;
        //subscribing for events
        Room.OnEnterRoom += See;

        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void See()
    {
        playerLocation = GameManager.currentRoom;

        //set the phase state based on the player location, project 1
        if(playerLocation.roomClass == "Elicitação") roomState = Phase.Elicitação;
        if(playerLocation.roomClass == "Especificação") roomState = Phase.Especificação;
        if(playerLocation.roomClass == "Validação") roomState = Phase.Validação;
        if(playerLocation.roomClass == "Arquitetura") roomState = Phase.Arquitetura;
        if(playerLocation.roomClass == "Componentes") roomState = Phase.Componentes;
        if(playerLocation.roomClass == "BancoDeDados") roomState = Phase.BancoDeDados;
        if(playerLocation.roomClass == "Interface") roomState = Phase.Interface;
        if(playerLocation.roomClass == "TesteComp") roomState = Phase.TesteComp;
        if(playerLocation.roomClass == "Sistema") roomState = Phase.Sistema;
        if(playerLocation.roomClass == "Cliente") roomState = Phase.Cliente;
        if(playerLocation.roomClass == "EvoReq") roomState = Phase.EvoReq;
        if(playerLocation.roomClass == "EvoSis") roomState = Phase.EvoSis;
        if(playerLocation.roomClass == "EvoMudanças") roomState = Phase.EvoMudanças;
        if(playerLocation.roomClass == "Final") roomState = Phase.Novo;

        //set the phase state based on the player location, project 2
        if(playerLocation.roomClass == "Requisitos") roomState = Phase.Requisitos;
        if(playerLocation.roomClass == "Sprint") roomState = Phase.Sprint;
        if(playerLocation.roomClass == "Incremento") roomState = Phase.Incremento;

        //set the last state
        if(playerLocation.roomClass == "Saída") roomState = Phase.Saída;

        Perception();
    }

    void Perception()
    {
        //check if the state changed, on change, act an reset the previousRoom variable 
        if(roomState != previousRoomState)
        {
            Actions();
            previousRoomState = roomState;
        }
    }

    void Actions()
    {
        previousPhaseCompleted = false;
        //check if the entire phase was explored
        int count = 0;
        int phaseRoomsNum = 0;
        
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if(room.GetComponent<Room>().roomClass == previousRoomState.ToString())
            {
                //count the number of rooms of that phase
                phaseRoomsNum++;
                //count each room of that phase that is explored
                if(room.GetComponent<Room>().explored) count++;
            }
        }

        //if each room of that phase is explored, that phase is complete
        if(count == phaseRoomsNum)
        {
            RoomsAgent.previousPhaseCompleted = true;
        }

        OnPhaseCompleted(roomState.ToString());
    }
}
