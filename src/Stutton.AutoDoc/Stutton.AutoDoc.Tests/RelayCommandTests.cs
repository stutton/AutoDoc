using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stutton.AutoDoc.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stutton.AutoDoc.Tests
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void ExecuteRunsAction()
        {
            var cmdRun = false;
            var cmd = new RelayCommand(() => cmdRun = true);

            cmd.Execute(null);

            Assert.IsTrue(cmdRun);
        }

        [TestMethod]
        public void CanExecuteDefaultsToTrue()
        {
            var cmd = new RelayCommand(Helpers.DoNothingAction);

            var canExecute = cmd.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void ExecuteRunsActionWithParameter()
        {
            var expectedParam = "test";
            string actualParam = null;
            var cmd = new RelayCommand(p => actualParam = p.ToString());

            cmd.Execute(expectedParam);

            Assert.AreEqual(expectedParam, actualParam);
        }

        [TestMethod]
        public void CanExecuteRunsPredicate()
        {
            var predicateRun = false;
            var cmd = new RelayCommand(Helpers.DoNothingAction, p => 
            {
                predicateRun = true;
                return true;
            });

            cmd.CanExecute(null);

            Assert.IsTrue(predicateRun);
        }
    }
}
