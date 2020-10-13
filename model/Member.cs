using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Member
    {


        public Member()
        {
            Random rnd = new Random();
            Id = rnd.Next(10000, 99999);
            Boats = new List<Boat>();
        }
        public List<Boat> Boats { get; set; }

        public string FirstName
        {
            set;
            get;
        }

        public string LastName
        {
            set;
            get;
        }
        public int Id
        {
            get;
            set;
        }

        public SocialSecurityNumber SocialSecurityNum
        {
            set;
            get;
        }

        public class SocialSecurityNumber
        {
            public string SsNum { get; set; } // social security number
            public DateTime BirthDate { get; set; }
            public long SerialNumber { get; set; }

            public SocialSecurityNumber(string ssNum)
            {
                // Validera
                bool isValidssNum = ValidateSsNum(ssNum);
                if (isValidssNum)
                {
                    DateTime birhDate = DateTime.Parse(buildDateString(ssNum));

                    int serialNumber = Convert.ToInt32(ssNum.Substring(6, 4));

                    SsNum = ssNum;
                    BirthDate = birhDate;
                    SerialNumber = serialNumber;
                }
                else
                {
                    throw new Exception("Invalid Serial Number");
                }

            }

            public override string ToString()
            {
                return BirthDate.ToShortDateString();
            }

            private string buildDateString(string date)
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(date.Substring(0, 2)).Append("/").Append(date.Substring(2, 2))
                    .Append("/").Append(date.Substring(4, 2));

                return strBuilder.ToString();
            }
            private bool ValidateSsNum(string ssNum)
            {
                if (ssNum.Length != 10)
                {
                    return false;
                }

                try
                {
                    int sum = 0;
                    int add = 0;
                    for (int i = 0; i < ssNum.Length - 1; i++)
                    {
                        if (i % 2 == 0)
                        {
                            add = (Convert.ToInt32(ssNum.Substring(i, 1)) * 2);
                            if (add >= 10)
                            {
                                add = (add % 10) + (add / 10);
                            }
                            sum += add;

                        }
                        else
                        {
                            add = Convert.ToInt32(ssNum.Substring(i, 1));
                            sum += add;
                        }
                    }

                    int sumRoundedUp = 10 * ((sum + 9) / 10); 
                    int controlNum = sumRoundedUp - sum;

                    int lastDigit = Convert.ToInt32(ssNum.Substring(9, 1));

                    if (controlNum != lastDigit)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Data.ToString());
                }
            }

        }
    }
}
