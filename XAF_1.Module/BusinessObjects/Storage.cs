using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XAF_1.Module.BusinessObjects
{
    [NavigationItem("Storage")]
    [DefaultClassOptions]
    public class Storage : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Storage(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        string numberSegment;
        string numberStorage;




        /// <summary>
        /// Номер склада
        /// </summary>
        [RuleUniqueValue("", DefaultContexts.Save,
        CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)] //поле уникальное
        public string NumberStorage
        {
            get => numberStorage;
            set => SetPropertyValue(nameof(NumberStorage), ref numberStorage, value);
        }


        /// <summary>
        /// номер площадки
        /// </summary>
        private string fAreaNumbers = null;
        public string AreaNumbers
        {
            get
            {
                if (!IsLoading && !IsSaving && fAreaNumbers == null)
                    UpdateArea(false);
                return fAreaNumbers;
            }
        }



        /// <summary>
        /// вес площадки
        /// </summary>
        private int? fAreaWeight = null;
        public int? AreaWeight
        {
            get
            {
                if (!IsLoading && !IsSaving && fAreaWeight == null)
                    UpdateAreaWeight(false);
                return fAreaWeight;
            }
        }


        /// <summary>
        /// вернуть колекцию 
        /// </summary>

        [Association("Storage-Areas"), Aggregated]
        public XPCollection<Area> Areas
        {
            get { return GetCollection<Area>("Areas"); }
        }


        /// <summary>
        /// Обновление площадки
        /// </summary>
        /// <param name="forceChangeEvents"></param>
        public void UpdateArea(bool forceChangeEvents)
        {
            string? oldTotal = fAreaNumbers;
            string tempTotal = "";
            foreach (Area detail in Areas)
                tempTotal += detail.AreaNumbers;
            fAreaNumbers = tempTotal;
            if (forceChangeEvents)
                OnChanged("AreaNumbers", oldTotal, fAreaNumbers);
        }

        /// <summary>
        /// Обновление всего веса
        /// </summary>
        /// <param name="forceChangeEvents"></param>
        public void UpdateAreaWeight(bool forceChangeEvents)
        {
            int? oldTotal = fAreaWeight;
            int tempTotal = 0;
            foreach (Area detail in Areas)
                tempTotal += detail.AreaWeight;
            fAreaWeight = tempTotal;
            if (forceChangeEvents)
                OnChanged("AreaWeight", oldTotal, fAreaWeight);
        }
               
        protected override void OnLoaded()
        {
            Reset();
            base.OnLoaded();
        }
        private void Reset()
        {            
            fAreaWeight = null;            
        }
        
    }
}