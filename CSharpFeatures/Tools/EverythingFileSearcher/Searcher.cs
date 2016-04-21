using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Allen.Util.EverythingFileSearcher
{
    public class Searcher
    {
        private const int EVERYTHING_OK = 0;
        private const int EVERYTHING_ERROR_MEMORY = 1;
        private const int EVERYTHING_ERROR_IPC = 2;
        private const int EVERYTHING_ERROR_REGISTERCLASSEX = 3;
        private const int EVERYTHING_ERROR_CREATEWINDOW = 4;
        private const int EVERYTHING_ERROR_CREATETHREAD = 5;
        private const int EVERYTHING_ERROR_INVALIDINDEX = 6;
        private const int EVERYTHING_ERROR_INVALIDCALL = 7;

        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern int Everything_SetSearchW(string lpSearchString);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetRegex(bool bEnable);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetMax(int dwMax);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SetOffset(int dwOffset);

        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchPath();

        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchCase();

        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetMatchWholeWord();

        [DllImport("Everything32.dll")]
        public static extern bool Everything_GetRegex();

        [DllImport("Everything32.dll")]
        public static extern uint Everything_GetMax();

        [DllImport("Everything32.dll")]
        public static extern uint Everything_GetOffset();

        [DllImport("Everything32.dll")]
        public static extern string Everything_GetSearchW();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetLastError();

        [DllImport("Everything32.dll")]
        public static extern bool Everything_QueryW(bool bWait);

        [DllImport("Everything32.dll")]
        public static extern void Everything_SortResultsByPath();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetNumFileResults();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetNumFolderResults();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetNumResults();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetTotFileResults();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetTotFolderResults();

        [DllImport("Everything32.dll")]
        public static extern int Everything_GetTotResults();

        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsVolumeResult(int nIndex);

        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsFolderResult(int nIndex);

        [DllImport("Everything32.dll")]
        public static extern bool Everything_IsFileResult(int nIndex);

        [DllImport("Everything32.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathNameW(int nIndex, StringBuilder lpString, int nMaxCount);

        [DllImport("Everything32.dll")]
        public static extern void Everything_Reset();


        /// <summary>
        ///     使用Everything执行文件搜索
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static IList<string> Search(string queryStr, bool isSortBypath = true)
        {
            int i;
            const int bufsize = 260;
            var buf = new StringBuilder(bufsize);

            // set the search
            Everything_SetSearchW(queryStr);

            // use our own custom scrollbar... 			
            // Everything_SetMax(listBox1.ClientRectangle.Height / listBox1.ItemHeight);
            // Everything_SetOffset(VerticalScrollBarPosition...);

            // execute the query
            Everything_QueryW(true);

            if (isSortBypath)
                Everything_SortResultsByPath();

            var count = Everything_GetNumResults();
            var result = new List<string>(count);


            // loop through the results, adding each result to the listbox.
            for (i = 0; i < count; i++)
            {
                // get the result's full path and file name.
                Everything_GetResultFullPathNameW(i, buf, bufsize);

                // add it to the list box				
                result.Add(buf.ToString());
            }
            return result;
        }
    }
}