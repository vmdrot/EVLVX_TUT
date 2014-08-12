using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Facade.Spares
{
    public class JIRAIssue
    {
        public JIRAIssue()
        {
            this.fields = new JIRAIssueFields();
        }

        public JIRAIssue(string project, string summary, string description, string issueType) : this()
        {
            this.fields.project.key = project;
            this.fields.summary = summary;
            this.fields.description = description;
            this.fields.issuetype.id = issueType;
        }
        public JIRAIssueFields fields { get; set; }

        //https://developer.atlassian.com/display/JIRADEV/Issue+Fields+in+JIRA+REST+APIs
        public class JIRAIssueFields
        {
            public JIRAIssueFields()
            {
                this.project = new JIRAIssueProject();
                this.issuetype = new JIRAIssueType();
            }
            public JIRAIssueProject project { get; set; }
            public string summary { get; set; }
            public string description { get; set; }
            public JIRAPriorityType priority { get; set; }
            public string duedate { get; set; }
            public string[] labels { get; set; }
            public JIRAParentIssue parent { get; set; }
            public JIRAIssueType issuetype { get; set; }
            //public DateTime createdDate { get; set; }
        }

        public class JIRAIssueProject
        {
            public string key { get; set; }
        }
        //public class JIRAIssueIdGenericSubType
        //{
        //    public string id { get; set; }
        //}

        public class JIRAIssueType
        {
            public string id { get; set; }
        }

        public class JIRAParentIssue 
        {
            public string id { get; set; }
        }

        public class JIRAPriorityType 
        {
            public string id { get; set; }
        }
    }

}
