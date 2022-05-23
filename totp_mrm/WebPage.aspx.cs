using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TotpMRM.BackendLogic;
using TotpMRM.BackendLogic.AlgoLogic;
using System.Timers;
using Autofac;


namespace TotpMRM
{
    public partial class WebPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["Timer"] = DateTime.Now.AddSeconds(30).ToString();
                timerTestTotp_tick(sender, e);
            }

        }

        protected void timerTestTotp_tick(object sender, EventArgs e)
        {
            totpMsg.Text = "New number:  " + passGen() + "\n";
        }

        protected void timerTest_tick(object sender, EventArgs e)
        {

            if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["Timer"].ToString())) < 0)
            {
                litMsg.Text = "Time left: " + (((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60).ToString() + " seconds!";
            }
            else
            {
                timerTestTotp_tick(sender, e);
                Session["Timer"] = DateTime.Now.AddSeconds(30).ToString();
                if (DateTime.Compare(DateTime.Now, DateTime.Parse(Session["Timer"].ToString())) < 0)
                {
                    litMsg.Text = "Time left: " + (((Int32)DateTime.Parse(Session["Timer"].ToString()).Subtract(DateTime.Now).TotalSeconds) % 60).ToString() + " seconds!";
                }
            }

        }
        private static IContainer SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserData>().As<IUserData>();
            builder.RegisterType<Totp>().As<ITotp>();

            return builder.Build();
        }

        private string passGen()
        {
            IContainer container = SetupContainer();
            string password = null;

            using (var containerScope = container.BeginLifetimeScope())
            {
                var service = containerScope.Resolve<IUserData>();
                password = service.GeneratePassword(123, DateTime.Now);
            }

            return password;
        }
    }
}