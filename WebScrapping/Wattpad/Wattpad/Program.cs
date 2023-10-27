using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wattpad.Nodes;
using Wattpad.logindetails;
using Wattpad.PJE.LoginDetails;

public class Program
{
    public static async Task Main()
    {
        //Nodes.NodeV3();


        //await LoginAction.ExecuteLogin();
        await LoginActionPje.ExecuteLogin();
    }


}
