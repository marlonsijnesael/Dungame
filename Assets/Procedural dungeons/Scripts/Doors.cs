
using UnityEngine;


//this class checks for other rooms on each side of the room, in order to see if there should be a door or a wall
public class Doors : MonoBehaviour {

    public GameObject doorEast, doorWest, doorNorth, doorSouth;
    public GameObject wallEast, wallWest, wallNorth, wallSouth;

    //  public bool hasDoorWest, hasDoorNorth, hasDoorSouth;
    public bool hasDoorNorth, hasDoorEast, hasDoorSouth, hasDoorWest;
    public int[] doorDirections = { 0, 0, 0, 0 };
    public GameObject interior;
    public Transform interiorSpawnPoint;

    [SerializeField]
    public RoomNode nodeData;

    //check if room should have an interior 
    public void SetInterior(GameObject _interior) {
        if (_interior != null) {
            GameObject interiorInstance = Instantiate(_interior);
            interiorInstance.transform.position = interiorSpawnPoint.position;
            interiorInstance.transform.SetParent(interiorSpawnPoint);
            }
        }

    public void SetBools(int[] _directionsInt) {
        doorDirections = _directionsInt;
        }

    //checks if doors or walls should be spawned 
    //this is kind of an ugly and is due to be replaced with a more elegenat solution
    public void SetDoors() {
        if (doorDirections[3] == 0) {
            wallWest.SetActive(true);
            doorWest.SetActive(false);
            } else {
            wallWest.SetActive(false);
            doorWest.SetActive(true);

            }
        if (doorDirections[1] == 0) {
            wallEast.SetActive(true);
            doorEast.SetActive(false);
            } else {
            wallEast.SetActive(false);
            doorEast.SetActive(true);
            }
        if (doorDirections[0] ==0) {
            wallNorth.SetActive(true);
            doorNorth.SetActive(false);
            } else {
            wallNorth.SetActive(false);
            doorNorth.SetActive(true);
            }
        if (doorDirections[2] == 0) {
            wallSouth.SetActive(true);
            doorSouth.SetActive(false);
            } else {
            wallSouth.SetActive(false);
            doorSouth.SetActive(true);
            }


        }
    }
