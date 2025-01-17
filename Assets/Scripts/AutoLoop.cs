using System.Collections;
using UnityEngine;

public class AutoLoop : MonoBehaviour
{
    public float speed = 8f;      // Geschwindigkeit des Autos
    public float distance = 40f; // Distanz, die das Auto fährt
    private Vector3 startPosition; // Startposition des Autos
    private bool isReturning = false; // Status, ob das Auto zurückgesetzt wird

    
    void Start()
    {
        // Speichere die Anfangsposition
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isReturning)
        {
            // Bewege das Objekt nach vorne entlang der Z-Achse
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Überprüfe, ob das Auto die gewünschte Distanz erreicht hat
            if (Vector3.Distance(startPosition, transform.position) >= distance)
            {
                StartCoroutine(ResetPosition());
            }
        }
    }

    private IEnumerator ResetPosition()
    {
        isReturning = true;

        // Verstecke das Objekt (simuliert das Verschwinden)
        //gameObject.SetActive(false);

        // Warte einen Moment, bevor das Auto zurückgesetzt wird
        yield return new WaitForSeconds(0.5f);

        // Setze das Auto zurück an die Startposition
        transform.position = startPosition;

        // Zeige das Objekt wieder
        //gameObject.SetActive(true);

        isReturning = false;
    }
}