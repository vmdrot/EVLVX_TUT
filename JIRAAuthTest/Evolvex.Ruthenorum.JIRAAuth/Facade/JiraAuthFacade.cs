using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using Evolvex.Ruthenorum.JIRAAuth.Factories;
using System.Web;
using Evolvex.Ruthenorum.JIRAAuth.Utilities;
using Evolvex.Ruthenorum.JIRAAuth.Facade.Spares;
using Newtonsoft.Json;

namespace Evolvex.Ruthenorum.JIRAAuth.Facade
{
    public class JiraAuthFacade
    {
        private static readonly string QUERY_ISSUES_REST_API_PATH = "/rest/api/2/search";
        private static readonly string CREATE_ISSUE_JSON_FMT = @"{{
    ""fields"":
    {{
        ""project"":
        {{
            ""key"": ""{0}""
        }},
        ""summary"": ""{1}"",
        ""description"": ""{2}"",
        ""issuetype"":
        {{
            ""id"": ""{3}""
        }}
    }}
}}";
        public static List<IJIRAUserInfo> ListNewUsers()
        {
            List<IJIRAUserInfo> rslt = new List<IJIRAUserInfo>();
            List<string> allUsrs = JIRAAuthenticatorFactory.Instance.Authenticator.ListAllUserNames();
            foreach (String usr in allUsrs)
            {
                IJIRAUserInfo jui = JIRAAuthenticatorFactory.Instance.Authenticator.GetUser(usr);

                if (jui.groups == null || jui.groups.Count < 2)
                    rslt.Add(jui);
            }
            return rslt;
        }

        public static string FindIssues(string jql)
        {
            /// http://pm.ruthenorum.info/rest/api/2/search?jql=project%3DSpilno-HR%20AND%20text%20~%20%22nbula_fil@voliacable.com%22
            /// 
            return JIRAAuthenticatorFactory.Instance.Authenticator.GenericQuery(QUERY_ISSUES_REST_API_PATH, string.Format("jql={0}", Tools.EncodeJQL(jql)));
        }

        public static string FindHRCandidateByEmail(string email)
        {
            return FindIssues(string.Format("project=Spilno-HR AND text ~ \"{0}\"", email));
        }

        public static string CreateIssue_old(string project, string summary, string description, string issueTypeId)
        {
            return JIRAAuthenticatorFactory.Instance.Authenticator.GenericPost("/rest/api/2/issue/", string.Format(CREATE_ISSUE_JSON_FMT, project, summary, description, issueTypeId));
        }

        private static JIRAIssue ConstructJIRAIssue(string project, string summary, string description, string issueTypeId, DateTime? dueDate, string priority, string[] labels)
        {
            JIRAIssue issue = new JIRAIssue(project, summary, description, issueTypeId);
            if(!string.IsNullOrEmpty (priority))
                issue.fields.priority = new JIRAIssue.JIRAPriorityType() { id = priority };
            if (dueDate != null)
                issue.fields.duedate = ((DateTime)dueDate).ToString("yyyy-MM-ddTHH:mm:ss");
            if (labels != null)
                issue.fields.labels = labels;
            return issue;
        }
        public static string CreateIssue(string project, string summary, string description, string issueTypeId, DateTime? dueDate, string priority, string[] labels)
        {
            JIRAIssue issue = ConstructJIRAIssue(project, summary, description, issueTypeId, dueDate, priority, labels);
            return CreateIssue(issue);
        }

        public static string CreateIssue(string project, string summary, string description, string issueTypeId)
        {
            return CreateIssue(project, summary, description, issueTypeId, null, null, null);
        }
        public static string CreateIssue(JIRAIssue issue)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            string jsonStr = JsonConvert.SerializeObject(issue, settings);
            return JIRAAuthenticatorFactory.Instance.Authenticator.GenericPost("/rest/api/2/issue/", jsonStr);
        }
        public static string CreateSubTask(string project, string parentId, string summary, string description, string issueTypeId, DateTime? dueDate, string priority, string[] labels)
        {
            JIRAIssue issue = ConstructJIRAIssue(project, summary, description, issueTypeId, dueDate, priority, labels);
            issue.fields.parent = new JIRAIssue.JIRAParentIssue() { id = parentId };
            return CreateIssue(issue);
        }

        public static List<JIRAFieldInfo> GetJIRAFieldDefs()
        {
            //List<JIRAFieldInfo> rslt = new List<JIRAFieldInfo>();
            String fieldsRespJson = JIRAAuthenticatorFactory.Instance.Authenticator.GenericQuery("/rest/api/2/field", "");
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            JIRAFieldInfo[] fields = JsonConvert.DeserializeObject<JIRAFieldInfo[]>(fieldsRespJson, settings);
            return new List<JIRAFieldInfo>(fields);
        }


    }
}
