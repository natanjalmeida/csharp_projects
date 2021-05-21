﻿using System.Text;

namespace RoboSharp
{
    public class LoggingOptions
    {
        internal const string LIST_ONLY = "/L ";
        internal const string REPORT_EXTRA_FILES = "/X ";
        internal const string VERBOSE_OUTPUT = "/V ";
        internal const string INCLUDE_SOURCE_TIMESTAMPS = "/TS ";
        internal const string INCLUDE_FULL_PATH_NAMES = "/FP ";
        internal const string PRINT_SIZES_AS_BYTES = "/BYTES ";
        internal const string NO_FILE_SIZES = "/NS ";
        internal const string NO_FILE_CLASSES = "/NC ";
        internal const string NO_FILE_LIST = "/NFL ";
        internal const string NO_DIRECTORY_LIST = "/NDL ";
        internal const string NO_PROGRESS = "/NP ";
        internal const string SHOW_ESTIMATED_TIME_OF_ARRIVAL = "/ETA ";
        internal const string LOG_PATH = "/LOG:{0} ";
        internal const string APPEND_LOG_PATH = "/LOG+:{0} ";
        internal const string UNICODE_LOG_PATH = "UNILOG:{0} ";
        internal const string APPEND_UNICODE_LOG_PATH = "/UNILOG+:{0} ";
        internal const string OUTPUT_TO_ROBOSHARP_AND_LOG = "/TEE ";
        internal const string NO_JOB_HEADER = "/NJH ";
        internal const string NO_JOB_SUMMARY = "/NJS ";
        internal const string OUTPUT_AS_UNICODE = "/UNICODE ";

        /// <summary>
        /// Do not copy, timestamp or delete any files.
        /// [/L]
        /// </summary>
        public bool ListOnly { get; set; }
        /// <summary>
        /// Report all extra files, not just those selected.
        /// [X]
        /// </summary>
        public bool ReportExtraFiles { get; set; }
        /// <summary>
        /// Produce verbose output, showing skipped files.
        /// [V]
        /// </summary>
        public bool VerboseOutput { get; set; }
        /// <summary>
        /// Include source file time stamps in the output.
        /// [/TS]
        /// </summary>
        public bool IncludeSourceTimeStamps { get; set; }
        /// <summary>
        /// Include full path names of files in the output.
        /// [/FP]
        /// </summary>
        public bool IncludeFullPathNames { get; set; }
        /// <summary>
        /// Print sizes as bytes in the output.
        /// [/BYTES]
        /// </summary>
        public bool PrintSizesAsBytes { get; set; }
        /// <summary>
        /// Do not log file sizes.
        /// [/NS]
        /// </summary>
        public bool NoFileSizes { get; set; }
        /// <summary>
        /// Do not log file classes.
        /// [/NC]
        /// </summary>
        public bool NoFileClasses { get; set; }
        /// <summary>
        /// Do not log file names.
        /// [/NFL]
        /// </summary>
        public bool NoFileList { get; set; }
        /// <summary>
        /// Do not log directory names.
        /// [/NDL]
        /// </summary>
        public bool NoDirectoryList { get; set; }
        /// <summary>
        /// Do not log percentage copied.
        /// [/NP]
        /// </summary>
        public bool NoProgress { get; set; }
        /// <summary>
        /// Show estimated time of arrival of copied files.
        /// [/ETA]
        /// </summary>
        public bool ShowEstimatedTimeOfArrival { get; set; }
        /// <summary>
        /// Output status to LOG file (overwrite existing log).
        /// [/LOG:file]
        /// </summary>
        public string LogPath { get; set; }
        /// <summary>
        /// Output status to LOG file (append to existing log).
        /// [/LOG+:file]
        /// </summary>
        public string AppendLogPath { get; set; }
        /// <summary>
        /// Output status to LOG file as UNICODE (overwrite existing log).
        /// [/UNILOG:file]
        /// </summary>
        public string UnicodeLogPath { get; set; }
        /// <summary>
        /// Output status to LOG file as UNICODE (append to existing log).
        /// [/UNILOG+:file]
        /// </summary>
        public string AppendUnicodeLogPath { get; set; }
        /// <summary>
        /// Output to RoboSharp and Log.
        /// [/TEE]
        /// </summary>
        public bool OutputToRoboSharpAndLog { get; set; }
        /// <summary>
        /// Do not output a Job Header.
        /// [/NJH]
        /// </summary>
        public bool NoJobHeader { get; set; }
        /// <summary>
        /// Do not output a Job Summary.
        /// [/NJS]
        /// </summary>
        public bool NoJobSummary { get; set; }
        /// <summary>
        /// Output as UNICODE.
        /// [/UNICODE]
        /// </summary>
        public bool OutputAsUnicode { get; set; }

        internal string Parse()
        {
            var options = new StringBuilder();

            if (ListOnly)
                options.Append(LIST_ONLY);
            if (ReportExtraFiles)
                options.Append(REPORT_EXTRA_FILES);
            if (VerboseOutput)
                options.Append(VERBOSE_OUTPUT);
            if (IncludeSourceTimeStamps)
                options.Append(INCLUDE_SOURCE_TIMESTAMPS);
            if (IncludeFullPathNames)
                options.Append(INCLUDE_FULL_PATH_NAMES);
            if (PrintSizesAsBytes)
                options.Append(PRINT_SIZES_AS_BYTES);
            if (NoFileSizes)
                options.Append(NO_FILE_SIZES);
            if (NoFileClasses)
                options.Append(NO_FILE_CLASSES);
            if (NoFileList)
                options.Append(NO_FILE_LIST);
            if (NoDirectoryList)
                options.Append(NO_DIRECTORY_LIST);
            if (NoProgress)
                options.Append(NO_PROGRESS);
            if (ShowEstimatedTimeOfArrival)
                options.Append(SHOW_ESTIMATED_TIME_OF_ARRIVAL);
            if (!LogPath.IsNullOrWhiteSpace())
                options.Append(string.Format(LOG_PATH, LogPath));
            if (!AppendLogPath.IsNullOrWhiteSpace())
                options.Append(string.Format(APPEND_LOG_PATH, AppendLogPath));
            if (!UnicodeLogPath.IsNullOrWhiteSpace())
                options.Append(string.Format(UNICODE_LOG_PATH, UnicodeLogPath));
            if (!AppendUnicodeLogPath.IsNullOrWhiteSpace())
                options.Append(string.Format(APPEND_UNICODE_LOG_PATH, AppendUnicodeLogPath));
            if (OutputToRoboSharpAndLog)
                options.Append(OUTPUT_TO_ROBOSHARP_AND_LOG);
            if (NoJobHeader)
                options.Append(NO_JOB_HEADER);
            if (NoJobSummary)
                options.Append(NO_JOB_SUMMARY);
            if (OutputAsUnicode)
                options.Append(OUTPUT_AS_UNICODE);

            return options.ToString();
        }
    }
}
