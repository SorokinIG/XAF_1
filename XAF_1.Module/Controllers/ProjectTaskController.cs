using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XAF_1.Module.BusinessObjects;




namespace XAF_1.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ProjectTaskController : ViewController
    {
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public ProjectTaskController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            // Specify the type of objects that can use the Controller.
            TargetObjectType = typeof(Area);
            // Activate the Controller in any type of View.
            TargetViewType = ViewType.DetailView;

            PopupWindowShowAction showNotesAction = new PopupWindowShowAction(this, "ShowNotesAction", PredefinedCategory.Edit)
            {
                Caption = "Объеденить площадки"
            };

            showNotesAction.CustomizePopupWindowParams += ShowNotesAction_CustomizePopupWindowParams;

            showNotesAction.Execute += ShowNotesAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            
        }      

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
           
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void ShowNotesAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Create a List View for Note objects in the pop-up window.
            e.View = Application.CreateListView(typeof(WeightArea), true);
        }

        private void ShowNotesAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Area task = (Area)View.CurrentObject;
            List<int> tempArr = new List<int>();
            foreach (WeightArea note in e.PopupWindowViewSelectedObjects)
            {                
               if(searchContainsAreaNumber(task.AreaNumbers, note.Number))
                {
                    tempArr.Add(note.Number);                    
                    task.AreaWeight += note.Weight;
                }                
            }
            tempArr.Sort();
            
            task.AreaNumbers = tempArr.First().ToString() + " - " + tempArr.Last().ToString();

            AuditTrailService.GetService(Application.ServiceProvider).BeginSessionAudit(((XPObjectSpace)View.ObjectSpace).Session, AuditTrailStrategy.OnObjectChanged);
            View.ObjectSpace.Committed += new EventHandler(ObjectSpace_Committed);
            View.ObjectSpace.Reloaded += new EventHandler(ObjectSpace_Reloaded);

            View.ObjectSpace.CommitChanges();
         
        }
        bool searchContainsAreaNumber(string area, int numberPicket)
        {
            if (area == null)
                return true;
            string[] numbers = area.Split(new char[] { '-' });
            int a = Int32.Parse(numbers.First());
            int b = Int32.Parse(numbers.Last());            
            for (int i = 0; i <= b-a; i++)
            {
                if (numberPicket == a + i)
                    return false;
            }       
            return true;
        }
        private void ObjectSpace_Reloaded(object sender, EventArgs e)
        {
            AuditTrailService.GetService(Application.ServiceProvider).EndSessionAudit(((XPObjectSpace)sender).Session);
            AuditTrailService.GetService(Application.ServiceProvider).BeginSessionAudit(((XPObjectSpace)sender).Session, AuditTrailStrategy.OnObjectChanged);
        }
        private void ObjectSpace_Committed(object sender, EventArgs e)
        {
            AuditTrailService.GetService(Application.ServiceProvider).SaveAuditData(((XPObjectSpace)sender).Session);
            AuditTrailService.GetService(Application.ServiceProvider).BeginSessionAudit(((XPObjectSpace)sender).Session, AuditTrailStrategy.OnObjectChanged);
        }
    }
}