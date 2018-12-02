using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        }

    //resumes the game by closing the pause menu and setting the timescale back to 1
    public void Resume() {
        Menu.SetActive(false);
        Time.timeScale = 1;
        }

    public void Quit() {
        Application.Quit();
        }

    //creates a new dungeon by resetting the scene
    public void CreateNewDungeon() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
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

        player.SetActive(!useOverview);
        }

    }
