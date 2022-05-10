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

        MakeCell(mazeCubes);  // ť�� ���� ����
        MakeMaze(mazeCubes);  // �游���
    }

    void MakeCell(GameObject[] cubes)
    {
        for (int i = 0; i < num * num; i++)
        {   // ���̺� ������� ����
            cubes[i].transform.position = maze.transform.position + new Vector3(4.0f * (i % num), 0, 4.0f * (i / num));
        }
    }

    void MakeMaze(GameObject[] cubes)
    {
        while(true)
        {
            int random = Random.Range(0, num);  // ù���� ���� ���ϱ�
            Debug.Log(random + 1);

            int[] indexs = new int[50];

            // ���� ���ڸ� ������ ť����� isTrigger�� ������
            //cubes[random].gameObject.GetComponent<BoxCollider>().isTrigger = true;

            return;
        }
    }
}
