using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace CustomLogonTest.Module.BusinessObjects
{
    
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  [DefaultClassOptions, DefaultProperty("UserName")]
public class Employee : PermissionPolicyUser{
    public Employee(Session session) : base(session) {}
    private Company company;
    [Association("Company-Employees")]
    public Company Company {
        get { return company; }
        set { SetPropertyValue("Company", ref company, value);}
    }
        [Association("Employee-Invoice")]
        public XPCollection<Invoice> Invoice
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoice));
            }
        }
    }
}