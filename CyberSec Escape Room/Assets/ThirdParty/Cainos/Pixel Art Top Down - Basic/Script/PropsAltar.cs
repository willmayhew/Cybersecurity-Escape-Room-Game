using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar : ChallengeObject
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor = new Color(1, 1, 1, 0);
        private Color targetColor;

        public string altarID;

        protected override void Start()
        {
            base.Start();

            //if (logic.isChallengeComplete(gameObject))
            //{
            //    CompleteChallenge();
            //}
        }

        protected override void Update()
        {
            base.Update();

            if (!challengeComplete)
            {
                FadeRuins();
            }

        }

        private void FadeRuins()
        {
            if (curColor == new Color(1, 1, 1, 1))
            {
                targetColor = new Color(1, 1, 1, 0);
            }
            else if (curColor == new Color(1, 1, 1, 0))
            {
                targetColor = new Color(1, 1, 1, 1);
            }

            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            SetRuinsColor(curColor);
        }

        private void SetRuinsColor(Color color)
        {
            foreach (var r in runes)
            {
                r.color = color;
            }
        }

        public override void CompleteChallenge()
        {

            base.CompleteChallenge();
            SetRuinsColor(new Color(1,1,1,1));

        }

    }
}
