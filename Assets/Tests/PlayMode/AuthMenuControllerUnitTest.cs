using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SMB.MainMenu;
using System.Reflection;

namespace SMB.Tests
{
    public class AuthMenuControllerUnitTest
    {
        private AuthMenuController _Controller;

        public AuthMenuControllerUnitTest()
        {
            GameObject go = new GameObject();
            _Controller = go.AddComponent<AuthMenuController>();
        }

        #region Unit Test 1:  _RestrictInputToAlphanumericChars(string text)
        [Test]
        public void TestStringSanitizeMethod_Path1()
        {
            /* AuthMenuController._RestrictInputToAlphanumericChars(string text)
             * Unit Test 1.
             * Input: empty string.
             * Expected Output: empty string.
             */
            string input = "";
            string expectedOutput = "";
            string output = _CallStringSanitizeMethod(_Controller, input);
            Assert.AreEqual(output, expectedOutput);
        }

        [Test]
        public void TestStringSanitizeMethod_Path2()
        {
            /* AuthMenuController._RestrictInputToAlphanumericChars(string text)
             * Unit Test 1.
             * Input: a string only consisting of non-alphanumeric characters.
             * Expected Output: empty string.
             */
            string input = " !@#$%^&*()_+-=`~[]{}\\|;:'\"/?.,<>ϷϿԔ";
            string expectedOutput = "";
            string output = _CallStringSanitizeMethod(_Controller, input);
            Assert.AreEqual(output, expectedOutput);
        }

        [Test]
        public void TestStringSanitizeMethod_Path3()
        {
            /* AuthMenuController._RestrictInputToAlphanumericChars(string text)
             * Unit Test 1.
             * Input: a string consisting of at least one alphanumeric character.
             * Expected Output: empty string.
             */
            string input = "1'or'1'='1";
            string expectedOutput = "1or11";
            string output = _CallStringSanitizeMethod(_Controller, input);
            Assert.AreEqual(output, expectedOutput);
        }

        private string _CallStringSanitizeMethod(AuthMenuController instance, string input)
        {
            // Need to use reflection here because the sanitize method is private.
            MethodInfo sanitizeMethod = instance.GetType().GetMethod(
                name: "_RestrictInputToAlphanumericChars",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            return (string)sanitizeMethod.Invoke(instance, new object[] { input });
        }
        #endregion
    }
}
