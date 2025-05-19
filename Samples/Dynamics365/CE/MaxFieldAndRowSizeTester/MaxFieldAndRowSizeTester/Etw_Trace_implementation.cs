using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using System.Diagnostics.Tracing; 

namespace Microsoft.Pfe.Xrm.ConsoleListener
{
    public class EventTextFormatterForConsole : IEventTextFormatter
    {
        public const string DefaultHeader = "******************************************************";
        public const string DefaultFooter = "======================================================";
        public const string DefaultDateTimeFormat = "MM/dd/yy HH:mm:ss.ffff";

        public EventTextFormatterForConsole(EventLevel verbosityLevel = EventLevel.Error)
        {
            this.Header = DefaultHeader;
            this.Footer = DefaultFooter;
            this.DateTimeFormat = DefaultDateTimeFormat;
            this.VerbosityLevel = verbosityLevel;
        }

        public string Header { get; set; }
        public string Footer { get; set; }
        public string DateTimeFormat { get; set; }
        public EventLevel VerbosityLevel { get; set; }

        public void WriteEvent(EventEntry eventEntry, TextWriter writer)
        {
            if (this.VerbosityLevel < eventEntry.Schema.Level
                || this.VerbosityLevel == EventLevel.LogAlways)
            {
                WriteEventLine(eventEntry, writer);
            }
            else if(this.VerbosityLevel >= EventLevel.Verbose)
            {
                WriteEventVerbose(eventEntry, writer);
            }
            else
            {
                WriteEventLine(eventEntry, writer);
            }
        }

        private void WriteEventLine(EventEntry eventEntry, TextWriter writer)
        {
            //writer.WriteLine(this.Header);
            writer.WriteLine($"{eventEntry.GetFormattedTimestamp(this.DateTimeFormat)} TID: {eventEntry.ThreadId.ToString("D5")} {eventEntry.FormattedMessage} ");
            //writer.WriteLine(this.Footer);
        }

        private void WriteEventVerbose(EventEntry eventEntry, TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Header);
            writer.WriteLine();
            writer.WriteLine("Timestamp: {0}", eventEntry.GetFormattedTimestamp(this.DateTimeFormat));
            writer.WriteLine("EventId: {0}", eventEntry.EventId);
            writer.WriteLine("Level: {0}", eventEntry.Schema.Level);
            writer.WriteLine("Keyword: {0}", eventEntry.Schema.KeywordsDescription);
            writer.WriteLine("Task: {0}", eventEntry.Schema.TaskName);
            writer.WriteLine("Message: {0}", eventEntry.FormattedMessage);
            writer.WriteLine("OpCode: {0}", eventEntry.Schema.OpcodeName);
            writer.WriteLine("ActivityId: {0}", eventEntry.ActivityId);
            if (eventEntry.RelatedActivityId != Guid.Empty)
            {
                writer.WriteLine("RelatedActivityId: {0}", eventEntry.RelatedActivityId);
            }
            writer.WriteLine("ProcessId: {0}", eventEntry.ProcessId);
            writer.WriteLine("ThreadId: {0}", eventEntry.ThreadId);
            writer.WriteLine();
            writer.WriteLine(this.Footer);
            writer.WriteLine();
        }

        private void WriteEventToConsole(EventEntry eventEntry, TextWriter writer)
        {
            writer.WriteLine();
            writer.WriteLine(this.Header);
            writer.WriteLine();
            writer.WriteLine("Timestamp: {0}", eventEntry.GetFormattedTimestamp(this.DateTimeFormat));
            writer.WriteLine("Level: {0}", eventEntry.Schema.Level);
            writer.WriteLine("Message: {0}", eventEntry.FormattedMessage);
            if (eventEntry.RelatedActivityId != Guid.Empty)
            {
                writer.WriteLine("RelatedActivityId: {0}", eventEntry.RelatedActivityId);
            }
            writer.WriteLine("ProcessId: {0}", eventEntry.ProcessId);
            writer.WriteLine("ThreadId: {0}", eventEntry.ThreadId);
            writer.WriteLine();
            writer.WriteLine(this.Footer);
            writer.WriteLine();
        }
    }

}

