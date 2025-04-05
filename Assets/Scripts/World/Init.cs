using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] GameObject creationplayer, creationpanel, player;
    [SerializeField] Transform entitycontainer;
    
    void Start()
    {
        int slot = PlayerPrefs.GetInt("slot");
        int newgame = PlayerPrefs.GetInt("newgame");
        if (newgame == 1)
        {
            creationplayer.SetActive(true);
            creationpanel.SetActive(true);
        }
        else
        {
            SaveData data = SaveSystem.GetSaveData();
            foreach (Transform t in entitycontainer)
            {
                Entity e = t.GetComponent<Entity>();
                foreach (SaveData.SaveEntity se in data.savedUniqueEntities) 
                {
                    if (se.name == e.uniqueName)
                    {
                        e.transform.position = se.position;
                        e.transform.rotation = se.rotation;
                        break;
                    }
                }
            }
            Destroy(creationplayer);
            Destroy(creationpanel);
            player.SetActive(true);

            Destroy(gameObject);
        }
    }
}
