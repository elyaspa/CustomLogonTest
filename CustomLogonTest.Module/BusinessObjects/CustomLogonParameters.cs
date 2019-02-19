using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogonTest.Module.BusinessObjects
{
   [DomainComponent, Serializable]
    [System.ComponentModel.DisplayName("Log In")]
    public class CustomLogonParameters : INotifyPropertyChanged, ISerializable {
        private Company company;
        private Employee employee;
        private string password;

        [ImmediatePostData]
        public Company Company {
            get { return company; }
            set {
                if (value == company) return;
                company = value;
                Employee = null;
                OnPropertyChanged("Company");
            }
        }
        [DataSourceProperty("Company.Employees"), ImmediatePostData]
        public Employee Employee {
            get { return employee; }
            set {
                if (Company == null) return;
                employee = value;
                if (employee != null) {
                    UserName = employee.UserName;
                }
                OnPropertyChanged("Employee");
            }
        }
        [Browsable(false)]
        public String UserName { get; set; }
        [PasswordPropertyText(true)]
        public string Password {
            get { return password; }
            set {
                if (password == value) return;
                password = value;
            }
        }
        public CustomLogonParameters() { }
        // ISerializable  
        public CustomLogonParameters(SerializationInfo info, StreamingContext context) {
            if (info.MemberCount > 0) {
                UserName = info.GetString("UserName");
                Password = info.GetString("Password");
            }
        }
        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [System.Security.SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("UserName", UserName);
            info.AddValue("Password", Password);
        }
    }
}
