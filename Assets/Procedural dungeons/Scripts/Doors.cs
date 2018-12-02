
using UnityEngine;


//this class checks for other rooms on each side of the room, in order to see if there should be a door or a wall
public class Doors : MonoBehaviour {

    public GameObject doorEast, doorWest, doorNorth, doorSouth;

    public GameObject wallEast, wallWest, wallNorth, wallSouth;

    public bool hasDoorEast, hasDoorWest, hasDoorNorth, hasDoorSouth;

    public GameObject interior;
    public Transform interiorSpawnPoint;

    //check if room should have an interior 
    public void SetInterior(GameObject _interior) {
        if (_interior != null) {
            GameObject interiorInstance = Instantiate(_interior);
            interiorInstance.transform.position = interiorSpawnPoint.position;
            interiorInstance.transform.SetParent(interiorSpawnPoint);
            }
        }

    //checks if doors or walls should be spawned 
    //this is kind of an ugly and is due to be replaced with a more elegenat solution
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
