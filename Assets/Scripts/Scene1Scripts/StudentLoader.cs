using UnityEngine;

[System.Serializable]
public class StudentPapers
{
    public GameObject[] papers;
}

public class StudentLoader : MonoBehaviour
{
    public GameObject[] studentPrefabs;
    public StudentPapers[] paperPrefabsPerStudent;

    private GameObject currentStudent;
    private GameObject[] currentPapers;
    private int index = 0;

    public Transform studentSpawn;
    public Transform papersSpawn;

    public void LoadNextStudent()
    {
        // Cleanup
        if (currentStudent) Destroy(currentStudent);
        if (currentPapers != null)
        {
            foreach (var p in currentPapers) Destroy(p);
            currentPapers = null;
        }

        if (index >= studentPrefabs.Length) return;

        // Spawn student
        currentStudent = Instantiate(studentPrefabs[index], studentSpawn.position, Quaternion.identity);

        // Spawn papers
        currentPapers = new GameObject[paperPrefabsPerStudent[index].papers.Length];
        for (int i = 0; i < currentPapers.Length; i++)
        {
            currentPapers[i] = Instantiate(
                paperPrefabsPerStudent[index].papers[i],
                papersSpawn.position,
                Quaternion.identity
            );
        }

        index++;
    }
}
