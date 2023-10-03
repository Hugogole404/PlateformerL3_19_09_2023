using UnityEngine;
using TMPro;

public class NotesCount : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int CountNotes;
    public int MaxNotes;

    public void UpdateScore()
    {
        Text.text = $"Memories : {CountNotes} / {MaxNotes} ";
    }
    private void Update()
    {
        UpdateScore();
    }
}
