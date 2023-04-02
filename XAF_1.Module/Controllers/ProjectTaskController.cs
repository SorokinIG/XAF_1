using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
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
                Caption = "Show Notes"
            };

            showNotesAction.CustomizePopupWindowParams += ShowNotesAction_CustomizePopupWindowParams;

            showNotesAction.Execute += ShowNotesAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            
        }

      

        private void CustomizeList(ICollection<Type> types)
        {
            List<Type> unusableTypes = new List<Type>();
            foreach (Type item in types)
            {
                if (item == typeof(Task))
                {
                    unusableTypes.Add(item);
                }
            }
            foreach (Type item in unusableTypes)
            {
                types.Remove(item);
            }
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
            foreach (WeightArea note in e.PopupWindowViewSelectedObjects)
            {
                

               if(note.IsActive && searchContainsAreaNumber(task.AreaNumbers, note.Number.ToString()))
                {
                    task.AreaNumbers += "," + note.Number.ToString();
                    task.AreaWeight += note.Weight;
                }
                
            }
            View.ObjectSpace.CommitChanges();
        }
        bool searchContainsAreaNumber(string area, string numberPicket)
        {
            string[] numbers = area.Split(new char[] { ',' });

            foreach (string s in numbers)
            {
                if(s.Contains(numberPicket))
                    return false;
            }
            return true;
        }
    }
}
