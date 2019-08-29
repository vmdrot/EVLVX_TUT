using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Evolvex.VKUtilLib.DataGovUa
{
    public class DataGovUaReader : WebBrowserReaderBase
    {
        protected static readonly string ROOT_URL = "http://data.gov.ua/datasets";
        protected static readonly string PAGE_URL_FMT = "http://data.gov.ua/datasets?field_organization_value=&title=&sort_bef_combine=created%20DESC&sort_order=DESC&sort_by=created&page={0}";
        protected static readonly Regex PassGuidRegex = new Regex("[0-9a-z]{8}\\-[0-9a-z]{4}\\-[0-9a-z]{4}\\-[0-9a-z]{4}\\-[0-9a-z]{12}");

        private int _currPgIdx = 0;
        public int PagesCount
        {
            get;
            private set;
        }

        public List<string> PassportUrls { get; set; }
		public List<DataSetInfoSimplified> Passports { get; set; }
		
		public int StartFromPage { get; set; }
        public int StopAfterPage { get; set; }
        public string SaveResultAs { get; set; }
        private bool? _writeResultsImmediately;
        private StreamWriter _resultsStream;
        private bool WriteResultsImmediately
        {
            get
            {
                if (_writeResultsImmediately != null)
                    return (bool)_writeResultsImmediately;
                WriteErrorsCount = 0;
                if (string.IsNullOrWhiteSpace(SaveResultAs))
                    _writeResultsImmediately = false;
                _writeResultsImmediately = TryOpenResultsStream();
                return (bool)_writeResultsImmediately;
            }
        }

		private bool _getJustUrls = true;

		public bool GetJustUrls
		{
			get
			{
				return this._getJustUrls;
			}
			set
			{
				this._getJustUrls = value;
			}
		}
        public int WriteErrorsCount { get; private set; }

        private bool TryOpenResultsStream()
        {
            try
            {
                _resultsStream = new StreamWriter(SaveResultAs, false);
                return _resultsStream != null;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error opening file '{0}' for saving passport urls, details: {1}", SaveResultAs, exc);
                WriteErrorsCount++;
                return false;
            }
        }


        protected override bool ReadWorker()
        {
            if (StartFromPage > 0)
                _currPgIdx = StartFromPage;
            bool rslt = DetectPagesCount();
            PagesCount = _currPgIdx + 1;
            if (!rslt)
            {
                Console.WriteLine("{0}\tFailed at DetectPagesCount", DateTime.Now);
                return rslt;
            }
            rslt = ReadPages();
            if (!rslt)
            {
                Console.WriteLine("{0}\tFailed at ReadPages", DateTime.Now);
                return rslt;
            }
            return rslt;
        }

        protected bool ReadPages()
        {
			if (this.GetJustUrls)
				PassportUrls = new List<string>();
			else
			{
				WriteHeaderForPassports();
				Passports = new List<DataSetInfoSimplified>();
			}

			for (int i = 0; i < PagesCount; i++)
            {
                if (StopAfterPage > 0 && i >= StopAfterPage)
                    break;

                string currUrl = string.Format(PAGE_URL_FMT, i);
                if (!this.Navigate(currUrl))
                    break;
				if (this.GetJustUrls)
				{
					List<string> currPageUrls = ParsePassportUrls();
					PassportUrls.AddRange(currPageUrls);
					if (WriteResultsImmediately)
						WriteResultsUrls(currPageUrls);
				}
				else
				{

					Passports.AddRange(ParsePassportUrlsEx());
					if (WriteResultsImmediately)
						WriteResults(Passports);
				}

			}
            return this.GetJustUrls? PassportUrls.Count > 0 : Passports.Count > 0;
        }

		private void WriteHeaderForPassports()
		{
			_resultsStream.WriteLine("{0}\t{1}\t{2}", "PassportUrl", "Name", "InformationOwner");
		}

		private void WriteResultsUrls(List<string> currPageUrls)
        {
            if (currPageUrls == null || currPageUrls.Count == 0)
                return;
            foreach (string url in currPageUrls)
                _resultsStream.WriteLine(url);
            _resultsStream.Flush();
        }

		private void WriteResults(List<DataSetInfoSimplified> dsPassports)
		{
			if (dsPassports == null || dsPassports.Count == 0)
				return;
			foreach (DataSetInfoSimplified dsi in dsPassports)
				_resultsStream.WriteLine("{0}\t{1}\t{2}", dsi.PassportUrl, dsi.Name, dsi.InformationOwner);
			//_resultsStream.WriteLine("{0}\t{1}\t{2}", dsi.PassportUrl.Trim(), dsi.Name.Trim(), dsi.InformationOwner.Trim());
			_resultsStream.Flush();
		}

		
		private List<DataSetInfoSimplified> ParsePassportUrlsEx()
		{
			List<DataSetInfoSimplified> rslt = new List<DataSetInfoSimplified>();
			List<string> rsltUrls = new List<string>();

			//http://data.gov.ua/passport/96d3216b-7c35-4053-b6fa-93d37ab7cfd6

			HtmlElementCollection anchors = _wc.Document.GetElementsByTagName("a");
			if (anchors.Count == 0)
				return rslt;
			foreach (HtmlElement currA in anchors)
			{
				string href = currA.GetAttribute("href");
				if (string.IsNullOrWhiteSpace(href))
					continue;
				string hrefToL = href.ToLower();
				if (hrefToL.IndexOf("/passport/") == -1)
					continue;
				if (PassGuidRegex.Matches(href).Count == 0)
					continue;
				
				if (!rsltUrls.Contains(href))
				{ 
					rsltUrls.Add(href);
					DataSetInfoSimplified dsi = new DataSetInfoSimplified() { PassportUrl = href, Name = currA.InnerText.Trim() };
					dsi.InformationOwner = ReadInformationOwnerByDSAnchor(currA);
					rslt.Add(dsi);
				}
			}
			return rslt;
		}

		private string ReadInformationOwnerByDSAnchor(HtmlElement currA)
		{
			const string RowClassStart = "views-row views-row-";
			#region find row div
			HtmlElement currElem = currA;
			HtmlElement rowDiv = null;
			while (currElem.Parent != null)
			{
				if (currElem.TagName.ToLower() == "div")
				{
					string currClass = ReadDivAttribValue(currElem, "class");
					if(!string.IsNullOrWhiteSpace(currClass))
					{
						if (currClass.Length >= RowClassStart.Length)
						{
							if (currClass.Substring(0, RowClassStart.Length).ToLower() == RowClassStart)
							{ 
								rowDiv = currElem;
								break;
							}
						}
					}
				}
			    currElem = currElem.Parent;
			}
			#endregion
			if(rowDiv == null)
				return null;
			HtmlElement bottomFieldsDiv = base.FindChildElementByClass(rowDiv, "div", "bottom views-fieldset");
			if (bottomFieldsDiv == null)
				return null;
			HtmlElement authorOrgField = base.FindChildElementByClass(bottomFieldsDiv, "div", "views-field views-field-author-field-organization views-field-field-organization");
			if (authorOrgField == null)
				return null;
			return authorOrgField.InnerText;
		}

		private List<string> ParsePassportUrls()
        {
            List<string> rslt = new List<string>();
            
            //http://data.gov.ua/passport/96d3216b-7c35-4053-b6fa-93d37ab7cfd6

            HtmlElementCollection anchors = _wc.Document.GetElementsByTagName("a");
            if (anchors.Count == 0)
                return rslt;
            foreach (HtmlElement currA in anchors)
            {
                string href = currA.GetAttribute("href");
                if (string.IsNullOrWhiteSpace(href))
                    continue;
                string hrefToL = href.ToLower();
                if (hrefToL.IndexOf("/passport/") == -1)
                    continue;
                if (PassGuidRegex.Matches(href).Count == 0)
                    continue;
                if (!rslt.Contains(href))
                    rslt.Add(href);
            }
            return rslt;
        }

        protected bool DetectPagesCount()
        {
            while (true)
            {
                Console.WriteLine("_currPgIdx = {0}", _currPgIdx);
                string currUrl = string.Format(PAGE_URL_FMT, _currPgIdx);
                if (!this.Navigate(currUrl))
                    return false;
                if (!ParseBiggerPageNumber())
                {
                    
                    return (_currPgIdx > 0);
                }
				if (this.StopAfterPage > 0 && (_currPgIdx + 1) >= this.StopAfterPage)
					return true;
            }
            return true;
        }

        private bool ParseBiggerPageNumber()
        {
            int currLastPg;
            if(ExtractCurrentLastPage(out currLastPg))
            {
                if (_currPgIdx < currLastPg)
                {
                    _currPgIdx = currLastPg;
                    return true;
                }
            }
            return false;
        }

        private bool ExtractCurrentLastPage(out int pageNr)
        {
            pageNr = -1;
            HtmlElementCollection anchors = _wc.Document.GetElementsByTagName("a");
            if (anchors.Count == 0)
                return false;
            foreach(HtmlElement currA in anchors)
            {
                string href = currA.GetAttribute("href");
                if(string.IsNullOrWhiteSpace(href))
                    continue;
                string hrefToL = href.ToLower();
                if(hrefToL.IndexOf("datasets?") == -1 || hrefToL.IndexOf("&page=") == -1)
                    continue;
                string currPageParam = ParseUrlGetParam(href, "page");
                if(string.IsNullOrWhiteSpace(currPageParam))
                    continue;
                int currPageNr;
                if(int.TryParse(currPageParam, out currPageNr))
                {
                    if(currPageNr > pageNr)
                        pageNr = currPageNr;
                }
            }
            return pageNr != -1;
        }

        protected override void DisposeCustomManagedResources()
        {
            if(this._resultsStream != null)
            {
                this._resultsStream.Flush();
                this._resultsStream.Close();
                this._resultsStream.Dispose();
                this._resultsStream = null;
            }
        }
    }
}
