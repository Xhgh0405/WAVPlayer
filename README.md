# HW3_WAVPlayer

## 專案簡介

`HW3_WAVPlayer` 是一個使用 **C# Windows Forms** 製作的 WAV 音效檔播放器。程式可以播放 `.wav` 音效檔，並提供「播放一次」、「重複播放」、「停止播放」與「結束程式」等基本功能。

本專案也加入了簡單的鋼琴鍵動畫效果：當音樂播放時，下方的鋼琴鍵會隨機閃動與下壓，讓播放器在播放音效時具有視覺回饋。

---

## 功能特色

- 可選擇本機 `.wav` 音效檔播放
- 內建預設音效檔 `XMAS.WAV`
- 支援播放一次
- 支援重複播放
- 支援停止播放
- 支援結束程式
- 播放時鋼琴鍵會產生動畫效果
- 使用 Windows Forms 建立圖形化介面
- 使用 `System.Media.SoundPlayer` 播放 WAV 音效
- 使用 `Timer` 控制鋼琴鍵動畫

---

## 開發環境

| 項目 | 內容 |
|---|---|
| 開發工具 | Visual Studio |
| 程式語言 | C# |
| 專案類型 | Windows Forms App |
| 目標框架 | .NET Framework 4.7.2 |
| 作業系統 | Windows |

---
## 執行說明

### 使用 Visual Studio 執行

1. 先下載或 clone 本專案到電腦。
2. 使用 Visual Studio 開啟專案中的方案檔：

```text
HW3_WAVPlayer.sln
```

3. 開啟後，確認 Visual Studio 上方的執行設定為：

```text
Debug / Any CPU
```

4. 按下上方的「開始」按鈕，或直接按鍵盤快捷鍵：


5. 程式啟動後，即可使用 WAV 播放器。
   
執行畫面
<img width="946" height="430" alt="image" src="https://github.com/user-attachments/assets/a4b9b52d-0d8d-4066-9bb7-373427459a80" />


---

## 專案結構

```text
HW3_WAVPlayer/
├── HW3_WAVPlayer.sln
├── HW3_WAVPlayer.csproj
├── Program.cs
├── frmWAVPlayer.cs
├── Properties/
│   └── AssemblyInfo.cs
├── sample_media/
│   └── XMAS.WAV
├── screenshots/
│   └── main_window.png
├── .gitignore
└── README.md
