using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;


public class TransformWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private const int GWL_STYLE = -16;
    private const uint  WS_POPUP = 0x80000000;
    private const uint WS_CAPTION = 0x00C00000;
    private const uint WS_SYSMENU = 0x00080000;
    private const uint  SWP_FRAMECHANGED = 0x0020;
    private const uint SWP_SHOWWINDOWA = 0x0040;
    
    private const int SPI_SETDESKWALLPAPER = 0x0014;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDCHANGE = 0x02;


    private IntPtr windowHandle;
    private RECT windowRect;
    
    
    
    readonly string path01 = @"C:\Users\jon10\Desktop\Image\GameEnd.png";
    readonly string path02 = @"C:\Users\jon10\Desktop\Image\GameEnd2.png";

    string path03 = @"C:\Users\ILoveYou\Desktop\Unity\CK\Image\Back04.png";
    //string path03 = @"C:\Unity\CK\Image\Back03.png";
    //string path04 = @"C:\Unity\CK\Image\Back04.png";



    /*
     * TODO : WallPaper Icon 위치 옮기기 위치 따서 하기
     * 01. 이미지 캡쳐후 AI 분석 (에바)
     * 02. root 접근 디펜스가 막음 
     * 03. shell32.dll
     */

    //TEST code

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOMOVE = 0x0002;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_SHOWWINDOW = 0x0040;
    
    private void Awake()
    {
        // 현재 활성화된 윈도우 핸들 가져오기
        // IntPtr windowHandle = GetActiveWindow();

        string imageFile = Path.Combine(Application.dataPath, "Image", "Back01.png");
        bool fileExist = File.Exists(imageFile);

        //if (fileExist)
        //{
        //    Debug.Log("Success WallPaper File");
        //}
        //else
        //{
        //    Debug.LogError("Fail WallPaper File--------");
        //}
        
    }

 
    
    //보스전
    //폴더 보스 방어막으로 하고 금가고 부서지게 한번 열ㅇ었던 폴더 는 못쓰게.
    //그 그림판 열어서 그림그려서 추가하기
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IntPtr recycleBinHandle = FindRecycleBinIcon();

            if (recycleBinHandle != IntPtr.Zero)
            {
                // 아이콘 위치 변경
                MoveIconToPosition(recycleBinHandle, UnityEngine.Random.Range(0,1000), UnityEngine.Random.Range(0,1000));
            }
            
            // if (GetWindowRect(windowHandle, out windowRect))
            // {
            //     // 창 모드로 변경하기
            //     SetWindowLong(windowHandle, GWL_STYLE, WS_POPUP | WS_CAPTION | WS_SYSMENU);
            //     SetWindowPos(windowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top, 640, 360, SWP_FRAMECHANGED | SWP_SHOWWINDOW);
            // }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TransformWallpaper();
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    public void Manager_TransforWallpaper()
    {
        SetDesktopWallpaper(path01);
    }

    public void Manager_2TransforWallpaper()
    {
        SetDesktopWallpaper(path02);
    }

    public void Manager_TextBox()
    {
        ShowMessageBox("감사해요...", "@../Player/Character/주인공", MessageBoxType.OK);
    }

    public void ShowMessageBox(string message, string title, MessageBoxType boxType)
    {
        uint type = (uint)boxType;
        MessageBox(IntPtr.Zero, message, title, type);
    }

    public enum MessageBoxType
    {
        OK = 0x00000000,
        OKCancel = 0x00000001,
        AbortRetryIgnore = 0x00000002,
        YesNoCancel = 0x00000003,
        YesNo = 0x00000004,
        RetryCancel = 0x00000005,
        CancelTryAgainContinue = 0x00000006,
    }

    private void TransformWallpaper()
    {

        SetDesktopWallpaper(path01);
    }

    public void SetDesktopWallpaper(string imagePath)
    {
        
        int result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

        if (result != 0)
        {
            Debug.Log("desktop wallpaper successfully.");
        }
        else
        {
            Debug.LogError("Failed change desktop wallpaper.----");
        }
    }
    
    
    private IntPtr FindRecycleBinIcon()
    {
        IntPtr recycleBinHandle = IntPtr.Zero;
        
        // 바탕화면 핸들 가져오기
        IntPtr desktopHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Progman", null);
        
        // 바탕화면 자식 윈도우에서 쓰레기통 아이콘 핸들 찾기
        if (desktopHandle != IntPtr.Zero)
        {
            IntPtr shellHandle = FindWindowEx(desktopHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
            if (shellHandle != IntPtr.Zero)
            {
                IntPtr folderViewHandle = FindWindowEx(shellHandle, IntPtr.Zero, "SysListView32", "FolderView");
                if (folderViewHandle != IntPtr.Zero)
                {
                    recycleBinHandle = FindWindowEx(folderViewHandle, IntPtr.Zero, "SysListView32", null);
                }
            }
        }
        
        return recycleBinHandle;
        
    }

    private void MoveIconToPosition(IntPtr iconHandle, int x, int y)
    {
        SetWindowPos(iconHandle, IntPtr.Zero, x, y, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
    }


}