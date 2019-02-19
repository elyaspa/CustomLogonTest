using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using CustomLogonTest.Module.BusinessObjects;

namespace CustomLogonTest.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        /*
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            PermissionPolicyUser sampleUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User"));
            if(sampleUser == null) {
                sampleUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sampleUser.UserName = "User";
                sampleUser.SetPassword("");
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
			// If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }*/
        //added by me
         public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        PermissionPolicyRole administrativeRole = ObjectSpace.FindObject<PermissionPolicyRole>(
            new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
        if (administrativeRole == null) {
            administrativeRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            administrativeRole.Name = SecurityStrategy.AdministratorRoleName;
            administrativeRole.IsAdministrative = true;
        }
        const string adminName = "Administrator";
        Employee administratorUser = ObjectSpace.FindObject<Employee>(
            new BinaryOperator("UserName", adminName));
        if (administratorUser == null) {
            administratorUser = ObjectSpace.CreateObject<Employee>();
            administratorUser.UserName = adminName;
            administratorUser.IsActive = true;
            administratorUser.SetPassword("");
            administratorUser.Roles.Add(administrativeRole);
        }
        PermissionPolicyRole userRole = ObjectSpace.FindObject<PermissionPolicyRole>(
            new BinaryOperator("Name", "User"));
        if (userRole == null) {
            userRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            userRole.Name = "User";
            userRole.AddTypePermission<Employee>(
                SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
            userRole.AddTypePermission<Company>(
                SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
        }
        if (ObjectSpace.FindObject<Company>(null) == null) {
            Company company1 = ObjectSpace.CreateObject<Company>();
            company1.Name = "Company 1";
            company1.Employees.Add(administratorUser);
            Employee user1 = ObjectSpace.CreateObject<Employee>();
            user1.UserName = "Sam";
            user1.SetPassword("");
            user1.Roles.Add(userRole);
            Employee user2 = ObjectSpace.CreateObject<Employee>();
            user2.UserName = "John";
            user2.SetPassword("");
            user2.Roles.Add(userRole);
            Company company2 = ObjectSpace.CreateObject<Company>();
            company2.Name = "Company 2";
            company2.Employees.Add(user1);
            company2.Employees.Add(user2);
        }
        ObjectSpace.CommitChanges();
    }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        /*private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

				defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }*/
    }
}
