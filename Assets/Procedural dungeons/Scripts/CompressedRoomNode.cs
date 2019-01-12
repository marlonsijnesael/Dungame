
using UnityEngine;

[System.Serializable]
public class CompressedRoomNode {
    public int interriorType = 0;
    public Vector2 position = new Vector2(0, 0);
    //public bool[] doorDirections= new bool[4];// hasDoorEast, hasDoorWest, hasDoorNorth, hasDoorSouth;
    public int[] doorDirections = { 0, 0, 0, 0 };
    public CompressedRoomNode(int _interrior, Vector3 _position,int[] _doorDirs ) {
        interriorType = _interrior;
        position = _position;
        doorDirections = _doorDirs;
        }
    }
//bool _hasDoorEast, bool _hasDoorWest, bool _hasDoorNorth, bool _hasDoorSouth
//doorDirections[0] = _hasDoorEast;
//doorDirections[1] = _hasDoorWest;
//doorDirections[2] = _hasDoorNorth;
//doorDirections[3] = _hasDoorSouth;