using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NEGOSUDClient.Services;

public class AuthService
{
    public enum UserRole
    {
        Employee,
        Admin
    }

    public static UserRole CurrentRole;

    public AuthService()
    {
        CurrentRole = UserRole.Employee;
    }

    public static void SwitchToAdmin()
    {
        CurrentRole = UserRole.Admin;
        MessageBox.Show("Passage en Admin");
    }

    public static void SwitchToEmploye()
    {
        CurrentRole = UserRole.Employee;
        MessageBox.Show("Passage en Employée");
    }

    public static bool IsAdmin() 
    {
        return CurrentRole == UserRole.Admin;
    }
}
