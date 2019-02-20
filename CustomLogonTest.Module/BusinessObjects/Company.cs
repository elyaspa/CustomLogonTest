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

namespace CustomLogonTest.Module.BusinessObjects
{
    [DefaultClassOptions]
public class Company : BaseObject {
    public Company(Session session) : base(session) {}
    private string name;
    public string Name {
        get { return name; }
        set { SetPropertyValue("Name", ref name, value); }
    }
    [Association("Company-Employees")]
    public XPCollection<Employee> Employees {
        get { return GetCollection<Employee>("Employees"); }
    }
        [Association("Company-Invoices")]
        public XPCollection<Invoice> Invoices
        {
            get
            {
                return GetCollection<Invoice>(nameof(Invoices));
            }
        }
        [Association("Company-Products")]
        public XPCollection<Product> Products
        {
            get
            {
                return GetCollection<Product>(nameof(Products));
            }
        }
    }
}