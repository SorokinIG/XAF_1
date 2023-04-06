using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
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

   
        string numberStorage;




        /// <summary>
        /// Номер склада
        /// </summary>
       
        public string NumberStorage
        {
            get => numberStorage;
            set => SetPropertyValue(nameof(NumberStorage), ref numberStorage, value);
        }        

        /// <summary>
        /// История изменений
        /// </summary>
        private XPCollection<AuditDataItemPersistent> auditTrail;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }       

        [Association("Storage-Areas")]
        public XPCollection<Area> Areas
        {
            get { return GetCollection<Area>("Areas"); }
        }

        [Association("Storage-WeightAreas")]
        public XPCollection<WeightArea> WeightAreas
        {
            get
            {
                return GetCollection<WeightArea>(nameof(WeightAreas));
            }
        }               
    }
}