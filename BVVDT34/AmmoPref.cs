using GHPC.AI;
using GHPC.AI.BehaviorTrees;
using GHPC.Effects.Voices;
using GHPC.Utility;
using ModUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BVVDT34
{
    public class AmmoPrefT34 : Module
    {
        public static AmmoPreferencesUs84Scriptable T34AmmoPref; // Custom Doctrinal Reload
        public override void LoadDynamicAssets()
        {
            T34AmmoPref = new AmmoPreferencesUs84Scriptable();
            T34AmmoPref.name = "BVVD T34 Special Ammo Pref";
            T34AmmoPref.PreferencesDescending = new List<AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs>(6)
            {
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             new AmmoPreferencesUs84Scriptable.AmmoPrefEntryUs(),
             };
            ////////// TANKS //////////////////
            T34AmmoPref.PreferencesDescending[0].TargetName = TargetShortNameUs.Tank;
            T34AmmoPref.PreferencesDescending[0].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.Sabot,
                AmmoType.AmmoShortName.Missile,
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.He,
                AmmoType.AmmoShortName.Coax,
            };
        
            ////////// APCs/IFVs //////////////////
            T34AmmoPref.PreferencesDescending[1].TargetName = TargetShortNameUs.Pc;
            T34AmmoPref.PreferencesDescending[1].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.He,
                AmmoType.AmmoShortName.Coax,
                AmmoType.AmmoShortName.Sabot,
                AmmoType.AmmoShortName.Missile,
            };
            ////////// AT //////////////////
            T34AmmoPref.PreferencesDescending[2].TargetName = TargetShortNameUs.Antitank;
            T34AmmoPref.PreferencesDescending[2].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.He,
                AmmoType.AmmoShortName.Coax,
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.Missile,
                AmmoType.AmmoShortName.Sabot,
            };
            ////////// TRUCKS //////////////////
            T34AmmoPref.PreferencesDescending[3].TargetName = TargetShortNameUs.Truck;
            T34AmmoPref.PreferencesDescending[3].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.He,
                AmmoType.AmmoShortName.Coax,
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.Missile,
                AmmoType.AmmoShortName.Sabot,
            };
            ////////// TROOPS //////////////////
            T34AmmoPref.PreferencesDescending[4].TargetName = TargetShortNameUs.Troops;
            T34AmmoPref.PreferencesDescending[4].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.Coax,
                AmmoType.AmmoShortName.He,
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.Missile,
                AmmoType.AmmoShortName.Sabot,
            };
            ////////// HELIS //////////////////
            T34AmmoPref.PreferencesDescending[5].TargetName = TargetShortNameUs.Chopper;
            T34AmmoPref.PreferencesDescending[5].AmmoPrefsDescending = new List<AmmoType.AmmoShortName>(5)
            {
                AmmoType.AmmoShortName.Coax,
                AmmoType.AmmoShortName.Missile,
                AmmoType.AmmoShortName.Sabot,
                AmmoType.AmmoShortName.Heat,
                AmmoType.AmmoShortName.He,
            };
            ////////////////////////////////
            ///
        }
    }
}
