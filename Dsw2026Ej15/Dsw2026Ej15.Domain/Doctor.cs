using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Doctor
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsActive { get; set; }
        public Speciality Speciality { get; set; }

        public Doctor (string name, string licenseNum, Speciality speciality)
        {
            Name = name;
            LicenseNumber = licenseNum;
            IsActive = true;
            Speciality = speciality;
        }
    }
}
