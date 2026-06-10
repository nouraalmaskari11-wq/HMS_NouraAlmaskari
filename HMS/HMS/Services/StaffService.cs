using HMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public static class StaffService
    {
        public static void DisplayAllStaff(List<StaffModel> staff)
        {
            foreach (var s in staff)
            {
                Console.WriteLine($"  Staff ID:{s.staffId}, FullName: {s.fullName}, role:{s.role}, isOnDuty:{s.isOnDuty} ");
            }
        }
        public static StaffModel FindStaffById(List<StaffModel> staff, string staffId)
        {
            foreach (var s in staff)
            {
                if (s.staffId == staffId)
                {
                    return s;
                }
            }
            return null;
        }
         public static void ToggleDutyStatus(StaffModel staff)
        {
            staff.isOnDuty =! staff.isOnDuty;

            Console.WriteLine($"New Duty Status: {staff.isOnDuty}");
        }
    }
}

