using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotesCount : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int CountNotes;
    public int MaxNotes;

    public void UpdateScore()
    {
        Text.text = $"Notes : {CountNotes} / {MaxNotes} ";
    }
    private void Update()
    {
        UpdateScore();
    }
}
