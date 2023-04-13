using System.Reflection;
using System.Windows.Forms;

namespace TextBoxLoggerProject
{
    /// <summary>
    /// フォームに表示されているTextboxにログを出力するクラス
    /// </summary>
    public class TextBoxLogger
    {

        /// <summary>
        /// ログを出力するテキストボックス
        /// </summary>
        public static TextBox? TextBox { get; set; }

        /// <summary>
        /// ログを出力するテキストファイルのパス
        /// </summary>
        /// <remarks>
        /// nullにすることでログを出力しないこともできる
        /// </remarks>
        public static string? TxtPath { get; set; } = GetDefaultTxtPath();



        private static readonly SemaphoreSlim semaphore = new(1);

        /// <summary>
        /// 空のコンストラクター
        /// </summary>
        public TextBoxLogger()
        {
        }

        /// <summary>
        /// TextBoxLoggerが使用するテキストボックスを設定して初期化
        /// </summary>
        /// <param name="textBox">ログを出力するテキストボックス</param>
        public TextBoxLogger(TextBox textBox) : base()
        {
            TextBox = textBox;
        }


        private static readonly object _lockObjectForLog = new object();

        /// <summary>
        /// TextBoxにログを出力する
        /// </summary>
        /// <param name="message">入力するメッセージ</param>
        public static void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            lock (_lockObjectForLog)
            {
                Console.WriteLine(message);
                if (TextBox is null) return;
                if (TextBox.InvokeRequired)
                {
                    // 別スレッドで使用時にはBeginInvokeを使用する
                    TextBox?.BeginInvoke(new Action(() => TextBox.AppendText($"{message}{Environment.NewLine}")));
                }
                else TextBox?.AppendText($"{message}{Environment.NewLine}");

                WriteMessageToFileAsync(message).Wait();
            }
        }
        /// <summary>
        /// TextBoxにログを出力する
        /// </summary>
        /// <param name="format">string.Format(string, params object[])関数に同じ</param>
        /// <param name="args"></param>
        public static void Log(string format, params object[] args)
        {
            string message = string.Format(format, args);
            Log(message);
        }




        /// <summary>
        /// ログをテキストファイルに出力
        /// </summary>
        /// <param name="message">入力するメッセージ</param>
        /// <returns></returns>
        static async Task WriteMessageToFileAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            if (string.IsNullOrWhiteSpace(TxtPath)) return;

            await semaphore.WaitAsync();

            try
            {
                using var writer = File.AppendText(TxtPath);
                string messageCombined = string.Concat(DateTime.Now.ToString("G"),
                    " : ", message);
                await writer.WriteLineAsync(messageCombined);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to file: {ex.Message}");
                Console.WriteLine($"filepath: {TxtPath}");
                Console.WriteLine($"message: {message}");
#if DEBUG
                Console.WriteLine($"Exception.Message: {ex.Message}");
                Console.WriteLine($"Exception.StackTrace: {ex.StackTrace}");
                throw;
#endif
            }
            finally
            {
                semaphore.Release();
            }
        }


        /// <summary>
        /// テキストファイルのパスを既定値に設定
        /// （カレントディレクトリ）\（プロジェクト名）Log.txt 
        /// </summary>
        static string GetDefaultTxtPath()
        {
            string currentDir = Environment.CurrentDirectory;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string logTextName = string.Concat(assemblyName, "Log.txt");
            string path = Path.Combine(currentDir, logTextName);

            return path;
        }

    }

}

