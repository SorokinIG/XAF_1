using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XAF_1.Module.BusinessObjects
{
    [NavigationItem("Storage")]
    [DefaultClassOptions]
    public class WeightArea : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public WeightArea(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        int weight;
        int number;

        /// <summary>
        /// Номер пикета
        /// </summary>    
        [RuleUniqueValue("", DefaultContexts.Save,
        CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]  //поле уникальное
        public int Number
        {
            get => number;
            set => SetPropertyValue(nameof(Number), ref number, value);
        }


        
        
        public int Weight
        {
            get => weight;
            set => SetPropertyValue(nameof(Weight), ref weight, value);
        }

        bool _isActive = false;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                SetPropertyValue(nameof(IsActive), ref _isActive, value);
            }
        }


    }
}