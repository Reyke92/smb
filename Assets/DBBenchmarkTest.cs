using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using UnityEngine;
using System.Text;
using System.Diagnostics;

namespace SMB.Tests
{
    internal class DBBenchmarkTest
    {
        private const string _CONNECTION_STRING = "";

        private static DBBenchmarkTest _Instance;

        private bool _IsRunning;

        private DBBenchmarkTest() { }

        internal static void Run()
        {
            if (_Instance == null)
            {
                _Instance = new DBBenchmarkTest();
                Application.quitting += _Instance._OnApplicationQuitting;
            }
            else if (_Instance._IsRunning) return;

            UnityEngine.Debug.Log("DB Benchmark Test starting.");

            // Generate random test strings to use for the benchmarks.
            System.Random r = new System.Random();
            byte[] randBytes;
            string[][] randStrings = new string[8][];
            int iterator = 0;
            StringBuilder sb = new StringBuilder();
            for (int sLength = 1; sLength <= 128; sLength *= 2)
            {
                randStrings[iterator] = new string[10]; // 10 tests each.
                randBytes = new byte[sLength];
                for (int stringNum = 0; stringNum < 10; stringNum++)
                {
                    r.NextBytes(randBytes);
                    for (int byteIterator = 0; byteIterator < sLength; byteIterator++)
                    {
                        // Append a random lowercase letter.
                        sb.Append((char)(97 + (randBytes[byteIterator] % 26)));
                    }
                    randStrings[iterator][stringNum] = sb.ToString();
                    sb.Clear();
                }

                iterator++;
            }

            

            // Unsubscribe from the Application Quitting event.
            Application.quitting -= _Instance._OnApplicationQuitting;
            _Instance._IsRunning = false;

            UnityEngine.Debug.Log("DB Benchmark Test done.");
        }

        private void _OnApplicationQuitting()
        {
            _IsRunning = false;
        }
    }
}
