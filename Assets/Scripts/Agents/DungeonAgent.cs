using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DungeonAgent : MonoBehaviour
{
    [SerializeField] private List<Risk> risksPossible = new List<Risk>();

    //states
    private bool elicitation = false;
    private bool especification = false;
    private bool validation = false;
    private bool arquitecture = false;
    private bool components = false;
    private bool DB = false;
    private bool impInterface = false;
    private bool testComponents = false;
    private bool testSystem = false;
    private bool testClient = false;
    private bool evoRequirements = false;
    private bool evoSystem = false;
    private bool evoChanges = false;
    private bool evoNew = false;

    //scrum
    private bool scrumReq = false;
    private bool scrumSprint = false;
    private bool scrumIncrement = false;

    //state verification auxiliars
    int auxDg;
    int auxReq;
    int auxImp;
    int auxVV;
    int auxEvo;
    int auxScrum;

    //scrum auxiliars
    public int sprintLoops;

    //events
    public delegate void DungeonActionComplete();
    public static DungeonActionComplete OnDungeonActionCompleted;

    private void Start()
    {
        //subscribing for events
        RoomsAgent.OnPhaseCompleted += See;
        //RoomsAgent.OnPhaseNotCompleted += NegativeActions;

        //add the team risks as possible
        foreach (Risk risk in GameManager.risks)
        {
            if(risk.riskClass == "Equipe") risksPossible.Add(risk);
        }
    }

    void See(string phase)
    {
        if(phase == "Elicitação") elicitation = true;
        if(phase == "Especificação") especification = true;
        if(phase == "Validação") validation = true;
        if(phase == "Arquitetura") arquitecture = true;
        if(phase == "Componentes") components = true;
        if(phase == "BancoDeDados") DB = true;
        if(phase == "Interface") impInterface = true;
        if(phase == "TesteComp") testComponents = true;
        if(phase == "Sistema") testSystem = true;
        if(phase == "Cliente") testClient = true;
        if(phase == "EvoReq") evoRequirements = true;
        if(phase == "EvoSis") evoSystem = true;
        if(phase == "EvoMudanças") evoChanges = true;
        if(phase == "Novo") evoNew = true;


        if(phase == "Requisitos") scrumReq = true;
        if(phase == "Sprint") scrumSprint = true;
        if(phase == "Incremento") scrumIncrement = true;

        Perception();
    }

    void Perception()
    {
        //reseting the risks possible
        risksPossible.Clear();

        //selecting the possible risks based on the game phase
        foreach (Risk risk in GameManager.risks)
        {
            if((elicitation || especification || validation || evoRequirements || scrumReq) && 
            (risk.riskClass == "Requisitos" || risk.riskClass == "Planejamento" || risk.riskClass == "Gerência" || risk.riskClass == "Direção"))
            {
                if(!risksPossible.Contains(risk)) risksPossible.Add(risk);
            }

            if((arquitecture || components ||testComponents || impInterface || scrumSprint) && 
            (risk.riskClass == "Produto" || risk.riskName == "Mudança de Requisitos" || risk.riskClass == "Gerência") || risk.riskClass == "Direção")
            {
                if(!risksPossible.Contains(risk)) risksPossible.Add(risk);
            }

            if((DB || testSystem || evoSystem) && 
            (risk.riskClass == "Infra" || risk.riskClass == "Produto") || risk.riskClass == "Gerência" || risk.riskClass == "Direção")
            {
                if(!risksPossible.Contains(risk)) risksPossible.Add(risk);
            }

            if((testClient || evoChanges || evoNew || scrumSprint) &&
            (risk.riskClass == "Externo" || risk.riskClass == "Direção"))
            {
                if(!risksPossible.Contains(risk)) risksPossible.Add(risk);
            }
        }

        Action();
    }

    void Action()
    {
        foreach (Risk risk in risksPossible)
        {
            if(GameManager.project == 1)
            {
                if(risk.riskClass == "Requisitos")
                {
                    if((elicitation || especification || validation || evoRequirements) &&
                    RoomsAgent.previousPhaseCompleted) risk.DecreaseProb(1);
                    else risk.IncreaseProb(1);
                }

                if(risk.riskClass == "Planejamento")
                {
                    if(elicitation ||arquitecture || evoSystem
                    &&
                    RoomsAgent.previousPhaseCompleted) risk.DecreaseProb(1);
                    else risk.IncreaseProb(1);
                }

                if(risk.riskClass == "Produto" )
                {
                    if(components || testComponents || evoSystem || evoNew &&
                    RoomsAgent.previousPhaseCompleted) risk.DecreaseProb(1);
                    else risk.IncreaseProb(1);
                }

                if(risk.riskClass == "Infra")
                {
                    if(DB || testSystem && RoomsAgent.previousPhaseCompleted) risk.DecreaseProb(1);
                    else risk.IncreaseProb(1);
                }

                if(impInterface)
                {
                    if(risk.riskName == "Mudança de Requisitos" || risk.riskName == "Dificuldade de Integração" &&
                    RoomsAgent.previousPhaseCompleted) 
                        risk.DecreaseProb(1);
                }
                else if(risk.riskName == "Mudança de Requisitos" || risk.riskName == "Dificuldade de Integração") risk.IncreaseProb(1);

                if(risk.riskClass == "Externo")
                {
                    if(testComponents || evoChanges || evoNew &&
                    RoomsAgent.previousPhaseCompleted) risk.DecreaseProb(1);
                    else risk.IncreaseProb(1);
                }
            }
            else
            {
                //when the sprint ends, all the risks probability decreases
                //more loops = less probability
                if(scrumSprint)
                {
                    int mod;
                    mod = sprintLoops/2;
                    if(mod < 1) mod = 1;
                    if(mod > 4) mod = 4;
                    risk.DecreaseProb(mod);
                    
                }

                if(scrumIncrement)
                {
                    int mod;
                    mod = SCRUM.sprints;
                    if(mod > 4) mod = 4;

                    if(risk.riskClass == "Externo" || risk.riskClass == "Produto")
                    {
                        risk.DecreaseProb(mod);   
                    }
                }
            }
        }

        //reset the variables so that the actions dont trigger multiple times
        if(elicitation == true) elicitation = false;
        if(especification == true) especification = false;
        if(validation == true) validation = false;
        if(arquitecture == true) arquitecture = false;
        if(components == true) components = false;
        if(DB == true) DB = false;
        if(impInterface == true) impInterface = false;
        if(testComponents == true) testComponents = false;
        if(testSystem == true) testSystem = false;
        if(testClient == true) testClient = false;
        if(evoRequirements == true) evoRequirements = false;
        if(evoSystem == true) evoSystem = false;
        if(evoChanges == true) evoChanges = false;
        if(evoNew == true) evoNew = false;

        OnDungeonActionCompleted();
    }

    public void SetRiskPossible(Risk risk)
    {
        risksPossible.Add(risk);
    }

    public List<Risk> GetRisksPossible()
    {
        return risksPossible;
    }
}