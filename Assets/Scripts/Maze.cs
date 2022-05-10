using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject prefabs;
    GameObject[] mazeCubes;
    GameObject maze;
    public int num;

    void Start()
    {
        mazeCubes = new GameObject[num * num];
        prefabs = Resources.Load<GameObject>("Prefabs/MazeCube");
        maze = GameObject.Find("Maze");

        for (int i = 0; i < num * num; i++)
        {
            GameObject cube = Instantiate(prefabs);
            cube.name = prefabs.name + (i + 1);
            cube.transform.parent = this.transform;
            mazeCubes[i] = cube; 
        }

        MakeCell(mazeCubes);  // 큐브 동적 생성
        MakeMaze(mazeCubes);  // 길만들기
    }

    void MakeCell(GameObject[] cubes)
    {
        for (int i = 0; i < num * num; i++)
        {   // 테이블 모양으로 생성
            cubes[i].transform.position = maze.transform.position + new Vector3(4.0f * (i % num), 0, 4.0f * (i / num));
        }
    }

    void MakeMaze(GameObject[] cubes)
    {
        while(true)
        {
            int random = Random.Range(0, num);  // 첫번쨰 숫자 정하기
            Debug.Log(random + 1);

            int[] indexs = new int[50];

            // 뽑은 숫자를 제외한 큐브들은 isTrigger가 꺼지게
            //cubes[random].gameObject.GetComponent<BoxCollider>().isTrigger = true;

            return;
        }
    }
}
