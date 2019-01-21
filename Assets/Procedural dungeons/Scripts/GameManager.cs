using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;



/// <summary>
/// This class enables the user to switch between perspectives and look at the created dungeon from a top-down perspective.
/// This in turn enables the user to review the dungeon in play mode without having to turn to the scene view.
/// </summary>
public class GameManager : MonoBehaviour {

    public GameObject overViewCamera;
    public bool useOverview = false;
    public GameObject player;
    public bool useDeveloperTool;
    public Button cameraButton;
    public GameObject Menu;
    public GameObject spawner;
    public InputField saveName;

    private void Start() {
        Cursor.visible = useDeveloperTool;
        Menu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        }

    private void Update() {

        MoveCamera();

        if (Input.GetKeyDown(KeyCode.X) && useDeveloperTool) {
            SwitchCamera();
            Menu.SetActive(true);
            }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Quit();
            }

        if (Input.GetKeyDown(KeyCode.V)) {
            ConvertToJSON(SaveDungeonStructure(spawner));
            }
        }

    //resumes the game by closing the pause menu and setting the timescale back to 1
    public void Resume() {
        Menu.SetActive(false);
        Time.timeScale = 1;
        }

    public void Quit() {
        Application.Quit();
        }

    public void QuitToMainMenu() {
        Destroy(SpawnSettings._instance.gameObject);
        SceneManager.LoadScene(0);
        }

    //creates a new dungeon by resetting the scene
    public void CreateNewDungeon() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        }
    public void SaveDungeon() {
        ConvertToJSON(SaveDungeonStructure(spawner));
        }
    //move the camera by using aswd or the arrow keys and zoom in by using the scrollwheel
    public void MoveCamera() {
        if (useOverview) {
            float xPos = Input.GetAxis("Horizontal");
            float zPos = Input.GetAxis("Vertical");
            float yPos = Input.mouseScrollDelta.y;
            overViewCamera.transform.position = new Vector3(overViewCamera.transform.position.x + xPos, overViewCamera.transform.position.y - yPos, overViewCamera.transform.position.z + zPos);
            }
        }

    //swtich between perspectives and enable/disable the player, pause menu and overview camera
    public void SwitchCamera() {
        useOverview = !useOverview;
        overViewCamera.SetActive(useOverview);
        Menu.SetActive(useOverview);
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
            }

        if (player != null) {
            player.SetActive(!useOverview);
            }
        }

    public static SaveDataClass SaveDungeonStructure(GameObject _spawner) {
        var result = new SaveDataClass();
        result.amountOfRooms = _spawner.transform.childCount;
        result.cRoomNodes = new CompressedRoomNode[result.amountOfRooms];

        for (int i = 0; i < _spawner.transform.childCount; i++) {
            Doors childDoors = _spawner.transform.GetChild(i).gameObject.GetComponent<Doors>();
            result.cRoomNodes[i] = new CompressedRoomNode(childDoors.nodeData.interriorType, new Vector2(childDoors.nodeData.worldPos.x, childDoors.nodeData.worldPos.z), childDoors.doorDirections);

            Debug.Log(_spawner.transform.childCount);
            }

        return result;
        }

    public void ConvertToJSON(SaveDataClass _saveData) {
        string fileName;
        if (saveName.text == "" || saveName.text == null) {
             fileName = "saveData" + DateTime.Now.ToString("dd-MM-yyyy") + ".JSON";
            } else {
            fileName = saveName.text + ".JSON";
            }
       // System.IO.File.WriteAllText("Assets/Resources/LevelData/" + fileName, JsonUtility.ToJson(_saveData, false));
        string tmpPath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log(tmpPath);
        System.IO.File.WriteAllText(tmpPath, JsonUtility.ToJson(_saveData, false));
        }
    }
