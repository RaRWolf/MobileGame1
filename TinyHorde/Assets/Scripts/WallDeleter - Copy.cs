using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeleterb : MonoBehaviour
{
    public Collider[] conflictDeleter;
    public LevelGenerator levelGenerator;


    void Start()
    {
        levelGenerator = FindObjectsOfType<LevelGenerator>()[0];
    }


    void Update()
    {
        //If a camera-detecting wall isn't currently conflicting with another wall, activate it.
        if (conflictDeleter[0] == null && (conflictDeleter[1]) != null)
        {
            (conflictDeleter[1]).enabled = true;
        }
        else if (conflictDeleter[1] == null && (conflictDeleter[0]) != null)
        {
            (conflictDeleter[0]).enabled = true;
        }
        else if (conflictDeleter[1] != null && (conflictDeleter[0]) != null)
        {
            (conflictDeleter[0]).enabled = false;
            (conflictDeleter[1]).enabled = false;
        }
    }

    //I only put NameChecker in a seperate function because I'm scared the "OnTriggerStay" being in FixedUpdate will mess with stuff
    void OnTriggerEnter(Collider other2)
    {
        NameChecker(other2);
    }

    //The NameChecker checks whether or not this wall is touching its antithesis. If it is, it'll deactivate both of them.
    void NameChecker(Collider other)
    {
        if (gameObject.name == "ExitNorth")
        {
            if (other.gameObject.name == ("ExitSouth") || other.gameObject.name == ("ExitNorth"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitEast")
        {
            if (other.gameObject.name == ("ExitWest") || other.gameObject.name == ("ExitEast"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitSouth")
        {
            if (other.gameObject.name == ("ExitNorth") || other.gameObject.name == ("ExitSouth"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitWest")
        {
            if (other.gameObject.name == ("ExitEast") || other.gameObject.name == ("ExitWest"))
            {
                ActivateDeleter(other);
            }
        }

        //Corners
        if (gameObject.name == "ExitNorthEast")
        {
            if (other.gameObject.name == ("ExitSouthWest")|| other.gameObject.name == ("ExitNorthEast"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitSouthEast")
        {
            if (other.gameObject.name == ("ExitNorthWest")|| other.gameObject.name == ("ExitSouthEast"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitSouthWest")
        {
            if (other.gameObject.name == ("ExitNorthEast")|| other.gameObject.name == ("ExitSouthWest"))
            {
                ActivateDeleter(other);
            }
        }
        if (gameObject.name == "ExitNorthWest")
        {
            if (other.gameObject.name == ("ExitSouthEast") || other.gameObject.name == ("ExitNorthWest"))
            {
                ActivateDeleter(other);
            }
        }
    }

        //Deactivates the walls, then tells the spawner it's okay to continue with the next spawning tile
        void ActivateDeleter(Collider other)
        {
            conflictDeleter[0] = other;
            conflictDeleter[1] = gameObject.GetComponent<Collider>();
            (conflictDeleter[0]).enabled = false;
            (conflictDeleter[1]).enabled = false;

        levelGenerator.spawning = false;
        levelGenerator.deleting = false;
    }
}
