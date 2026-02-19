using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EelSpawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float eelLifetime;
    [SerializeField] private float eelSpeed;
    [SerializeField] private GameObject eelPrefab;


    private void Start()
    {
        StartCoroutine(SpawnEel());
    }

    private IEnumerator SpawnEel()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);

        GameObject newEel = Instantiate(eelPrefab, transform.position, transform.rotation);
        newEel.GetComponent<Rigidbody2D>().velocity = transform.right * eelSpeed;

        StartCoroutine(DestroyEel(newEel));
        StartCoroutine(SpawnEel());
    }

    private IEnumerator DestroyEel(GameObject eel)
    {
        yield return new WaitForSeconds(eelLifetime);
        Destroy(eel);
    }

}
