using DevExpress.CodeParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
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
    [DefaultClassOptions]
    [NavigationItem("Storage")]
    public class Area : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public Area(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
           
           

        }
        /// <summary>
        /// Площадка
        /// </summary>
        private string fAreaNumbers;
        public string AreaNumbers
        {
            get { return fAreaNumbers; }
            set
            {
                SetPropertyValue(nameof(AreaNumbers), ref fAreaNumbers, value);                            
            }
        }

        /// <summary>
        /// Вес пикета
        /// </summary>
        private int fAreaWeight;
        public int AreaWeight
        {
            get { return fAreaWeight; }
            set
            {
                SetPropertyValue(nameof(AreaWeight), ref fAreaWeight, value);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private Storage fStorage;
        [Association("Storage-Areas")]
        public Storage Storage
        {
            get { return fStorage; }
            set
            {
                SetPropertyValue(nameof(Storage), ref fStorage, value);
            }
        }

        [Association("Area-NumberAreas")]
        public XPCollection<WeightArea> NumberAreas
        {
            get
            {
                return GetCollection<WeightArea>(nameof(NumberAreas));
            }
        }


    }
}