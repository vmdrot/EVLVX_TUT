using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Evolvex.Ruthenorum.JIRAAuth.Facade;
using Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces;
using Evolvex.Ruthenorum.JIRAAuth.Factories;
using Newtonsoft.Json;
using Evolvex.Ruthenorum.JIRAAuth.Facade.Spares;
using Evolvex.Ruthenorum.JIRAAuth.Tests.Forms;

namespace Evolvex.Ruthenorum.JIRAAuth.Tests
{
    [TestFixture]
    public class JiraAuthFacadeTests
    {
        [Test]
        public void ListNewUsers()
        {

            DateTime started = DateTime.Now;
            List<IJIRAUserInfo> juis = JiraAuthFacade.ListNewUsers();
            Console.WriteLine("juis.Count = {0}", juis.Count);
            foreach(IJIRAUserInfo jui in juis)
            {
                Console.WriteLine(jui);
            }
            DateTime ended = DateTime.Now;
            Console.WriteLine("Fetching took {0}", (TimeSpan)(ended - started));
        }

        [Test]
        public void FindHRIssueByEmailTest()
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "pwd");
            string respJson = JiraAuthFacade.FindIssues("project=Spilno-HR AND text ~ \"nbula_fil@voliacable.com\"");
            Console.WriteLine(respJson);
        }

        [Test]
        public void FindHRIssueByEmailTest1()
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "pwd");
            string respJson = JiraAuthFacade.FindHRCandidateByEmail("osv1960@mail.ru");
            Console.WriteLine(respJson);
        }

        [Test]
        public void FindHRIssueByEmailTest2()
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "pwd");
            string respJson = JiraAuthFacade.FindHRCandidateByEmail("atrium.sk@gmail.com");
            Console.WriteLine(respJson);
        }
        private void CreateHRIssueTestWorker(string name)
        {
            CreateHRIssueTestWorker(name, string.Format("{0} description", name));
        }

        private void CreateHRIssueTestWorker(string name, string description)
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "pwd");
            string respJson = JiraAuthFacade.CreateIssue("SPTVHR", name, description, "12");
            Console.WriteLine(respJson);
        }

        [Test]
        public void CreateHRIssueTest()
        {
            CreateHRIssueTestWorker("Holopupenko Vasyl(Popazdra)");
        }

        [Test]
        public void CreateHRIssueTest1()
        {
            CreateHRIssueTestWorker("Alfred Zalupenko (Popyhujlivka)");
        }

        [Test]
        public void CreateHRIssueTest2()
        {
            CreateHRIssueTestWorker("Харитон Йобищенко (Лохвиця)", "Повний перелік іншомовних сторінок \\\"Спільнобачення\\\": \\r\\n\\r\\nhttps://www.facebook.com/spilnotv.en (англ.)\\r\\n\\r\\nhttps://www.facebook.com/SpilnoTv.Polski (польською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.it (італійською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ru (російською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.es (іспанською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.de (німецькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.fr (французькою\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.cz (чеською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.sk (словацькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.pt (португальською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.bg (болгарською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ro (румунською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.nl (нідерландською)https://www.facebook.com/spilnotv.hu (угорською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.he (івритом)https://www.facebook.com/spilnotv.sv (шведською)\\r\\n\\r\\n");
        }

        [Test]
        public void CreateHRIssueTest3()
        {
            //CreateHRIssueTestWorker("Шльома Гомельський (Борщагівка)", "Повний перелік іншомовних сторінок \"Спільнобачення\": \\r\\n\\r\\nhttps://www.facebook.com/spilnotv.en (англ.)\\r\\n\\r\\nhttps://www.facebook.com/SpilnoTv.Polski (польською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.it (італійською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ru (російською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.es (іспанською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.de (німецькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.fr (французькою\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.cz (чеською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.sk (словацькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.pt (португальською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.bg (болгарською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ro (румунською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.nl (нідерландською)\\r\\nhttps://www.facebook.com/spilnotv.hu (угорською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.he (івритом)\\r\\nhttps://www.facebook.com/spilnotv.sv (шведською)\\r\\n\\r\\n");
            CreateHRIssueTestWorker("Шльома Гомельський (Борщагівка)", "Повний перелік іншомовних сторінок \\\"Спільнобачення\\\": \\r\\n\\r\\nhttps://www.facebook.com/spilnotv.en (англ.)\\r\\n\\r\\nhttps://www.facebook.com/SpilnoTv.Polski (польською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.it (італійською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ru (російською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.es (іспанською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.de (німецькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.fr (французькою\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.cz (чеською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.sk (словацькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.pt (португальською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.bg (болгарською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ro (румунською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.nl (нідерландською)\\r\\nhttps://www.facebook.com/spilnotv.hu (угорською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.he (івритом)\\r\\nhttps://www.facebook.com/spilnotv.sv (шведською)\\r\\n\\r\\n");
        }

        [Test]
        public void CreateHRIssueTest4()
        {
            //CreateHRIssueTestWorker("Шльома Гомельський (Борщагівка)", "Повний перелік іншомовних сторінок \"Спільнобачення\": \\r\\n\\r\\nhttps://www.facebook.com/spilnotv.en (англ.)\\r\\n\\r\\nhttps://www.facebook.com/SpilnoTv.Polski (польською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.it (італійською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ru (російською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.es (іспанською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.de (німецькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.fr (французькою\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.cz (чеською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.sk (словацькою)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.pt (португальською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.bg (болгарською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.ro (румунською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.nl (нідерландською)\\r\\nhttps://www.facebook.com/spilnotv.hu (угорською)\\r\\n\\r\\nhttps://www.facebook.com/spilnotv.he (івритом)\\r\\nhttps://www.facebook.com/spilnotv.sv (шведською)\\r\\n\\r\\n");
            CreateHRIssueTestWorker("Шльома Гомельський (Борщагівка)", @"Дорога Маріє,
Пишу за дорученням усієї Команди СПІЛЬНОБАЧЕННЯ. 
Ми жалкуємо з приводу того, що ти добровільно вирішила припинити участь у творенні «Спільнобачення», однак розуміємо, що на те у тебу були власні вагомі причини.
За останні два місяці ми опанували не лише значну аудиторію, але й нажили собі впливових ворогів.  Членство у нашій організації (колишнє, а особливо – нинішнє) автоматично ставить відповідну людину під загрозу репресій. Відповідно, необхідно буде здати твоє журналістське посвідчення, отримане від Spilno.TV – можеш віддати їх будь-кому з членів Команди, що залишаються, відписавшись нам на пошту, кому саме ти віддала. За нашими відчуттями, найкраще це зробити протягом найближчих 3-х робочих днів. 
Мусимо зазначити, що нам вкрай не вистачало твоїх роботящих рук та світлої голови упродовж цих кількох «марафонських» місяців Майдану і цього листа ми пишемо з важким серцем. Однак, ми розуміємо, що то був твій добровільний чи вимушений обставинами (наявність часу, тощо) вибір і розуміємо тебе. Коли і якщо все обійдеться, а також  якщо у тебе з'явиться час та бажання відновити твою активну участь у нашому проекті, будемо раді знову тебе побачити. Ми надзвичайно вдячні тобі за попередню участь у проекті, як і не забудемо той внесок, що ти робила чи хотіла зробити, але не встигла, у спільне з іншими творення «Спільнобачення». Ну і, формальний вихід з організації не є приводом припинити з нами спілкування – якщо є щось, чим ми могли б один одному допомогти, пиши, не забувай і не «губися».
З повагою,
Команда «Спільнобачення»
");
        }

        private void CreateInoZmiTranslationIssueTestWorker(string name, string description)
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            Dictionary<string, string> suffixesLabels = new Dictionary<string, string>();
            suffixesLabels.Add("aнгл", "English");
            suffixesLabels.Add("нім", "Deutsch");
            suffixesLabels.Add("фр", "Francais");
            suffixesLabels.Add("іт", "Italiano");
            suffixesLabels.Add("ісп", "Espagnol");
            suffixesLabels.Add("польськ", "Polska");
            suffixesLabels.Add("гол", "Nederlands");

            string respJson = JiraAuthFacade.CreateIssue("SPTVINOZMI", name, description, "13");
            respJson = JiraAuthFacade.FindIssues(string.Format("project = Spilno-InoZMI AND description ~ \"{0}\"", description));
            JIRAIssuesSearchResult res = JsonConvert.DeserializeObject<JIRAIssuesSearchResult>(respJson);

            string parentId = res.issues[0].id;
            System.Threading.Thread.Sleep(1000 * 3);
            foreach (string sfx in suffixesLabels.Keys)
            {
                respJson = JiraAuthFacade.CreateSubTask("SPTVINOZMI", parentId, String.Format("{0} - {1}.", name, sfx), description, "15", DateTime.Now.AddDays(3), "1", new string[] { suffixesLabels[sfx] });
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine(respJson);
            }
            Console.WriteLine(respJson);
        }

        [Test]
        public void FindInoZMIIssueByUrlTest()
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            string respJson = JiraAuthFacade.FindIssues("project = Spilno-InoZMI AND description ~ \"http://crime.in.ua/news/20140121/titushki-safari\"");
            JIRAIssuesSearchResult res = JsonConvert.DeserializeObject<JIRAIssuesSearchResult>(respJson);
            Console.WriteLine("res.maxResults = {0}", res.maxResults);
            Console.WriteLine("res.issues[0].id = {0}", res.issues[0].id);
            Console.WriteLine(respJson);
        }

        [Test]
        public void FindInoZMIIssueByUrlTest1()
        {

            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            string respJson = JiraAuthFacade.FindIssues("project = Spilno-InoZMI AND description ~ \"http://abc.com/article1\"");
            JIRAIssuesSearchResult res = JsonConvert.DeserializeObject<JIRAIssuesSearchResult>(respJson);
            Console.WriteLine("res.maxResults = {0}", res.maxResults);
            Console.WriteLine("res.issues[0].id = {0}", res.issues[0].id);
            Console.WriteLine(respJson);
        }

        public class JIRAIssuesSearchResult
        {
            public int startAt { get; set; }
            public int maxResults { get; set; }
            public int total { get; set; }
            public JIRAIssue[] issues { get; set; }

            public class JIRAIssue
            {
                public string id { get; set; }
            }
        }

        [Test]
        public void CreateTranslationJobWithSubTasksTest1()
        {
            CreateInoZmiTranslationIssueTestWorker("Тестовий переклад", "http://abc.com/article1");
        }

        [Test]
        public void CreateSubTaskTest1()
        {
            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            //JiraAuthFacade.CreateSubTask("SPTVINOZMI", "11721", String.Format("{0} - {1}.", "name", "sfx"), "description", "15", DateTime.Now.AddDays(3), "1", new string[] { "English" });
            JiraAuthFacade.CreateSubTask("SPTVINOZMI", "11721", String.Format("{0} - {1}.", "name", "sfx"), "description", "15", null, "1", new string[] { "English" });
        }

        [Test]
        public void CreateSubTaskTest2()
        {
            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            //JiraAuthFacade.CreateSubTask("SPTVINOZMI", "11721", String.Format("{0} - {1}.", "name", "sfx"), "description", "15", DateTime.Now.AddDays(3), "1", new string[] { "English" });
            JiraAuthFacade.CreateSubTask("SPTVINOZMI", "11721", String.Format("{0} - {1}.", "name", "sfx"), "description", "15", null, "1", new string[] { "English" });
        }

        [Test]
        public void CreateSubTaskRawTest1()
        {
            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            MiscTestParamsForm frm = new MiscTestParamsForm();
            if(frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            JIRAAuthenticatorFactory.Instance.Authenticator.GenericPost("/rest/api/2/issue/", frm.JSON);

        }

        [Test]
        public void ListFieldDefs()
        {
            JIRAAuthenticatorFactory.Instance.InitAuthenticator("valeriy.drotenko", "Asempra123");
            //JiraAuthFacade.CreateSubTask("SPTVINOZMI", "11721", String.Format("{0} - {1}.", "name", "sfx"), "description", "15", DateTime.Now.AddDays(3), "1", new string[] { "English" });
            List<JIRAFieldInfo> fields = JiraAuthFacade.GetJIRAFieldDefs();
            Console.WriteLine("fields.Count = {0}", fields.Count);
            foreach (JIRAFieldInfo fld in fields)
            {
                Console.WriteLine("fld.id: {0}, name: {1}, searchable: {2}, orderable: {3}, navigable: {4}, schema.custom: {5}, schema.customId: {6}, schema.type: {7}", fld.id, fld.name, fld.searchable, fld.orderable, fld.navigable, fld.schema != null ? fld.schema.custom : "", fld.schema != null ? fld.schema.customId : -1, fld.schema != null ? fld.schema.type : "");
            }
        }
        
    }
}
