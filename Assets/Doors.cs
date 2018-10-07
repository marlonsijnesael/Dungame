using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public GameObject doorEast, doorWest, doorNorth, doorSouth;

    public GameObject wallEast, wallWest, wallNorth, wallSouth;

    public bool hasDoorEast, hasDoorWest, hasDoorNorth, hasDoorSouth;

    public void Start() {

        }

    public void SetDoors() {

        if (!hasDoorWest) {
            wallWest.SetActive(true);
            doorWest.SetActive(false);
            } else {
            wallWest.SetActive(false);
            doorWest.SetActive(true);

            }
        if (!hasDoorEast) {
            wallEast.SetActive(true);
            doorEast.SetActive(false);
            } else {
            wallEast.SetActive(false);
            doorEast.SetActive(true);
            }
        if (!hasDoorNorth) {
            wallNorth.SetActive(true);
            doorNorth.SetActive(false);
            } else {
            wallNorth.SetActive(false);
            doorNorth.SetActive(true);


            }
        if (!hasDoorSouth) {
            wallSouth.SetActive(true);
            doorSouth.SetActive(false);
            } else {
            wallSouth.SetActive(false);
            doorSouth.SetActive(true);
            }

        }
    }
