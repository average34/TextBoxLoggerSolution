using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextBoxLoggerProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Runtime.CompilerServices;

namespace TextBoxLoggerProject.Tests
{
    [TestClass()]
    public class TextBoxLoggerTests
    {
        [TestMethod()]
        public void LogTest()
        {

            //var form = new MainForm();
            //form.Show();

            Task.Run(() =>
            {
                var form = new MainForm();
                form.ShowDialog();
            });
            Task.Delay(1000).Wait();
            TextBoxLogger.Log("test 1");
            TextBoxLogger.Log("test 2");

            // Assert
            Assert.AreEqual(2, TextBoxLogger.TextBox.Lines.Length - 2);
            //最初に「起動完了」が追加され、最後に改行文字が追加される。
            //そのためLogを呼び出した値より2高い数字が実際の行数である。
        }

        [TestMethod()]
        public async Task LogAsyncTest()
        {

            Task.Run(() =>
            {
                var form = new MainForm();
                form.ShowDialog();
            });

            await Task.Delay(1000);
            TextBoxLogger.Log("Sync 1st");


            await Task.Run(async () =>
            {
                Console.WriteLine("task start");
                TextBoxLogger.Log($"Thread log 1");
                await Task.Delay(1000);
                TextBoxLogger.Log($"Thread log 2");

                TextBoxLogger.Log($"Thread log 3");
                Console.WriteLine("task end");

            });


            TextBoxLogger.Log("test sync 2nd");

            Task.Delay(5000).Wait();

            // Assert
            Assert.AreEqual(6, TextBoxLogger.TextBox.Lines.Length - 2);
        }

        [TestMethod()]
        public async Task TestLogIsThreadSafe()
        {
            // Setup
            Task.Run(() =>
            {
                var form = new MainForm();
                form.ShowDialog();
            });

            await Task.Delay(1000);

            List<Task> tasks = new();
            var rand = new Random();

            // Action
            int numThreads = 13;
            int numLogsPerThread = 7;
            int totalNumLogs = numThreads * numLogsPerThread;

            // Act
            for (int i = 0; i < numThreads; i++)
            {

                int thread = i;
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < numLogsPerThread; j++)
                    {
                        int randomNumber = rand.Next(1, 1001);
                        Task.Delay((int)randomNumber).Wait();
                        int currentId = Environment.CurrentManagedThreadId;
                        TextBoxLogger.Log($"Thread i:{thread} j:{j} ID:{currentId} rand:{randomNumber}");
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            // Assert
            Assert.AreEqual(totalNumLogs, TextBoxLogger.TextBox.Lines.Length - 2);
        }






    }
}