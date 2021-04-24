using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App
{
    public class ScoreUI : MonoBehaviour
    {
        public Player player;
        public TMP_Text score;

        private void Update()
        {
            score.text = $"{player.minionsCollected}";
        }
    }
}
