using System;
using System.Linq;

namespace InsuranceCompany.Models
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext db)
        {
            db.Database.EnsureCreated();

            if (db.Staffs.Any() && db.Risks.Any() && db.ClientGroups.Any()&&db.PolicyTypes.Any() && db.Policys.Any()&&db.Clients.Any())
            {
                return;
            }

            Random randObj = new Random(1);

            string[] policyName = { "policyName1", "policyName2", "policyName3", "policyName4", "policyName5" };
            int count_policyName_voc = policyName.GetLength(0);
            string[] policyDescription = { "policyDescription1", "policyDescription2", "policyDescription3", "policyDescription4", "policyDescription5" };
            int count_policyDescription_voc = policyDescription.GetLength(0);
            string[] policyCondition = { "policyCondition1", "policyCondition2", "policyCondition3", "policyCondition4", "policyCondition5" };
            int count_policyCondition_voc = policyCondition.GetLength(0);
            for (int ID = 1; ID <= 1000; ID++)
            {
                db.PolicyTypes.Add(new PolicyTypes
                {
                    PolicyName = policyName[randObj.Next(count_policyName_voc)],
                    PolicyDescription = policyDescription[randObj.Next(count_policyDescription_voc)],
                    PolicyCondition = policyCondition[randObj.Next(count_policyCondition_voc)]
                });
            }
            db.SaveChanges();

            string[] name = { "staffName1", "staffName2", "staffName3", "staffName4", "staffName5" };
            int count_staffName_voc = name.GetLength(0);
            string[] post = { "post1", "post2", "post3", "post4", "post5" };
            int count_staffPost_voc = post.GetLength(0);
            string[] experience = { "experience1", "experience2", "experience3", "experience4", "experience5" };
            int count_staffExperience_voc = experience.GetLength(0);
            for (int ID = 1; ID <= 1000; ID++)
            {
                string staffName = name[randObj.Next(count_staffName_voc)];
                string staffPost = post[randObj.Next(count_staffPost_voc)];
                string staffExperience = experience[randObj.Next(count_staffExperience_voc)];
                db.Staffs.Add(new Staffs { StaffName = staffName, StaffPost = staffPost, StaffExperience = staffExperience });
            }
            db.SaveChanges();

            
            string[] nameRisks = { "nameRisks1", "nameRisks2", "nameRisks3", "nameRisks4", "nameRisks5" };
            int count_nameRisks_voc = nameRisks.GetLength(0);
            string[] RiskDescription = { "RiskDescription1", "RiskDescription2", "RiskDescription3", "RiskDescription4", "RiskDescription5" };
            int count_RiskDescription_voc = RiskDescription.GetLength(0);
            string[] averageProbability = { "averageProbability1", "averageProbability2", "averageProbability3", "averageProbability4", "averageProbability5" };
            int count_averageProbability_voc = averageProbability.GetLength(0);
            for (int ID = 1; ID <= 1000; ID++)
            {
                db.Risks.Add(new Risks
                {
                    RiskName = nameRisks[randObj.Next(count_nameRisks_voc)],
                    RiskDescription = RiskDescription[randObj.Next(count_RiskDescription_voc)],
                    AverageProbability = averageProbability[randObj.Next(count_averageProbability_voc)],
                    TypeId = randObj.Next(1, 3)
                });
            }
            db.SaveChanges();

            string[] groupName = { "groupName1", "groupName2", "groupName3", "groupName4", "groupName5" };
            int count_groupName_voc = groupName.GetLength(0);
            string[] groupDescription = { "groupDescription1", "groupDescription2", "groupDescription3", "groupDescription4", "groupDescription5" };
            int count_groupDescription_voc = groupDescription.GetLength(0);
            
            for (int ID = 1; ID <= 1000; ID++)
            {
                
                db.ClientGroups.Add(new ClientGroups
                {
                    GroupName= groupName[randObj.Next(count_groupName_voc)],
                    GroupDescription = groupDescription[randObj.Next(count_groupDescription_voc)],
                });
            }
            db.SaveChanges();

            string[] clientName = { "name1", "name2", "name3", "name4", "name5" };
            int count_clientName_voc = clientName.GetLength(0);
            string[] clientSex = { "m.", "w."};
            int count_clientSex_voc = clientSex.GetLength(0);
            string[] clientAddress = { "address1", "address2", "address3", "address4" , "address5" };
            int count_clientAddress_voc = clientAddress.GetLength(0);
            string[] clientPassport = { "123", "456", "124", "6467", "24222" };
            int count_clientPassport_voc = clientPassport.GetLength(0);
            for (int ID = 1; ID <= 1000; ID++)
            {
                DateTime today = DateTime.Now.Date;
                DateTime date = today.AddDays(-ID);
                db.Clients.Add(new Client
                {
                    ClientName = clientName[randObj.Next(count_clientName_voc)],
                    ClientDateBirth = date,
                    ClientSex = clientSex[randObj.Next(count_clientSex_voc)],
                    ClientAddress = clientAddress[randObj.Next(count_clientAddress_voc)],
                    ClientPhone = randObj.Next(100000,999999),
                    ClientPassport = clientAddress[randObj.Next(count_clientPassport_voc)],
                    GroupId = randObj.Next(1, 999),

                });
            }
            db.SaveChanges();

            string[] paymentMark = { "paymentMark1", "paymentMark2", "paymentMark3", "paymentMark4", "paymentMark5" };
            int count_paymentMark_voc = paymentMark.GetLength(0);
            string[] endMark = { "endMark1", "endMark2", "endMark3", "endMark4", "endMark5" };
            int count_endMark_voc = endMark.GetLength(0);
            for (int ID = 1; ID <= 1000; ID++)
            {
                DateTime today = DateTime.Now.Date;
                DateTime date = today.AddDays(-ID);
                db.Policys.Add(new Policys
                {
                    PolicyNumber = randObj.Next(1000,9000),
                    DateBegin = date,
                    DateEnd = date,
                    Cost = randObj.Next(1000, 9000),
                    Summ = randObj.Next(1000, 9000),
                    TypeId = randObj.Next(1, 999),
                    PaymentMark= paymentMark[randObj.Next(count_paymentMark_voc)],
                    EndMark = endMark[randObj.Next(count_endMark_voc)],
                    ClientId= randObj.Next(1, 999),
                    StaffId = randObj.Next(1, 999)
                });
            }
            db.SaveChanges();

            

        }
    }
}
