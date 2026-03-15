using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using ClinicFiles.Models;

namespace ClinicFiles.Data
{
    public static class PatientData
    {
        private static string filePath = "patients.json";

        // List to hold all patients in memory
        public static List<Patient> Patients = new List<Patient>();

        // Random for generating unique 6-digit file numbers
        private static Random rnd = new Random();

        // Load patients from JSON file
        public static void LoadPatients()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Patients = serializer.Deserialize<List<Patient>>(json) ?? new List<Patient>();
            }
        }

        // Save patients to JSON file
        public static void SavePatients()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(Patients);
            File.WriteAllText(filePath, json);
        }

        // Generate a 6-digit unique FileNumber
        public static string GenerateUniqueFileNumber()
        {
            string newId;

            LoadPatients(); // make sure we check existing FileNumbers

            do
            {
                newId = rnd.Next(100000, 1000000).ToString(); // generates 100000 to 999999
            }
            while (Patients.Exists(p => p.FileNumber == newId));

            return newId;
        }
    }
}