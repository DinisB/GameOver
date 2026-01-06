using UnityEngine;

public static class PuzzleSaveManager 
{
    // Salva o estágio atual do puzzle do piano
    public static void SavePianoStage(int stage)
    {
        PlayerPrefs.SetInt("PianoPuzzleStage", stage);
    }
    // Da load do estágio atual do puzzle do piano
    public static int LoadPianoStage()
    {
        return PlayerPrefs.GetInt("PianoPuzzleStage", 0);
    }
}