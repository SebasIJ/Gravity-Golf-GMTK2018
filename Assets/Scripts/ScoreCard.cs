using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCard : MonoBehaviour {

    private static int totalScore;

    //Added list to contain all scores instead of adding them all losing individual scores
    private static List<int> scores = new List<int>();

    public static int TotalScore {

        get {
            //addition starts here
            totalScore = 0;

            for (int i = 0; i < scores.Count; i++)
            {
                totalScore += scores[i];
            }
            //addition ends here

            return totalScore;
        }
        set {
            //commented original code
            //totalScore = value;

            //new code
            scores.Add(value);
        }
    }
}
