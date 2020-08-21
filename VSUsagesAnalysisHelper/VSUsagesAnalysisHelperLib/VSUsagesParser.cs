using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSUsagesAnalysisHelperLib
{
    public class VSUsagesParser
    {
        private Dictionary<string, int> _header;
        private Dictionary<int, string> _headerRev;
        private static readonly string NoReferencesClause = "No references found to";

        public List<VSUsageRec> Parse(string vsSearchResult)
        {
            List<VSUsageRec> rslt = new List<VSUsageRec>();
            List<string> lines = new List<string>(SplitLines(vsSearchResult));
            if (lines.Count == 0)
                return rslt;
            FillHeader(lines[0]);
            string lastNode = null;
            for (int i = 1; i < lines.Count; i++)
            {
                string[] currFields = SplitLine(lines[i]);
                if (IsNodeLine(currFields))
                {
                    lastNode = lines[i].Trim();
                    continue;
                }

                VSUsageRec rec;
                if (TryParseRec(lines[i], out rec))
                {
                    rec.UsageNode = lastNode;
                    rec.DefMember = ParseDefMethod(lastNode);
                    if (string.IsNullOrEmpty(rec.DefMember) && rec.IsNoReferences)
                        rec.DefMember = ParseDefMethodFromNoRef(rec.Code);
                    rslt.Add(rec);
                }
            }
            return rslt;
        }


        private bool IsNodeLine(string[] fields)
        {
            if (fields.Length < 2)
                return true;
            int totalLength = 0;
            if (fields[0].Trim().Length > 0)
            {
                for (int i = 1; i < fields.Length; i++)
                {
                    totalLength += fields[i].Length;
                }
                if (totalLength == 0)
                    return true;
            }
            return false;
        }

        private string ParseDefMethod(string nodeText)
        {
            if (string.IsNullOrWhiteSpace(nodeText))
                return string.Empty;
            int bracketPos = nodeText.IndexOf('(');
            if (bracketPos == -1)
                return nodeText;
            int dotPos = nodeText.IndexOf('.');
            if (dotPos == -1)
                return nodeText;
            if (dotPos > bracketPos)
                return nodeText;
            string rslt = nodeText.Substring(dotPos + 1, bracketPos - dotPos -1);
            int ltPos = rslt.IndexOf('<');
            if (ltPos != -1)
                rslt = rslt.Substring(0, ltPos);
            return rslt;
        }

        private string ParseDefMethodFromNoRef(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return string.Empty;
            int bracketPos = code.LastIndexOf('\'');
            if (bracketPos == -1)
                return code;
            int dotPos = code.LastIndexOf('.');
            if (dotPos == -1)
                return code;
            if (dotPos > bracketPos)
                return code;
            string rslt = code.Substring(dotPos + 1, bracketPos - dotPos - 1);
            return rslt;
        }

        private bool TryParseRec(string ln, out VSUsageRec rec)
        {
            string[] fields = SplitLine(ln);
            rec = new VSUsageRec();
            int rfs = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if (!_headerRev.ContainsKey(i)) continue;
                switch (_headerRev[i])
                {
                    case "Code": rec.Code = fields[i]; if (!string.IsNullOrEmpty(rec.Code)) { rfs++; } break;
                    case "File": rec.File = fields[i]; if (!string.IsNullOrEmpty(rec.File)) { rfs++; } break;
                    case "Line": if (int.TryParse(fields[i], out int tmp)) { rec.Line = tmp; rfs++; } 
                        else { 
                            int a = 0; }; break;
                    case "Column": if (int.TryParse(fields[i], out int tmp1)) { rec.Column = tmp1; rfs++; } 
                        else { 
                            int a = 0; }; break;
                    case "Project": rec.Project = fields[i]; if (!string.IsNullOrEmpty(rec.Project)) { rfs++; } break;
                    case "Kind": rec.Kind = fields[i]; if (!string.IsNullOrEmpty(rec.Kind)) { rfs++; } break;
                    case "Containing Member": rec.ContainingMember = fields[i]; if (!string.IsNullOrEmpty(rec.ContainingMember)) { rfs++; } break;
                    case "Containing Type": rec.ContainingType = fields[i]; if (!string.IsNullOrEmpty(rec.ContainingType)) { rfs++; } break;
                }
            }
            if (IsNoReferencesFound(rec.Code))
            {
                rec.IsNoReferences = true;
                return true;
            }
            return (rfs >= 2);
        }

        private bool IsNoReferencesFound(string code)
        {
            return (code.IndexOf(NoReferencesClause) == 0);
        }

        private void FillHeader(string hdrLine)
        {
            string[] fields = SplitLine(hdrLine);
            if (fields.Length < 2)
                throw new ArgumentException("Invalid header line supplied - too few fields");
            _header = new Dictionary<string, int>();
            _headerRev = new Dictionary<int, string>();
            for (int i = 0; i < fields.Length; i++)
            {
                string fldTrm = fields[i].Trim();
                if (!_header.ContainsKey(fldTrm))
                {
                    _header.Add(fldTrm, i);
                    _headerRev.Add(i, fldTrm);
                }

            }
        }

        private string[] SplitLine(string ln)
        {
            if (string.IsNullOrEmpty(ln))
                return new string[] { };
            return ln.Split('\t');
        }
        private string[] SplitLines(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                return new string[] { };
            return raw.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }
    }
}
