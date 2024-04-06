using System;
using System.Collections.Generic;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.InitialProgress
{
    [Serializable]
    public class InitialProgressContent
    { 
        [SpreadsheetPage("InitField")] public List<SlotData> InitialFieldData;

        [SpreadsheetPage("AutoMerge")] public SlotsAutoMergerData AutoMerger;
        [SpreadsheetPage("Anvil")] public AnvilData Anvil;
        [SpreadsheetPage("AnvilAutoRefiller")] public AnvilAutoRefillerData AutoRefiller;
        [SpreadsheetPage("AnvilRefill")] public AnvilRefillData AnvilRefilling;
        [SpreadsheetPage("AnvilAutoUse")] public AnvilAutoUseData AnvilAutoUsing;
        
    }
}