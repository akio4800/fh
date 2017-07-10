using SelvesSoftware.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.GUI
{
    public class Information
    {

        /// <summary>
        /// Contains all Errors - RED in Warnsystem
        /// </summary>
        private List<String> _errors;
        
        public List<String> Errors
        {

            get
            {
                if (_errors == null)
                {
                    _errors = new List<String>();
                }
                return _errors;
            }
        }

        




        /// <summary>
        /// Contains all INFOS - YELLOW in Warnsystem
        /// </summary>vv
        private String _infos;
    public String Infos
    {
        get
        {
            return _infos;
        }

            set
            {
                _infos = value;
            }
    }


}

public class PaInformation : Information
    {

        PersonalAssistant pa;

        public PaInformation(PersonalAssistant pa)
        {
            this.pa = pa;
            getInformation();
        }

        public void getInformation()
        {
            checkWeiterbildung();
            checkAuftraggeber();
            checkInfos();
        }

        private void checkInfos()
        {

            Infos = pa.InfoField;

        }

        private void checkAuftraggeber()
        {
            if (pa.Purchasers == null || pa.Purchasers.Count == 0)
            {
                Errors.Add("Dieser Persönliche Assistent hat noch keinen Auftraggeber!");

            }
        }

        private void checkWeiterbildung()
        {
            DateTime d = DateTime.Now.AddMonths(6);
            if (pa.Active && pa.deadLineHours < d)
            {               
                    Errors.Add("Bis zum " + pa.deadLineHours.Value.ToShortDateString() + " müssen noch " + (16 - pa.consumedHours) + " Stunden Weiterbildung absolviert werden!" );
            }
        }
    }


    public class AgInformation : Information
    {
        PurchaserData ag;

        public AgInformation(PurchaserData ag)
        {
            this.ag = ag;
            getInformation();
        }

        public void getInformation()
        {
            checkStunden();
            checkPa();
            checkRequired();
            checkBwilligungszeitraum();
            checkInfos();
            checkSVNR();
        }

        private void checkSVNR()
        {
            if(ag.Purchaser.SVN == 0)
            {
                Errors.Add("Dieser Auftraggeber hat noch keine Sozialversicherungsnummer!");
            }
        }
        
        private void checkInfos()
        {
           
                Infos = ag.Purchaser.InfoField;
            
        }

        private void checkStunden()
        {
            //Monatsabrechnung needed
        }

        private void checkPa()
        {
            if (ag.Purchaser.Employees == null || ag.Purchaser.Employees.Count == 0)
            {
                Errors.Add("Dieser Auftraggeber hat noch keine Persönlichen Assistenten!");
            }
        }

        private void checkRequired()
        {
            if (!ag.Purchaser.hasIntroCourse)
            {
                Errors.Add("Dieser Auftraggeber hat noch keinen Einführungskurs besucht!");
            }

            if (!ag.Purchaser.hasContract)
            {
                Errors.Add("Für diesen Auftraggeber existiert noch kein Kontrakt!");
            }
        }

        private void checkBwilligungszeitraum()
        {
            DateTime d = DateTime.Now.AddMonths(6);
            if (!noBewilligungszeitraum())
            {
                if (ag.Purchaser.Active && ag.Purchaser.ApprovalEnd < d)
                {
                    Errors.Add("Der Bewilligungszeitraum endet am " + ag.Purchaser.ApprovalEnd.Value.ToShortDateString());
                }

            }
            else
            {
                Errors.Add("Bei diesem Auftraggeber ist noch kein Bewilligungszeitraum eingetragen! ");
            }

        }

        private bool noBewilligungszeitraum()
        {
            if (!(ag.Purchaser.ApprovalEnd == null && ag.Purchaser.ApprovalBegin == null))
            {
                int r1 = DateTime.Compare((DateTime)ag.Purchaser.ApprovalEnd, (DateTime)ag.Purchaser.ApprovalBegin);

                return r1 == 0;
            }
            else
            {
                return true;
            }
        }
    }




}
