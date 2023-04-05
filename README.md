# TextBoxLoggerSolution

## ダウンロード
[TextboxLogger.cs](<https://github.com/average34/TextBoxLoggerSolution/blob/master/TextBoxLoggerProject/TextboxLogger.cs>)  
をコピーして好きなところで使おう

## 使い方

起動時などに

``` C#
var logger = new TextBoxLogger(this.textBoxLog);

TextBoxLogger.Log("起動完了");
```
と書くことで好きな場所でログを書き、テキストボックスに出力できる  
同時に「（プログラム名）Log.txt」のテキストファイルにもログを出力する  
またConsole.WriteLine()もしているので標準出力にも出力する

詳しい使い方は以下のコードを参照  
[MainForm.cs](<https://github.com/average34/TextBoxLoggerSolution/blob/master/TextBoxLoggerProject/MainForm.cs>)  
[TextBoxLoggerTests.cs](<https://github.com/average34/TextBoxLoggerSolution/tree/master/TextBoxLoggerProjectTests>)

## ライセンス
CC0  
商用利用・改変ご自由に  
