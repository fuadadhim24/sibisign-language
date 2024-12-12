using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TimerSettings : MonoBehaviour
{
    public TMP_Text TextTimer; // Timer display
    public TMP_Text TextLevel; // Level display
    public TMP_Text TextScore; // Score display
    public Image QuestionImage; // Image for the question
    public List<Button> AnswerButtons; // Buttons for answer choices
    public List<Sprite> LetterImages; // Images for the letters (A-Z)
    public float waktu; // Timer for each question
    public int level = 1; // Current level
    public int nyawa = 5; // Lives available
    public int score = 0; // Player's score
    public int soal = 0; // Current question number
    public List<Image> nyawaImages; // List of life images

    private int correctAnswerIndex; // Index of the correct answer
    private List<int> resultSoal = new List<int>(); // Generated questions
    private List<int> resultJawaban = new List<int>(); // Generated answers
    public int jumlahSoal = 1; // Count of generated questions
    private bool levelIsDone = false; // Check if level is completed

    void Start()
    {
        if (LetterImages.Count < 25)
        {
            Debug.LogError("Not enough images for all letters (A-Z). Please add 25 images.");
            return;
        }

        SetupLevel(level);
        UpdateNyawaImages();
        UpdateLevelText();
        UpdateScoreText();
        GenerateSoal(jumlahSoal); // Generate 4 questions initially
        GenerateQuestion();
    }

    void SetupLevel(int currentLevel)
    {
        if (currentLevel == 1)
        {
            waktu = 60; // 1 minute for Level 1
        }
        else if (currentLevel >= 2 && currentLevel <= 5)
        {
            waktu = 60; // Reset to 1 minute for levels 2-5
            jumlahSoal++;
        }
        else if (currentLevel >= 6)
        {
            waktu = Mathf.Max(10, 60 - (currentLevel - 5) * 5); // Reduce time by 5 seconds per level after 5
        }

        Debug.Log("Level " + currentLevel + " initialized with time: " + waktu + " seconds");
    }

    void UpdateNyawaImages()
    {
        for (int i = 0; i < nyawaImages.Count; i++)
        {
            nyawaImages[i].enabled = i < nyawa; // Enable images only up to the current life count
        }
    }

    void UpdateLevelText()
    {
        if (TextLevel != null)
        {
            TextLevel.text = "Level: " + level.ToString();
        }
    }

    void Update()
    {
        if (waktu > 0)
        {
            waktu -= Time.deltaTime; // Decrease timer
            UpdateTimerDisplay();
        }
        else if (waktu <= 0 && nyawa > 0)
        {
            nyawa--;
            UpdateNyawaImages();
            GenerateSoal(4); // Reset questions
            GenerateQuestion(); // Generate a new question
            SetupLevel(level); // Reset the timer for the level
        }
        else if (nyawa <= 0)
        {
            Debug.Log("Game Over");
            // Add game over logic here
        }
    }

    void UpdateTimerDisplay()
    {
        int Menit = Mathf.FloorToInt(waktu / 60); // Calculate minutes
        int Detik = Mathf.FloorToInt(waktu % 60); // Calculate seconds
        TextTimer.text = Menit.ToString("00") + ":" + Detik.ToString("00"); // Display time
    }

    void UpdateScoreText()
    {
        if (TextScore != null)
        {
            TextScore.text = "Score: " + score.ToString();
        }
    }

    void GenerateSoal(int jumlahSoal)
    {
        resultSoal.Clear();
        resultJawaban.Clear();
        this.jumlahSoal = 0;

        for (int i = 0; i < jumlahSoal; i++)
        {
            int soalIndex;

            do
            {
                soalIndex = Random.Range(0, LetterImages.Count);
            } while (resultSoal.Contains(soalIndex));

            resultSoal.Add(soalIndex);
            resultJawaban.Add(soalIndex); // Add correct answer for each question
            this.jumlahSoal++;
        }

        Debug.Log("Questions generated: " + this.jumlahSoal);
    }

    void GenerateQuestion()
    {
        if (resultSoal.Count == 0)
        {
            Debug.LogWarning("No more questions available for this level.");
            return;
        }

        // Set the current question
        correctAnswerIndex = resultSoal[0];
        QuestionImage.sprite = LetterImages[correctAnswerIndex];

        // Shuffle and assign options
        List<int> options = new List<int> { correctAnswerIndex };
        while (options.Count < 4)
        {
            int randomIndex = Random.Range(0, LetterImages.Count);
            if (!options.Contains(randomIndex))
            {
                options.Add(randomIndex);
            }
        }

        options.Shuffle(); // Custom shuffle function for lists

        for (int i = 0; i < AnswerButtons.Count; i++)
        {
            int optionIndex = options[i];
            AnswerButtons[i].GetComponent<Image>().sprite = LetterImages[optionIndex];

            // Assign correct or incorrect logic to the button
            AnswerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            if (optionIndex == correctAnswerIndex)
            {
                AnswerButtons[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                AnswerButtons[i].onClick.AddListener(IncorrectAnswer);
            }
        }

        resultSoal.RemoveAt(0); // Remove the current question from the list
    }

    void ResetQuestion()
    {
        // Clear listeners to prevent multiple calls
        foreach (var button in AnswerButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        if (resultSoal.Count > 0)
        {
            GenerateQuestion(); // Generate the next question
        }
        else
        {
            Debug.Log("Level Completed");
            levelIsDone = true;
            // Add logic to move to the next level or restart
        }
    }

    public void CorrectAnswer()
    {
        score += 100;
        UpdateScoreText();
        soal++;
        Debug.Log("Correct! Moving to the next question.");
        ResetQuestion();
    }

    public void IncorrectAnswer()
    {
        nyawa--;
        UpdateNyawaImages();

        if (nyawa > 0)
        {
            Debug.Log("Incorrect! Remaining lives: " + nyawa);
            ResetQuestion();
        }
        else
        {
            Debug.Log("Game Over");
            // Add game over logic here (e.g., show Game Over screen)
        }
    }
}

// Extension method for shuffling a list
public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
