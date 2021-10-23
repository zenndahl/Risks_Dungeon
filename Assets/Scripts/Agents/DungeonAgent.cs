using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonAgent : MonoBehaviour
{
    [Flags]
    public enum RequirementsState
    {
        init = 0,
        e1 = 1,
        e2 = 2,
        e3 = 4,
        end = 8,
        R1 = e1 | e2 | e3,
        R2 = e1 | e2,
        R3 = e1 | e3,
        R4 = e2 | e3,
    }

    [Flags]
    public enum ImplementationState
    {
        init = 0,
        p1 = 1,
        p2 = 2,
        p3 = 4,
        p4 = 8,
        end = 16,
        I1 = p1 | p2 | p3 | p4,
        I2 = p1 | p2 | p3,
        I3 = p1 | p2 | p4,
        I4 = p2 | p3 | p4,
        I5 = p1 | p2,
        I6 = p1 | p3,
        I7 = p1 | p4,
        I8 = p2 | p3,
        I9 = p2 | p4,
        I10 = p3 | p4,
    }

    [Flags]
    public enum VVState
    {
        init = 0,
        v1 = 1,
        v2 = 2,
        v3 = 4,
        end = 8,
        VV1 = v1 | v2 | v3,
        VV2 = v1 | v2,
        VV3 = v1 | v3,
        VV4 = v2 | v3
    }

    [Flags]
    public enum EvolutionState
    {
        init = 0,
        ev1 = 1,
        ev2 = 2,
        ev3 = 4,
        ev4 = 8,
        end = 16,
        EVO1 = ev1 | ev2 | ev3 | ev4,
        EVO2 = ev1 | ev2 | ev3,
        EVO3 = ev1 | ev2 | ev4,
        EVO4 = ev2 | ev3 | ev4,
        EVO5 = ev1 | ev2,
        EVO6 = ev1 | ev3,
        EVO7 = ev1 | ev4,
        EVO8 = ev2 | ev3,
        EVO9 = ev2 | ev4,
        EVO = ev3 | ev4
    }

    [Flags]
    public enum DungeonState
    {
        //dungeon state will only consider the full states
        init = 0,
        d1 = 1,
        d2 = 2,
        d3 = 4,
        d4 = 8,
        end = 16,
        D1 = d1 | d2 | d3 | d4,
        D2 = d1 | d2 | d3,
        D3 = d1 | d2 | d4,
        D4 = d2 | d3 | d4,
        D5 = d1 | d2,
        D6 = d1 | d3,
        D7 = d1 | d4,
        D8 = d2 | d3,
        D9 = d2 | d4,
        D10 = d3 | d4
    }

    private void Start()
    {
        DungeonState state = DungeonState.D2;
        if((state & DungeonState.d4) == DungeonState.d4) Debug.Log(state);
    }
}