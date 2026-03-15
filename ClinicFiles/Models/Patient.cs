using System;

namespace ClinicFiles.Models
{
    public class Patient
    {
        public string ID { get; set; }            // Use string or int, your choice
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalHistory { get; set; }
        public string FileNumber { get; set; }
        public DateTime DateAdded { get; set; }


    }
}