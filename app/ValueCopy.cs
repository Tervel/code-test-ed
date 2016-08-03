using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace app {
    public class MyClass {
        [FieldCopy]
        private int _copyValue;
        private int _nonCopyValue;
    }

    internal class FieldCopyAttribute : Attribute {
        /// <summary>
        /// go over all of the fields of src using reflection, and copies
        /// the values of fields which are marked with a [FieldCopy] attribute into dest.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        private static void CopyFields<T>(T dest, T src) {
            Type fieldsType = typeof(MyClass);

            // Select all fields which have the [FieldCopy] attribute using LINQ
            var fields = fieldsType
                .GetFields(BindingFlags.Public | BindingFlags.Instance |
                            BindingFlags.DeclaredOnly | BindingFlags.NonPublic)
                .Where(_ =>
                    _.IsDefined(typeof(FieldCopyAttribute))
                );

            // For every field selected
            foreach (var field in fields) {
                Debug.Print("src {0}: {1}",
                    field.Name, field.GetValue(src));

                // Grab the value of the source field and copy it over to 
                // the equivalent destination field
                var srcValue = field.GetValue(src);
                field.SetValue(dest, srcValue);

                Debug.Print("dest {0}: {1}",
                    field.Name, field.GetValue(dest));
            }
        }

        public static void MainThree(string[] args) {
            var testClassSrc = new MyClass();
            Type fieldsType = typeof(MyClass);
            
            // Set field value of our src class via reflection
            var myFieldInfo = fieldsType.GetField("_copyValue", BindingFlags.NonPublic | BindingFlags.Instance);
            myFieldInfo.SetValue(testClassSrc, 12);

            var testClassDest = new MyClass();

            CopyFields(testClassDest, testClassSrc);
        }
    }
}
