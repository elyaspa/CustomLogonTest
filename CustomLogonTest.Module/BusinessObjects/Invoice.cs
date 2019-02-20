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
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Invoice : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Invoice(Session session)
            : base(session)
        {
           
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //if (SecuritySystem.CurrentUser != null)
            //{
            //    Employees = Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId) as Employee;
                
            //}

            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

       

        Company company;
        Employee employees;
        string code;
        string name;

        [Association("Company-Invoices")]
        public Company Company
        {
            get
            {
                return company;
            }
            set
            {
                SetPropertyValue(nameof(Company), ref company, value);
            }
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get
            {
                return name;
                
            }
            set
            {
                SetPropertyValue(nameof(Name), ref name, value);
            }
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                SetPropertyValue(nameof(Code), ref code, value);
            }
        }
        [Association("Invoice-Products")]
        public XPCollection<Product> Products
        {
            get
            {
                return GetCollection<Product>(nameof(Products));
            }
        }
        [Association("Employee-Invoice")]
        public Employee Employees
        {
            get
            {
                return employees;
            }
            set
            {
                SetPropertyValue(nameof(Employees), ref employees, value);
            }
        }
       
    }
}