using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/Templates/AgentSkill", fileName = "Skill", order = 0)]
    public class AgentSkillTemplate : ScriptableObject
    {
        [Header("기본정보")]
        public string displayName;
        [TextArea]
        public string description;

        [Space(10)]
        public float cooldownTime;

        [Header("리소스 관련")]
        public Sprite face;        

        //[Header("스킬 구현")]
        [HideInInspector]
        public List<SkillEffect> effects;
    }
}

#if UNITY_EDITOR
namespace CookApps.Editor
{
    using System;
    using UnityEditor;
    using UnityEditorInternal;
    using CookApps.Game;

    [CustomEditor(typeof(AgentSkillTemplate)), CanEditMultipleObjects]
    public class AgentSkillTemplateEditor : EffectEditor
    {
        private AgentSkillTemplate _target;

        private ReorderableList effectsList;
        private SkillEffect currentEffect;

        private void OnEnable()
        {
            _target = target as AgentSkillTemplate;

            CreateEffectList();
        }

        public override void OnInspectorGUI()
        {
            AgentSkillTemplate template = target as AgentSkillTemplate;

            base.OnInspectorGUI();

            effectsList?.DoLayoutList();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(this);
            }
        }

        private void InitMenu_Effects()
        {
            var menu = new GenericMenu();

            menu.AddItem(new GUIContent("즉시 상태이상"), false, CreateEffectCallback, typeof(InstantAbnormalStatusSkillEffect));
            menu.AddItem(new GUIContent("즉시 데미지"), false, CreateEffectCallback, typeof(InstantDamageSkillEffect));
            menu.AddItem(new GUIContent("투사체 데미지"), false, CreateEffectCallback, typeof(ProjectileDamageSkillEffect));
            menu.AddItem(new GUIContent("즉시 회복"), false, CreateEffectCallback, typeof(InstantHealSkillEffect));

            menu.ShowAsContext();
        }

        private void CreateEffectList()
        {
            effectsList = SetupReorderableList("Skill Effects", _target.effects, 
                (rect, x) => 
                { 
                }, 
                (x) => 
                {
                    currentEffect = x;
                },
                () => 
                {
                    InitMenu_Effects();
                },
                (x) =>
                {
                    DestroyImmediate(currentEffect, true);
                    currentEffect = null;
                    EditorUtility.SetDirty(target);
                });

            effectsList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = _target.effects[index];

                if (element != null)
                {
                    rect.y += 2;
                    rect.width -= 10;
                    rect.height = EditorGUIUtility.singleLineHeight;

                    var label = element.GetLabel();
                    EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);
                    rect.y += 5;
                    rect.y += EditorGUIUtility.singleLineHeight;

                    element.Draw(rect);

                    if (GUI.changed)
                    {
                        EditorUtility.SetDirty(element);
                    }
                }
            };

            effectsList.elementHeightCallback = (index) =>
            {
                var element = _target.effects[index];
                return element.GetHeight();
            };
        }

        private void CreateEffectCallback(object obj)
        {
            var effect = ScriptableObject.CreateInstance((Type)obj) as SkillEffect;

            if (effect != null)
            {
                effect.hideFlags = HideFlags.HideInHierarchy;
                _target.effects.Add(effect);

                var template = target as AgentSkillTemplate;
                var path = AssetDatabase.GetAssetPath(template);
                AssetDatabase.AddObjectToAsset(effect, path);
                EditorUtility.SetDirty(template);
            }
        }
    }
}
#endif