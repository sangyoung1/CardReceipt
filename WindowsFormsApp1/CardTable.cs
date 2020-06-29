using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CardTable
    {
        public string TYPE { get; set; }
        public string USEDATE { get; set; }
        public string USEPLACE { get; set; }
        public string CARD { get; set; }
        public string CARDNUMBER { get; set; }
        public string AMOUNT { get; set; }
        public string PURPOSE { get; set; }
        public string PROJECTCODE { get; set; }
        public string CLASSIFICATION { get; set; }
        public string CONTENT { get; set; }
        public string DEPARTMENT { get; set; }
        public string USERNAME { get; set; }
        public string RESOLUTION { get; set; }
        public string OTHERS { get; set; }
    }

    public class Code
    {
        public string ProjectCode { get; set; }
    }

    public class Card
    {
        public string CardNumber { get; set; }
    }

    public class Name
    {
        public string ENAME { get; set; }
    }

    public class ProAmount
    {
        public string ProjectCode { get; set; }
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
    
    public class Project
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string Manager { get; set; }
        public string ProjectDate { get; set; }
        public string CardNumber { get; set; }
        public string Member { get; set; }
        public decimal ProjectId { get; set; }
        public string ProjectLimit { get; set; }
    }

    public class Details
    {
        public string USEDATE { get; set; }
        public string USEPLACE { get; set; }
        public string CARDNUMBER { get; set; }
        public string AMOUNT { get; set; }
        public string PURPOSE { get; set; }
        public string CLASSIFICATION { get; set; }
        public string CONTENT { get; set; }
        public string USERNAME { get; set; }
    }

    public class CardData
    {
        public string CardNumber { get; set; }
        public string CardUser { get; set; }
        public string CardLimit { get; set; }
        public decimal CardID { get; set; }
    
    }
}
