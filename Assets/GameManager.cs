using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Infos")]
    public int project;
    public string projectName;
    public string actualScene;
    public string nextScene;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ERPProject(){
        project = 0;
        projectName = "Desenvolvimento de Sistema ERP";
    }

    public void AppProject(){
        project = 1;
        projectName = "Desenvolvimento de App"
    }

    public LoadNextScene(){
        SceneManager.LoadScene(nextScene);
    }
}
