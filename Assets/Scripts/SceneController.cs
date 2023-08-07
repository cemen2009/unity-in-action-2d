using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Text scoreText;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    private int score = 0;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
            firstRevealed = card;
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private void Start()
    {
        Vector3 startPosition = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard);

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, sprites[id]);

                float posX = (offsetX * i) + startPosition.x;
                float posY = -(offsetY * j) + startPosition.y;
                card.transform.position = new Vector3(posX, posY, startPosition.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    private IEnumerator CheckMatch()
    {
        if (firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
