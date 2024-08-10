using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameLogic.Utilities
{
    internal class ButtonTranslator
    {
        private const string _russian = "ru";
        private const string _english = "en";
        private const string _turkey = "tr";

        private Dictionary<int, string> _buildings = new Dictionary<int, string>();

        public ButtonTranslator()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            SetTranslationDictionary(YandexGamesSdk.Environment.i18n.lang);
#endif
        }

        public string GetTranslation(int buildingIndex)
        {
            if (_buildings.ContainsKey(buildingIndex))
                return _buildings[buildingIndex];

            throw new Exception("Dont have such building id");
        }

        private void SetTranslationDictionary(string language)
        {
            switch (language)
            {
                case _russian:
                    _buildings.Add(BuildingsHash.BarracksIndex, UiHash.BarracksButtonTextRu);
                    _buildings.Add(BuildingsHash.TowerIndex, UiHash.TowerButtonTextRu);
                    _buildings.Add(BuildingsHash.ResoorceBuildingIndex, UiHash.ResoorceBuildingButtonTextRu);
                    break;

                case _english:
                    _buildings.Add(BuildingsHash.BarracksIndex, UiHash.BarracksButtonTextEn);
                    _buildings.Add(BuildingsHash.TowerIndex, UiHash.TowerButtonTextEn);
                    _buildings.Add(BuildingsHash.ResoorceBuildingIndex, UiHash.ResoorceBuildingButtonTextEn);
                    break;

                case _turkey:
                    _buildings.Add(BuildingsHash.BarracksIndex, UiHash.BarracksButtonTextTr);
                    _buildings.Add(BuildingsHash.TowerIndex, UiHash.TowerButtonTextTr);
                    _buildings.Add(BuildingsHash.ResoorceBuildingIndex, UiHash.ResoorceBuildingButtonTextTr);
                    break;

                default:
                    throw new Exception("No such language");
            }
        }
    }
}