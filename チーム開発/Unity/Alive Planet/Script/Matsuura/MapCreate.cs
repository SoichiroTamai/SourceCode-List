using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapCreate : MonoBehaviour
{
    public bool mapCreate;

    public bool tellMapCreate;

    public GameObject Mosaic;

    public GameObject BlockSprite;

    public List<GameObject> MapBlockList = new List<GameObject>();       // 船(部屋)
    public List<GameObject> MapBlockList_route = new List<GameObject>(); // 船(通路)

    public List<GameObject> MosaicList = new List<GameObject>();

    private Vector2 v_SpriteVH;

    private int i_MapRangeSizeX;

    private int i_MapRangeSizeY;

    private int i_MinXSize;

    private int i_MinYSize;

    private bool b_getDownKey;

    private int Rooms;

    private bool b_compleate;

    void Start()
    {
        v_SpriteVH.x = BlockSprite.GetComponent<SpriteRenderer>().bounds.size.x;
        v_SpriteVH.y = BlockSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        i_MinXSize = 4;
        i_MinYSize = 4;
        i_MapRangeSizeX = i_MinXSize;
        i_MapRangeSizeY = i_MinYSize;
        mapCreate = true;
        b_getDownKey = false;
        Rooms = 2;

        b_compleate = false;

        MapBlockList.Add(BlockSprite);

    }

    // Update is called once per frame
    void Update()
    {
        //tellMapCreate = mapCreate;
        if (mapCreate)
        {
            Rooms = Random.Range(2, 6);
            MapGeneration();
            tellMapCreate = true;
        }
        else
        {
            tellMapCreate = false;
        }


        //if (Input.GetKey(KeyCode.Space))
        //{
        //    if (!b_getDownKey)
        //    {
        //        mapCreate = true;
        //        b_getDownKey = true;
        //    }
        //}
        //else
        //{
        //    b_getDownKey = false;
        //}

        //if (Input.GetKey(KeyCode.Return))
        //{
        //    foreach (Transform n in gameObject.transform)
        //    {
        //        GameObject.Destroy(n.gameObject);
        //    }
        //    MapBlockList.Clear();
        //}

        //tellMapCreate = mapCreate;

        //if (mapCreate)
        //{
        //    MapGeneration();
        //}
        //else
        //{
        //    Rooms = Random.Range(2, 6);
        //}
    }

    public bool MapCreateCompleate()
    {
        return b_compleate;
    }

    // マップを生成
    private void MapGeneration()
    {
        int XRoomint = 0;
        int YRoomint = 0;
        int DoorY = 0;
        int RoomY = 0;
        int DoorX = 0;

        Vector2 Doorvec = new Vector2(0f, 0f);
        Vector2 oDoorvec = new Vector2(0f, 0f);

        for (int l = 0; l < Rooms; l++)
        {
            {
                i_MapRangeSizeX = Random.Range(i_MinXSize + (i_MinXSize - DoorX), 18);
                i_MapRangeSizeY = Random.Range(i_MinYSize, 6);
            }

            for (int i = 0; i < i_MapRangeSizeY; i++)
            {
                for (int j = 0; j < i_MapRangeSizeX; j++)
                {
                    if (!BlockSprite) { return; }

                    Vector2 vec;
                    vec.x = v_SpriteVH.x * j + XRoomint;
                    vec.y = v_SpriteVH.y * i + YRoomint;

                    GameObject obj = Instantiate(BlockSprite, vec, Quaternion.identity);

                    obj.transform.parent = transform;

                    if (i == i_MapRangeSizeY - 1 && j == i_MapRangeSizeX - 1)
                    {
                        Doorvec = vec;

                        if (DoorY > 0)
                        {
                            Debug.Log(vec.y);
                        }
                    }
                    MapBlockList.Add(obj);

                }
            }

            if (Mosaic)
            {
                if (l > 0)
                {
                    Vector3 vec3 = new Vector3((float)i_MapRangeSizeX / 2.0f - 0.5f + XRoomint, (float)i_MapRangeSizeY / 2.0f - 0.5f + YRoomint, 0.0f);

                    GameObject Mobj = Instantiate(Mosaic, vec3, Quaternion.identity);

                    Mobj.GetComponent<Transform>().localScale = new Vector3(i_MapRangeSizeX, i_MapRangeSizeY, 1.0f);

                    Mobj.AddComponent<MosaicScript>();

                    MosaicList.Add(Mobj);
                }

            }

            int boolint;

            {
                boolint = Random.Range(0, 2);
            }

            if (l == Rooms - 1) break;


            switch (boolint)
            {
                case 0:
                    XRoomint += i_MapRangeSizeX + 1;

                    RoomY = (int)Doorvec.y;

                    {
                        if (DoorY > 0)
                        {
                            Doorvec.y = Random.Range(RoomY - 1, (int)Doorvec.y);
                        }
                        else
                        {
                            Doorvec.y = Random.Range(0, (int)Doorvec.y);
                        }
                    }

                    Doorvec.x += v_SpriteVH.x;
                    GameObject obj0 = Instantiate(BlockSprite, Doorvec, Quaternion.identity);

                    obj0.transform.parent = transform;

                    MapBlockList_route.Add(obj0);

                    break;
                case 1:
                    YRoomint += i_MapRangeSizeY + 1;
                    Vector2 DoorVec = new Vector2(0f, 0f);

                    {
                        XRoomint += Random.Range(0, 5);
                        DoorVec.x = Random.Range((int)XRoomint, (int)XRoomint + 5);
                    }

                    if (DoorVec.x > Doorvec.x)
                    {
                        DoorVec.x -= 1;
                    }


                    DoorVec.y = Doorvec.y + 1;

                    GameObject obj1 = Instantiate(BlockSprite, DoorVec, Quaternion.identity);

                    obj1.transform.parent = transform;

                    MapBlockList_route.Add(obj1);

                    //Debug.Log(DoorVec);

                    DoorY++;
                    break;
                case 2:
                    YRoomint -= i_MapRangeSizeY - 1;
                    {
                        XRoomint += Random.Range(0, 5);
                    }
                    break;
                case 3:
                    YRoomint -= i_MapRangeSizeY - i_MapRangeSizeY;
                    {
                        XRoomint += Random.Range(0, 5);
                    }
                    break;
                case 4:
                    YRoomint -= i_MapRangeSizeY * 2;
                    {
                        XRoomint += Random.Range(0, 5);
                    }
                    break;
                case 5:
                    YRoomint -= i_MapRangeSizeY + i_MapRangeSizeY * 2;
                    {
                        XRoomint += Random.Range(0, 5);
                    }
                    break;
            }

        }
        mapCreate = false;
        b_compleate = true;
        Debug.Log("マップ配置完了");
    }

}
