﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TrafikApi.Utility
{
    public class Json
    {
        public string CallPythonInCSharp(string filename, string pythonpath, string arguments)
        {
            //string fileName = @"C:\Users\andih\PycharmProjects\Clustering\Test.py"; // Path to your Python script
            //ProcessStartInfo start = new ProcessStartInfo(@"C:\Users\andih\AppData\Local\Programs\Python\Python36-32\python.exe"); // Path to your Python environment

            //start.Arguments = fileName + " " + "First argument" + " " + "Second argument"; // First argument must be the Python script name, then afterwards each argument as a string
            string result = "";
            try
            {
                string fileName = filename; // Path to your Python script
                ProcessStartInfo start = new ProcessStartInfo(pythonpath); // Path to your Python environment

                start.Arguments = fileName + " " + arguments; // First argument must be the Python script name, then afterwards each argument as a string
                start.UseShellExecute = false;// Do not use OS shell
                start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
                start.RedirectStandardError = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                        System.IO.File.WriteAllText("test4.txt", stderr);
                        result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    }
                }
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("test4.txt", e.Message);
            }
            return result;
        }
    }
}
