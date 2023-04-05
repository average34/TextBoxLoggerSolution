using System.Reflection;

namespace TextBoxLoggerProject
{
    /// <summary>
    /// フォームに表示されているTextboxにログを出力するクラス
    /// </summary>
    public class TextBoxLogger
    {
        private static TextBox? _textBox;
        private static string? _txtPath;

        /// <summary>
        /// ログを出力するテキストボックス
        /// </summary>
        public static TextBox? TextBox
        {
            get { return _textBox; }
            set { _textBox = value; }
        }

        /// <summary>
        /// ログを出力するテキストファイルのパス
        /// </summary>
        /// <remarks>
        /// nullにすることでログを出力しないこともできる
        /// </remarks>
        public static string? TxtPath
        {
            get { return _txtPath; }
            set { _txtPath = value; }

        }

        /// <summary>
        /// 空のコンストラクター
        /// </summary>
        public TextBoxLogger()
        {
            SetTxtPathDefault();
        }

        /// <summary>
        /// TextBoxLoggerが使用するテキストボックスを設定して初期化
        /// </summary>
        /// <param name="textBox">ログを出力するテキストボックス</param>
        public TextBoxLogger(TextBox textBox) : base()
        {
            SetTxtPathDefault();
            TextBox = textBox;
        }



        /// <summary>
        /// TextBoxにログを出力する
        /// </summary>
        /// <param name="message">入力するメッセージ</param>
        public static void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            Console.WriteLine(message);
            if (_textBox is null) return;
            //この関数をUIスレッドとは別スレッドで動かしているかどうか確認する
            if (_textBox.InvokeRequired)
            {
                //別スレッドで使用時にはInvokeを使用する
                _textBox?.Invoke(new Action(() => _textBox.AppendText($"{message}{Environment.NewLine}")));

            }
            else _textBox?.AppendText($"{message}{Environment.NewLine}");

            WriteMessageToFileAsync(message).Wait();
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
            if (string.IsNullOrWhiteSpace(_txtPath)) return;
            try
            {
                using StreamWriter writer = File.AppendText(_txtPath);
                string messageCombined = string.Concat(DateTime.Now.ToString("G"),
                    " : ", message);
                await writer.WriteLineAsync(messageCombined);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to file: {ex.Message}");
                Console.WriteLine($"filepath: {_txtPath}");
                Console.WriteLine($"message: {message}");
#if DEBUG
                Console.WriteLine($"Exception.Message: {ex.Message}");
                Console.WriteLine($"Exception.StackTrace: {ex.StackTrace}");
                throw;
#endif
            }
        }

        /// <summary>
        /// テキストファイルのパスを既定値に設定
        /// （カレントディレクトリ）\（プロジェクト名）Log.txt 
        /// </summary>
        static void SetTxtPathDefault()
        {
            if (_txtPath != null) return;
            string currentDir = Environment.CurrentDirectory;
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string logTextName = string.Concat(assemblyName, "Log.txt");
            string path = Path.Combine(currentDir, logTextName);
            TxtPath = path;

        }

    }

}
