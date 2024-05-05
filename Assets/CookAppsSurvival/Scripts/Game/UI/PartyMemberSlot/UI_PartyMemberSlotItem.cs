using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CookApps.Game
{
    public class UI_PartyMemberSlotItem : MonoBehaviour
    {
        [SerializeField] private Image image_Face;
        [SerializeField] private TextMeshProUGUI text_Level;
        [SerializeField] private TextMeshProUGUI text_Name;
        [SerializeField] private TextMeshProUGUI text_ATK;

        private AgentTemplate _agentTemplate;

        public void Initialize(AgentTemplate agentTemplate)
        {
            _agentTemplate = agentTemplate;

            image_Face.sprite = agentTemplate.face;
            text_Level.text = "LV. 1";
            text_Name.text = agentTemplate.displayName;
            text_ATK.text = "" + agentTemplate.ATK;
        }
    }
}